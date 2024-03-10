using Newtonsoft.Json;
using RotasViagensFront.Models;
using System.Net.Http;

namespace RotasViagensFront.Service
{
    public interface IApiService
    {

        Task<List<RotaViagemModel>> GetRotas();

        Task Adicionar(RotaViagemModel rotaViagem);

        Task<RotaViagemModel> GetRotaById(int id = 0);
        Task<RotaViagemModel> Atualizar(RotaViagemModel rotaViagem);
        Task Apagar(int id);
        Task<ResultadoModel> ProcurarMelhorResultado(ResultadoModel busca);
    }
}
