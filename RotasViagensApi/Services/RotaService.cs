using RotasViagensApi.Models;

public class RotaService : IRotaService
{
    private readonly ApplicationDb _context;

    public RotaService(ApplicationDb context)
    {
        _context = context;
    }

    public async Task<List<RotaViagem>> GetAllRotasAsync()
    {
        return await _context.GetAllRotasAsync();
    }

    public async Task<RotaViagem> GetRotaByIdAsync(int id)
    {
        return await _context.GetRotaByIdAsync(id);
    }

    public async Task<RotaViagem> CreateRotaAsync(RotaViagem rota)
    {
        return await _context.CreateRotaAsync(rota);
    }

    public async Task UpdateRotaAsync(RotaViagem rota)
    {
        await _context.UpdateRotaAsync(rota);
    }

    public async Task DeleteRotaAsync(RotaViagem rota)
    {
        try
        {
            await _context.DeleteRotaAsync(rota.Id);
            //await _context.DeleteEscalasAsync(rota.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<Resultado> GetBestRotaAsync(string origem, string destino)
    {
        // Implementação de exemplo. Você precisará adaptar conforme sua lógica de negócios e estrutura do banco de dados.
        var rotas = await GetAllRotasAsync();
        RotaViagem melhorRota = null;
        decimal melhorPreco = decimal.MaxValue;

        foreach (var rota in rotas)
        {
            if (rota.Origem == origem && rota.Destino == destino)
            {
                // Assumindo que o valor da rota reflete o custo (você pode precisar ajustar isso)
                if (rota.Valor < melhorPreco)
                {
                    melhorPreco = rota.Valor;
                    melhorRota = rota;
                }
            }
        }

        var resultadoPesquisa = new Resultado();

        if (melhorRota != null)
        {
            resultadoPesquisa.Resposta = melhorRota.Imprimir();
        }

        return resultadoPesquisa;
    }
}
