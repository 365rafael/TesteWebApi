using TesteWebApi.Models;

namespace TesteWebApi.Repositorios.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<(int, string, string, int)> CriarPedidoAsync(int usuarioId, int produtoId, int quantidade);
        Task<List<Pedido>> BuscarTodosPedidos();
    }

}
