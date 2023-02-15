using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Context.DTO;
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

        public async Task<List<Pedido>> BuscarTodosPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<object> CriarPedidoAsync(int usuarioId, List<PedidoItemDto> produtos)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                DataPedido = DateTime.Now,
            };

            _context.Pedidos.Add(pedido);

            var pedidoItens = new List<PedidoItem>();

            foreach (var produto in produtos)
            {
                var produtoEntity = await _context.Produtos.FindAsync(produto.ProdutoId);
                if (produtoEntity == null)
                {
                    throw new ArgumentException($"Produto {produto.ProdutoId} não encontrado");
                }

                var pedidoItem = new PedidoItem
                {
                    Pedido = pedido,
                    Produto = produtoEntity,
                    Quantidade = produto.Quantidade,
                };

                _context.PedidoItems.Add(pedidoItem);

                pedidoItens.Add(pedidoItem);
            }

            await _context.SaveChangesAsync();

            var pedidoItensDto = pedidoItens.Select(pedidoItem =>
                new { ProdutoNome = pedidoItem.Produto.Nome, pedidoItem.Quantidade }
            ).ToList();

            return new { PedidoId = pedido.Id, UsuarioNome = usuario.Nome, Itens = pedidoItensDto };
        }

        public async Task<Pedido> BuscarPorId(int id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Apagar(int id)
        {
            var pedido = await BuscarPorId(id);
            if (pedido == null)
            {
                return false;
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
