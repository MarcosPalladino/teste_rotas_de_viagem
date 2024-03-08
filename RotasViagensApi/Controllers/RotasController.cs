using Microsoft.AspNetCore.Mvc;
using RotasViagensApi.Models;

[ApiController]
[Route("api/[controller]")]
public class RotasController : ControllerBase
{
    private readonly IRotaService _rotaService;

    public RotasController(IRotaService rotaService)
    {
        _rotaService = rotaService;
    }

    // GET: api/Rotas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RotaViagem>>> GetRotas()
    {
        return Ok(await _rotaService.GetAllRotasAsync());
    }

    // GET: api/Rotas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RotaViagem>> GetRota(int id)
    {
        var rota = await _rotaService.GetRotaByIdAsync(id);

        if (rota == null)
        {
            return NotFound();
        }

        return rota;
    }

    // POST: api/Rotas
    [HttpPost]
    public async Task<ActionResult<RotaViagem>> PostRota(RotaViagem rota)
    {
        var newrota = await _rotaService.CreateRotaAsync(rota);

        return newrota;
    }

    // PUT: api/Rotas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRota(int id, RotaViagem rota)
    {
        if (id != rota.Id)
        {
            return BadRequest();
        }

        await _rotaService.UpdateRotaAsync(rota);

        return NoContent();
    }

    // DELETE: api/Rotas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRota(int id)
    {
        var rota = await _rotaService.GetRotaByIdAsync(id);
        if (rota == null)
        {
            return NotFound();
        }

        await _rotaService.DeleteRotaAsync(rota);

        return NoContent();
    }

    // GET: api/Rotas/best
    [HttpGet("best")]
    public async Task<ActionResult<Resultado>> GetBestRota(string origem, string destino)
    {
        var resultado = await _rotaService.GetBestRotaAsync(origem, destino);

        if (resultado == null)
        {
            return NotFound();
        }

        return resultado;
    }
}
