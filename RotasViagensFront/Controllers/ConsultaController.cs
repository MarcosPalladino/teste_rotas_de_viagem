using RotasViagensFront.Models;
using Microsoft.AspNetCore.Mvc;
using RotasViagensFront.Service;

namespace RotasViagensFront.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IApiService _apiService;

        public ConsultaController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Consultar(ResultadoModel busca)
        {
            ResultadoModel resultado = await _apiService.ProcurarMelhorResultado(busca);

            return View(resultado);
        }
    }
}
