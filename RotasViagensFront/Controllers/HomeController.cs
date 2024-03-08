using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RotasViagensFront.Models;
using System.Diagnostics;

namespace RotasViagensFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
