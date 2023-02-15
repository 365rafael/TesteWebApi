using Microsoft.AspNetCore.Mvc;
using TesteWebApi.Context.DTO;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        

        public PedidoController(IPedidoRepositorio pedidoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            
        }

        ///<summary>
        ///Adiciona um novo pedido ao banco de dados.
        ///</summary> 
        [HttpPost("/usuario/{id}/pedido")]
        public async Task<ActionResult<object>> CriarPedido(int id, [FromBody] CriarPedidoDto dto)
        {
            var pedidoItens = new List<PedidoItemDto>
        {
            new PedidoItemDto { ProdutoId = dto.ProdutoId, Quantidade = dto.Quantidade }
        };

            if (dto.ProdutosExtras != null)
            {
                pedidoItens.AddRange(dto.ProdutosExtras);
            }

            var resultado = await _pedidoRepositorio.CriarPedidoAsync(id, pedidoItens);

            return Ok(resultado);
        }

        ///<summary>
        ///Lista todos os pedidos cadastrados no banco de dados.
        ///</summary> 
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> BuscarTodosPedidos()
        {
            List<Pedido> pedidos = await _pedidoRepositorio.BuscarTodosPedidos();
            return Ok(pedidos);
        }

        ///<summary>
        ///Lista o pedido pelo seu id.
        ///</summary> 
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pedido>>> BuscarPorId(int id)
        {
            Pedido pedido = await _pedidoRepositorio.BuscarPorId(id);
            if(pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }
            return Ok(pedido);
        }

        ///<summary>
        ///Deleta um pedido, buscando pelo id.
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _pedidoRepositorio.Apagar(id);
            
            return Ok(apagado);
        }

    }
}
