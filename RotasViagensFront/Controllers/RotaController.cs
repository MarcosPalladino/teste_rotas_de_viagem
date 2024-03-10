using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RotasViagensFront.Models;
using RotasViagensFront.Service;

namespace RotasViagensFront.Controllers
{
    public class RotaController : Controller
    {
        private readonly IApiService _apiService;

        public RotaController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var rotas = await _apiService.GetRotas();

            return View(rotas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(RotaViagemModel rotaViagem)
        {
            await _apiService.Adicionar(rotaViagem);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(RotaViagemModel rotaViagem)
        {
            var rota = await _apiService.GetRotaById(rotaViagem.Id);

            if (rota.Id == 0) throw new Exception("Houve um erro na atualização da rota");

            await _apiService.Atualizar(rotaViagem);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var rota = await _apiService.GetRotaById(id);

            return View(rota);
        }

        public async Task<IActionResult> ApagarConfirma(int id)
        {
            var rota = await _apiService.GetRotaById(id);

            return View(rota);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            var rota = await _apiService.GetRotaById(id);

            if (rota.Id == 0) throw new Exception("Houve um erro na exclusão da rota");

            await _apiService.Apagar(id);

            return RedirectToAction("Index");
        }


    }
}
