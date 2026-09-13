# Publicação e downloads

Acesse **TI > Publicar arquivo** (`/downloads/publicar`) com uma sessão autenticada.

- **Nova versão do coletor:** selecione o APK, incremento MAJOR/MINOR/PATCH, notas e atualização obrigatória. A web envia o multipart para `POST api/AppVersions/upload-apk` usando os campos existentes: `File`, `Plataforma=1`, `TargetOSType=3`, `VersionIncrementType`, `ReleaseNotes`, `IsObrigatoria`.
- **Outro aplicativo ou arquivo:** selecione um diretório existente ou marque **Criar novo diretório**, informe o nome e envie o arquivo. Arquivos existentes não são sobrescritos. A pasta `COLETOR` é reservada ao fluxo de versões.
- `/downloads` e `/download-agent` mostram o catálogo real da pasta compartilhada. `/downloads/COLETOR/{versao}` mostra os APKs daquela versão. Arquivos são servidos como anexos por `/downloads/file/{caminho}`, com suporte a retomada por HTTP Range.
- A consulta pública `/updates/coletor/check?targetOS=3&major=1&minor=0&patch=0` encaminha a consulta à API e preserva o envelope de resposta e a indicação de atualização obrigatória.

## Configuração no servidor

O padrão de `Downloads:RootPath`, tanto na web quanto na API, é `\\192.168.0.150\public\PPCP\Downloads`. Pode ser substituído em `appsettings.json` ou pela variável `Downloads__RootPath`.

A web usa `Domain.Helpers.Servidores.GetBaseUrl()` para chamar a API. Uma substituição pode ser definida em `Downloads:ApiBaseUrl` (`Downloads__ApiBaseUrl`), incluindo o sufixo `/api/`.

A identidade que executa a **ADI_WEB** precisa de leitura e escrita no compartilhamento para catálogo e uploads gerais; a **ADI_APP** precisa de escrita para publicar versões do coletor. O limite por arquivo é 500 MB. Se houver IIS/proxy na frente da API, seu limite de corpo também precisa comportar o multipart. A API utiliza o diretório temporário do ASP.NET Core durante o recebimento.

A origem pública configurada em `COLETOR/COLETOR/Helpers/Servidores.cs`, método `GetWebBaseUrl()`, é `https://192.168.0.235:5260/`, conforme o endereço informado. Esse endereço precisa encaminhar as rotas `/downloads/*` e `/updates/*` para a **ADI_WEB**, enquanto `/api/*` continua na **ADI_APP**, se ambas compartilham a mesma origem. Se a web for hospedada em outra origem, ajuste esse método antes de distribuir o APK.

As conexões do cliente HTTP de publicação exigem certificado confiável no servidor web. A publicação de versões mantém a exigência existente de consultar o último commit no GitHub e rejeitar um commit já publicado.

A API prepara o APK e o HTML em uma pasta temporária, move o conteúdo completo para `COLETOR/{versao}` e só então registra a versão. Falhas anteriores ao registro removem os arquivos criados pela requisição. A operação é serializada na instância da API; não há transação distribuída entre o SQL Server e o compartilhamento.

A pasta operacional `DATABASE`, arquivos temporários, links de diretório e arquivos internos de banco/chaves/configuração não são expostos no catálogo.

## Verificação local

Os testes usam diretórios temporários e não publicam versões reais:

```powershell
dotnet run --project ADI_WEB/Tests/DownloadsChecks/DownloadsChecks.csproj
dotnet run --project ADI_APP/Tests/PublicationChecks/PublicationChecks.csproj
```

O primeiro verifica armazenamento, proteção contra sobrescrita/caminhos inválidos, limpeza de upload incompleto, download público/Range e consulta de versão. O segundo verifica gravação dos arquivos antes do registro, falha no banco, duplicidade, validação de APK e concorrência.
