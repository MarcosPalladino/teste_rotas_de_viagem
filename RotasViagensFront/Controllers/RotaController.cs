using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RotasViagensFront.Models;

namespace RotasViagensFront.Controllers
{
    public class RotaController : Controller
    {
        private readonly HttpClient _httpClient;

        public RotaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            List<RotaViagem> rotas = new List<RotaViagem>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7059/api/Rotas");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                rotas = JsonConvert.DeserializeObject<List<RotaViagem>>(content);
            }

            return View(rotas);
        }
    }
}
