using Newtonsoft.Json;
using RotasViagensFront.Models;

namespace RotasViagensFront.Service
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase = "https://localhost:7059";

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_urlBase);
        }

        public async Task<List<RotaViagemModel>> GetRotas()
        {
            try
            {
                List<RotaViagemModel>? rotas = new List<RotaViagemModel>();

                HttpResponseMessage response = await _httpClient.GetAsync("/api/Rotas");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    rotas = JsonConvert.DeserializeObject<List<RotaViagemModel>>(content);
                }

                return rotas ?? new List<RotaViagemModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Adicionar(RotaViagemModel rotaViagem)
        {
            try
            {
                RotaViagemModel? novaRota = new RotaViagemModel();

                var response = await _httpClient.PostAsJsonAsync($"/api/Rotas", rotaViagem);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    novaRota = JsonConvert.DeserializeObject<RotaViagemModel>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<RotaViagemModel> GetRotaById(int id = 0)
        {
            try
            {
                RotaViagemModel? rota = new RotaViagemModel();

                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Rotas/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    rota = JsonConvert.DeserializeObject<RotaViagemModel>(content);
                }

                return rota ?? new RotaViagemModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<RotaViagemModel> Atualizar(RotaViagemModel rotaViagem)
        {
            try
            {
                RotaViagemModel? rota = new RotaViagemModel();

                var response = await _httpClient.PutAsJsonAsync($"/api/Rotas", rotaViagem);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    rota = JsonConvert.DeserializeObject<RotaViagemModel>(content);
                }

                return rota ?? new RotaViagemModel();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Apagar(int id)
        {
            RotaViagemModel? rota = new RotaViagemModel();

            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Rotas/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                rota = JsonConvert.DeserializeObject<RotaViagemModel>(content);
            }
        }

        public async Task<ResultadoModel> ProcurarMelhorResultado(ResultadoModel busca)
        {
            try
            {
                ResultadoModel? resultado = new ResultadoModel();

                HttpResponseMessage response = await _httpClient.GetAsync($"api/Rotas/best?origem={busca.Origem}&destino={busca.Destino}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    resultado = JsonConvert.DeserializeObject<ResultadoModel>(content);
                }

                return resultado ?? new ResultadoModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
