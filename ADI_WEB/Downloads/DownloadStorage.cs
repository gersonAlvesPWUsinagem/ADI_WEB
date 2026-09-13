namespace ADI_WEB.Downloads;

public sealed record DownloadEntry(string Name, string Path, bool IsDirectory, long Size);

// The share is accessed by the web server identity, never by the browser.
public sealed class DownloadStorage(IConfiguration configuration)
{
    public const long MaxFileSize = 500L * 1024 * 1024;
    private readonly string _root = System.IO.Path.GetFullPath(configuration["Downloads:RootPath"]
        ?? @"\\192.168.0.150\public\PPCP\Downloads");

    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name != name.Trim() || name.EndsWith('.') ||
            name.StartsWith('.') || name.IndexOfAny("<>:\"/\\|?*".ToCharArray()) >= 0 ||
            name.Any(char.IsControl) || name.Length > 150 ||
            System.Text.RegularExpressions.Regex.IsMatch(name, @"^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9])(?:\.|$)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            throw new ArgumentException("Informe um nome válido, sem caminhos ou caracteres especiais.");
    }

    public string Resolve(string? relative)
    {
        var current = _root;
        RejectLink(current);
        if (string.IsNullOrEmpty(relative)) return current;
        foreach (var part in relative.Split('/'))
        {
            ValidateName(part);
            // This folder already contains operational databases, not public downloads.
            if (part.Equals("DATABASE", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Esta pasta não está disponível para downloads.");
            current = System.IO.Path.Combine(current, part);
            RejectLink(current);
        }
        return current;
    }

    private static void RejectLink(string path)
    {
        if ((Directory.Exists(path) || File.Exists(path)) &&
            (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0)
            throw new ArgumentException("Links de diretório não são permitidos.");
    }

    public IReadOnlyList<DownloadEntry> List(string? relative)
    {
        var directory = Resolve(relative);
        if (!Directory.Exists(directory)) throw new DirectoryNotFoundException("Pasta de downloads indisponível.");
        var entries = new List<DownloadEntry>();
        foreach (var path in Directory.EnumerateFileSystemEntries(directory))
        {
            var name = System.IO.Path.GetFileName(path);
            var child = string.IsNullOrEmpty(relative) ? name : $"{relative}/{name}";
            try { Resolve(child); }
            catch (ArgumentException) { continue; }
            var isDirectory = Directory.Exists(path);
            if (!isDirectory && !IsPublicFile(name)) continue;
            entries.Add(new(name, child, isDirectory, isDirectory ? 0 : new FileInfo(path).Length));
        }
        return entries.OrderByDescending(e => e.IsDirectory)
            .ThenByDescending(e => Version.TryParse(e.Name, out var version) ? version : new Version())
            .ThenBy(e => e.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public static bool IsPublicFile(string name) => !name.StartsWith('.') &&
        !new[] { ".db", ".sqlite", ".sqlite3", ".mdb", ".key", ".pem", ".pfx", ".config", ".html" }
            .Contains(System.IO.Path.GetExtension(name), StringComparer.OrdinalIgnoreCase);

    public async Task SaveAsync(string directory, string name, Stream source, long length, bool createDirectory,
        CancellationToken cancellationToken = default)
    {
        ValidateName(name);
        if (!IsPublicFile(name)) throw new ArgumentException("Este tipo de arquivo não pode ser publicado no catálogo.");
        if (length <= 0 || length > MaxFileSize) throw new ArgumentException("Selecione um arquivo entre 1 byte e 500 MB.");
        if (directory.Split('/')[0].Equals("COLETOR", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Use a publicação de versão do coletor para esta pasta.");
        var destination = Resolve(directory);
        if (createDirectory) Directory.CreateDirectory(destination);
        if (!Directory.Exists(destination)) throw new DirectoryNotFoundException("Selecione uma pasta existente ou marque a criação de uma nova.");
        var target = Resolve($"{directory}/{name}");
        if (File.Exists(target)) throw new IOException("Já existe um arquivo com esse nome. Escolha outro nome ou diretório.");
        var temporary = System.IO.Path.Combine(destination, $".{Guid.NewGuid():N}.upload");
        try
        {
            await using (var output = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true))
            {
                var buffer = new byte[81920];
                long total = 0;
                int read;
                while ((read = await source.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    total += read;
                    if (total > length || total > MaxFileSize) throw new IOException("O arquivo excedeu o tamanho informado.");
                    await output.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                }
                if (total != length) throw new IOException("O envio do arquivo ficou incompleto. Tente novamente.");
            }
            File.Move(temporary, target, overwrite: false);
        }
        finally
        {
            if (File.Exists(temporary)) File.Delete(temporary);
        }
    }
}
