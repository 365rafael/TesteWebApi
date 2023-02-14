using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public ProdutoRepositorio(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Produto>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Quantidade > 0 && p.Ativo == true);
        }

        public async Task<Produto> BuscarPorUrl(string url)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(p => p.Url == url && p.Ativo && !p.Excluido);

        }

        public async Task<List<Produto>> BuscarProdutosPorCategoria(int idCategoria)
        {
            return await _dbContext.Produtos
                .Where(p => p.CategoriaId == idCategoria && p.Ativo == true && p.Excluido == false && p.Quantidade > 0)
                .ToListAsync();
        }

        public async Task<List<Produto>> BuscarProdutosPorCategoriaUrl(string url)
        {
            var categoria = await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Url == url && c.Ativo && !c.Excluido);

            if (categoria == null)
            {
                return null;
            }

            var produtos = await _dbContext.Produtos
                .Where(p => p.CategoriaId == categoria.Id && p.Quantidade > 0 && p.Ativo && !p.Excluido)
                .ToListAsync();

            return produtos;
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            produto.Ativo= true;
            produto.Excluido= false;
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Atualizar(Produto produto, int id)
        {
            var produtoAtualizar = await _dbContext.Produtos.FindAsync(id);
            if (produtoAtualizar == null)
            {
                return null;
            }

            produtoAtualizar.CategoriaId = produto.CategoriaId;
            produtoAtualizar.Nome = produto.Nome;
            produtoAtualizar.Url = produto.Url;
            produtoAtualizar.Quantidade = produto.Quantidade;
            produtoAtualizar.Ativo = produto.Ativo;
            produtoAtualizar.Excluido = produto.Excluido;
            await _dbContext.SaveChangesAsync();
            return produtoAtualizar;

            
        }

        public async Task<bool> Apagar(int id)
        {
            var produto = await BuscarPorId(id);
            if (produto == null)
            {
                return false;
            }

            _dbContext.Produtos.Remove(produto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        
    }
}
