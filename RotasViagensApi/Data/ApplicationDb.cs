using Microsoft.AspNetCore.Mvc.Rendering;
using RotasViagensApi.Models;
using System.Data.SqlClient;

public class ApplicationDb
{
    private readonly string _connectionString;

    public ApplicationDb(string connectionString)
    {
        _connectionString = connectionString;
    }

    // retorna todas as rotas de viagem do banco juntamente com suas escalas
    public async Task<List<RotaViagem>> GetAllRotasAsync()
    {
        var rotas = new List<RotaViagem>();

        // captura todas as rotas de viagem do banco
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM ROTAS", connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var rota = new RotaViagem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Origem = reader["Origem"].ToString(),
                        Destino = reader["Destino"].ToString(),
                        Valor = (decimal)reader["Valor"]
                    };
                    rotas.Add(rota);
                }
            }
        }

        // captura todas as escalas do banco
        var escalas = await GetEscalasAsync();

        // atribui as escalas a suas devidas rotas
        foreach (RotaViagem rota in rotas)
        {
            var escalasRota = escalas.Where(rt => rt.IdRota == rota.Id).ToList();

            rota.Escalas = escalasRota;
        }

        return rotas;
    }

    // retorna todas as escalas ou escalas referente a uma determinada rota
    public async Task<List<Escala>> GetEscalasAsync(int idRota = 0)
    {
        var escalas = new List<Escala>();

        var query = string.Empty;

        if (idRota > 0)
        {
            query = $"SELECT * FROM ESCALAS WHERE IDROTA = {idRota}";
        }
        else
        {
            query = $"SELECT * FROM ESCALAS WHERE IDROTA IN (SELECT ID FROM ROTAS)";
        }

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(query, connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var escala = new Escala
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        IdRota = Convert.ToInt32(reader["IdRota"]),
                        Destino = reader["Destino"].ToString()
                    };
                    escalas.Add(escala);
                }
            }
        }

        return escalas;
    }

    // retorna a rota determinada com suas escalas
    public async Task<RotaViagem> GetRotaByIdAsync(int idRota)
    {
        RotaViagem? rota = null;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM ROTAS WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", idRota);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    rota = new RotaViagem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Origem = reader["Origem"].ToString(),
                        Destino = reader["Destino"].ToString(),
                        Valor = (decimal)reader["Valor"]
                    };
                }
            }

        }

        if (rota != null)
        {
            var escalas = await GetEscalasAsync(idRota);

            rota.Escalas = escalas;
        }

        return rota ?? new RotaViagem();
    }

    // inclui uma rota com suas escalas no banco
    public async Task<RotaViagem> CreateRotaAsync(RotaViagem rota)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("INSERT INTO ROTAS (ORIGEM, DESTINO, VALOR) OUTPUT Inserted.ID VALUES (@Origem, @Destino, @Valor)", connection);
            command.Parameters.AddWithValue("@Origem", rota.Origem);
            command.Parameters.AddWithValue("@Destino", rota.Destino);
            command.Parameters.AddWithValue("@Valor", rota.Valor);

            //await command.ExecuteNonQueryAsync();

            var newIdRota = Convert.ToInt32(command.ExecuteScalar());

            rota.Id = newIdRota;

            if (rota.Escalas != null)
            {
                foreach (Escala item in rota.Escalas)
                {
                    var commandEscala = new SqlCommand("INSERT INTO ESCALAS (IDROTA, DESTINO) OUTPUT Inserted.ID VALUES (@IdRota, @Destino)", connection);
                    commandEscala.Parameters.AddWithValue("@IdRota", newIdRota);
                    commandEscala.Parameters.AddWithValue("@Destino", item.Destino);

                    //await commandEscala.ExecuteNonQueryAsync();

                    var newIdEscala = Convert.ToInt32(commandEscala.ExecuteScalar());

                    item.IdRota = newIdRota;
                    item.Id = newIdEscala;
                }
            }
        }

        return rota;
    }

    // atualiza uma rota e suas escalas no banco
    public async Task UpdateRotaAsync(RotaViagem rota)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("UPDATE ROTAS SET ORIGEM = @Origem, DESTINO = @Destino, VALOR = @Valor WHERE ID = @Id", connection);
            command.Parameters.AddWithValue("@Id", rota.Id);
            command.Parameters.AddWithValue("@Origem", rota.Origem);
            command.Parameters.AddWithValue("@Destino", rota.Destino);
            command.Parameters.AddWithValue("@Valor", rota.Valor);

            await command.ExecuteNonQueryAsync();

            if (rota.Escalas != null)
            {
                foreach (Escala item in rota.Escalas)
                {
                    var commandEscala = new SqlCommand("UPDATE ESCALAS SET DESTINO = @Destino WHERE ID = @Id", connection);
                    commandEscala.Parameters.AddWithValue("@Id", item.Id);
                    commandEscala.Parameters.AddWithValue("@Destino", item.Destino);

                    await commandEscala.ExecuteNonQueryAsync();
                }
            }
        }
    }
    // exclui uma rota com suas escalas do banco
    public async Task DeleteRotaAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM ROTAS WHERE Id = @Id; DELETE FROM ESCALAS WHERE IdRota = @Id ", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }

    // exclui as escalas de uma rota no banco (deprecated)
    public async Task DeleteEscalasAsync(int idRota)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM ESCALAS WHERE IdRota = @Id", connection);
            command.Parameters.AddWithValue("@Id", idRota);

            await command.ExecuteNonQueryAsync();
        }
    }
}


