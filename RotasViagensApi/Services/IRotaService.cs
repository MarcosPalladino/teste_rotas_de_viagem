using RotasViagensApi.Models;

public interface IRotaService
{
    // Método para recuperar todas as rotas de viagem disponíveis
    Task<List<RotaViagem>> GetAllRotasAsync();

    // Método para recuperar uma única rota de viagem por seu ID
    Task<RotaViagem> GetRotaByIdAsync(int id);

    // Método para criar uma nova rota de viagem
    Task<RotaViagem> CreateRotaAsync(RotaViagem rota);

    // Método para atualizar uma rota de viagem existente
    Task UpdateRotaAsync(RotaViagem rota);

    // Método para excluir uma rota de viagem existente
    Task DeleteRotaAsync(RotaViagem rota);

    // Método para encontrar a melhor rota de viagem entre uma origem e um destino
    // com base em critérios específicos, como o menor preço e o número de escalas
    Task<Resultado> GetBestRotaAsync(string origem, string destino);
}