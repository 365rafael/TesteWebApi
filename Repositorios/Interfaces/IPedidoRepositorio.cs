using TesteWebApi.Context.DTO;
using TesteWebApi.Models;

namespace TesteWebApi.Repositorios.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<object> CriarPedidoAsync(int usuarioId, List<PedidoItemDto> produtos);
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<Pedido> BuscarPorId(int id);
        Task<bool> Apagar(int id);
    }

}
