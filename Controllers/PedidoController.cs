using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context.DTO;
using TesteWebApi.Models;
using TesteWebApi.Repositorios;
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
            var (pedidoId, usuarioNome, produtoNome, quantidade) = await _pedidoRepositorio.CriarPedidoAsync(id, dto.ProdutoId, dto.Quantidade);
            return Ok(new { PedidoId = pedidoId, UsuarioNome = usuarioNome, ProdutoNome = produtoNome, Quantidade = quantidade });
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

    }
}
