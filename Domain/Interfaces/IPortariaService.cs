using Domain.Dtos.Portaria;
using Domain.Helpers;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPortariaService
    {
        Task<ApiDataService<IEnumerable<ControlePessoaDto>>> GetColaboradorControladoAsync();
        Task<ApiDataService<ControlePessoa>> PatchRegistrarHorasAsync(ApontarHrEntradaSaidaDto data);
        Task<ApiDataService<string>> PostGerarListaDeApontamentAsync(string geradoPor);
        Task<ApiDataService<ControlePessoa>> PatchRegistrarOcorrenciaAsync(PortariaOcorrenciaDto data);
    }
}
