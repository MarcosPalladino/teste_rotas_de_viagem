function submitEditForm() {
    var id = $('#editId').val();
    var origem = $('#editOrigem').val();
    var destino = $('#editDestino').val();

    // Monta o objeto com os dados coletados do formulário
    var rotaData = {
        Id: id,
        Origem: origem,
        Destino: destino
    };

    var apiUrl = 'https://localhost:7059/api/Rotas';

    // Se tiver um ID, estamos atualizando uma rota existente, caso contrário, estamos criando uma nova
    var methodType = id ? 'PUT' : 'POST';
    var urlWithId = id ? apiUrl + '/' + id : apiUrl;

    // Faz a requisição para a Web API
    $.ajax({
        url: urlWithId,
        type: methodType,
        contentType: 'application/json',
        data: JSON.stringify(rotaData),
        success: function (result) {
            // Fechar a janela modal
            $('#editModal').modal('hide');

            // Recarregar parte da página ou redirecionar conforme necessário
            location.reload(); 
        },
        error: function (xhr, status, error) {
            // Tratar erros aqui
            console.error('Erro ao atualizar a rota: ', error);
        }
    });
}

function submitDeleteForm() {
    var id = $('#deleteId').val(); // Obtém o ID da rota de viagem a ser excluída.

    var apiUrl = `https://localhost:7059/api/Rotas/${id}`;

    // Faz a requisição para a Web API usando o método DELETE
    $.ajax({
        url: apiUrl,
        type: 'DELETE', // Tipo de requisição HTTP adequado para deletar um recurso
        success: function (result) {
            // Fecha a janela modal após a exclusão bem-sucedida
            $('#deleteModal').modal('hide');

            // Recarrega a página ou atualiza a lista de rotas de viagem para refletir a exclusão
            location.reload(); // Simples recarga da página
        },
        error: function (xhr, status, error) {
            // Trate os possíveis erros aqui
            console.error('Erro ao excluir a rota: ', error);
        }
    });
}


function submitSearchForm() {
    var origem = $('#searchOrigin').val();
    var destino = $('#searchDestination').val();
        
    var searchUrl = `https://localhost:7059/api/Rotas/best?origem=${origem}&destino=${destino}`;

    // Faz a requisição para a Web API
    $.ajax({
        url: searchUrl,
        type: 'GET',
        success: function (result) {
            // Trate o resultado da busca aqui
            console.log(result);
            // Fechar a janela modal após a busca
            $('#searchModal').modal('hide');
        },
        error: function (xhr, status, error) {
            // Trate os erros de requisição aqui
            console.error('Erro ao buscar rotas: ', error);
        }
    });
}


