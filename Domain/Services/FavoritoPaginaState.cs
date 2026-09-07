using Domain.Dtos.Favoritos;
using Domain.Interfaces;

namespace Domain.Services;

public class FavoritoPaginaState
{
    private readonly IFavoritoPaginaService _service;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private List<FavoritoPaginaDto> _favoritos = [];
    public FavoritoPaginaState(IFavoritoPaginaService service) => _service = service;
    public IReadOnlyList<FavoritoPaginaDto> Favoritos => _favoritos;
    public bool Carregado { get; private set; }

    public async Task GarantirCarregadoAsync()
    {
        if (Carregado) return;
        await _semaphore.WaitAsync();
        try
        {
            if (Carregado) return;
            var resultado = await _service.ListarAsync();
            if (!resultado.Success) throw new InvalidOperationException(resultado.Message ?? "Não foi possível carregar os favoritos.");
            _favoritos = resultado.Value ?? [];
            Carregado = true;
        }
        finally { _semaphore.Release(); }
    }

    public bool Contem(string rota) => _favoritos.Any(x => x.Rota.Trim('/').Equals(rota.Trim('/'), StringComparison.OrdinalIgnoreCase));
    public void Alternar(FavoritoPaginaDto favorito)
    {
        var atual = _favoritos.FirstOrDefault(x => x.Rota.Trim('/').Equals(favorito.Rota.Trim('/'), StringComparison.OrdinalIgnoreCase));
        if (atual is null) _favoritos.Add(favorito); else _favoritos.Remove(atual);
    }
    public void Limpar() { _favoritos = []; Carregado = false; }
}
