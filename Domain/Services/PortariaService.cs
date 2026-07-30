using Domain.Dtos.Portaria;
using Domain.Helpers;
using Domain.Interfaces;
using Shared.Models;

namespace Domain.Services
{
    public class PortariaService : BaseHttpService, IPortariaService
    {

        public PortariaService(HttpClient httpClient, IDataSessionHelper dataSession)
        : base(httpClient, dataSession)
        {
        }

        public async Task<ApiDataService<IEnumerable<ControlePessoaDto>>> GetColaboradorControladoAsync()
        {
            var result = await GetAsync<IEnumerable<ControlePessoaDto>>("portaria/get-colaborador-controlado");
            return result;
        }
        public async Task<ApiDataService<ControlePessoa>> PatchRegistrarHorasAsync(ApontarHrEntradaSaidaDto data)
        {
            var result = await PatchAsync<ControlePessoa>("Portaria/RegistrarHoras", data);
            return result;
        }
        public async Task<ApiDataService<string>> PostGerarListaDeApontamentAsync(string geradoPor)
        {
            var result = await PostAsync<string>(geradoPor!, "Portaria");
            return result;
        }
        public async Task<ApiDataService<ControlePessoa>> PatchRegistrarOcorrenciaAsync(PortariaOcorrenciaDto data)
        {
            var result = await PatchAsync<ControlePessoa>("Portaria/PatchOcorrencia", data);
            return result;
        }
        public async Task<ApiDataService<IEnumerable<ColaboradoresControladosDto>>> GetColaboradorAsync()
        {
            var result = await GetAsync<IEnumerable<ColaboradoresControladosDto>>("/portaria");
            return result;
        }

        

        //public async Task<ApiDataService<ColaboradoresControladosDto>> PatchAtivarControleColaboradorAsync(IncluirRemoverColaboradoresControladoDTO data)
        //{
        //    try
        //    {
        //        var result = await _requestResponse.PatchAsync<IncluirRemoverColaboradoresControladoDTO, ApiDataService<ColaboradoresControladosDto>>("/portaria", data);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<ColaboradoresControladosDto>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message,
        //        };
        //    }
        //}

        //public async Task<ApiDataService<PortariaRegistro>> PostPortariaRegistroColaboradorAsync(PortariaRegistroColaboradorDto portariaRegistroColaboradorDto)
        //{
        //    try
        //    {
        //        var data = new PortariaRegistro
        //        {
        //            COLABORADOR_CONDUTOR = portariaRegistroColaboradorDto.ColaboradorCondutor!.ToUpper(),
        //            MATRICULA = portariaRegistroColaboradorDto.Matricula!.ToUpper(),
        //            DEPARTAMENTO_EMPRESA = portariaRegistroColaboradorDto.DepartamentoEmpresa!.ToUpper(),
        //            DOCUMENTO = portariaRegistroColaboradorDto.Documento!.ToUpper(),
        //            TIPO = TipoRegistroEnum.COLABORADOR,
        //        };
        //        var result = await _requestResponse.PostAsync<PortariaRegistro, ApiDataService<PortariaRegistro>>(data!, "/PortariaRegistro/PostCreate");
        //        if (result == null || result.IsError)
        //            return result!;

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<PortariaRegistro>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}
        //public async Task<ApiDataService<string>> PostGerarListaDeApontamentAsync(string geradoPor)
        //{
        //    try
        //    {

        //        var result = await _requestResponse.PostAsync<string, ApiDataService<string>>(geradoPor!, "/portaria");
        //        if (result == null || result.IsError)
        //            return result!;

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<string>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}

        //public async Task<ApiDataService<PortariaRegistro>> PutPortariaRegistroColaboradorAsync(PortariaRegistroColaboradorDto portariaRegistroColaboradorDto)
        //{
        //    try
        //    {
        //        var data = new PortariaRegistro
        //        {
        //            REGISTROID = portariaRegistroColaboradorDto.RegistroID,
        //            COLABORADOR_CONDUTOR = portariaRegistroColaboradorDto.ColaboradorCondutor!.ToUpper(),
        //            MATRICULA = portariaRegistroColaboradorDto.Matricula!.ToUpper(),
        //            DEPARTAMENTO_EMPRESA = portariaRegistroColaboradorDto.DepartamentoEmpresa!.ToUpper(),
        //            DOCUMENTO = portariaRegistroColaboradorDto.Documento!.ToUpper(),
        //            TIPO = TipoRegistroEnum.COLABORADOR,
        //        };
        //        var result = await _requestResponse.PutAsync<PortariaRegistro, ApiDataService<PortariaRegistro>>(data, "/PortariaRegistro/PutUpdate");
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<PortariaRegistro>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}

        //public async Task<ApiDataService<IEnumerable<PortariaRegistro>>> GetAtivosAsync()
        //{
        //    try
        //    {
        //        var result = await _requestResponse.GetAsync<ApiDataService<IEnumerable<PortariaRegistro>>>("/PortariaRegistro/GetAtivos");
        //        if (result == null || result.IsError)
        //            return new ApiDataService<IEnumerable<PortariaRegistro>>
        //            {
        //                IsError = true,
        //                MessageError = result?.MessageError ?? "Erro desconhecido"
        //            };

        //        return new ApiDataService<IEnumerable<PortariaRegistro>>
        //        {
        //            Data = result.Data,
        //            IsError = result.IsError,
        //            MessageError = result.MessageError
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<IEnumerable<PortariaRegistro>>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}

        //public async Task<ApiDataService<IEnumerable<TodosColaboradoresControladosDto>>> GetTodosColaboradoresControladosAsync()
        //{
        //    try
        //    {
        //        var result = await _requestResponse.GetAsync<ApiDataService<IEnumerable<TodosColaboradoresControladosDto>>>("/portaria/GetTodosColaboradoresControlados");

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<IEnumerable<TodosColaboradoresControladosDto>>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}
        //public async Task<ApiDataService<IEnumerable<ColaboradoresControladosRelDto>>> GetColaboradoresControladosRelAsync(ColaboradoresControladosRelGridDto data)
        //{
        //    try
        //    {
        //        var result =  await _requestResponse
        //            .PostAsync<ColaboradoresControladosRelGridDto, ApiDataService<IEnumerable<ColaboradoresControladosRelDto>>>
        //            (data!,"/portaria/GetColaboradoresControladosRel");

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<IEnumerable<ColaboradoresControladosRelDto>>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}
        //public async Task<ApiDataService<bool>> PatchPortariaRegistroColaboradorAsync(int id)
        //{
        //    try
        //    {
        //        var result = await _requestResponse.PatchAsync<bool, ApiDataService<bool>>($"/PortariaRegistro/ToggleAtivo/{id}", false);
        //        if (result == null || result.IsError)
        //            return new ApiDataService<bool>
        //            {
        //                IsError = true,
        //                MessageError = result?.MessageError ?? "Erro desconhecido"
        //            };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiDataService<bool>
        //        {
        //            IsError = true,
        //            MessageError = ex.Message
        //        };
        //    }
        //}
    }
}
