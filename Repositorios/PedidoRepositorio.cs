using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(int, string, string, int)> CriarPedidoAsync(int usuarioId, int produtoId, int quantidade)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                DataPedido = DateTime.Now,
            };

            _context.Pedidos.Add(pedido);

            var pedidoItem = new PedidoItem
            {
                Pedido = pedido,
                ProdutoId = produtoId,
                Quantidade = quantidade,
            };

            _context.PedidoItems.Add(pedidoItem);

            await _context.SaveChangesAsync();

            return (pedido.Id, usuario.Nome, produto.Nome, quantidade);
        }

    }
}
