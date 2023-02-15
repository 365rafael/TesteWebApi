using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context.DTO;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        

        public PedidoController(IPedidoRepositorio pedidoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            
        }

        [HttpPost("/usuario/{id}/pedido")]
        public async Task<ActionResult<object>> CriarPedido(int id, [FromBody] CriarPedidoDto dto)
        {
            var (pedidoId, usuarioNome, produtoNome, quantidade) = await _pedidoRepositorio.CriarPedidoAsync(id, dto.ProdutoId, dto.Quantidade);
            return Ok(new { PedidoId = pedidoId, UsuarioNome = usuarioNome, ProdutoNome = produtoNome, Quantidade = quantidade });
        }

    }
}
