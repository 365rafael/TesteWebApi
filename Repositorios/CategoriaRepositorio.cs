using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoriaRepositorio(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<List<Categoria>> BuscarTodasCategorias()
        {
            return await _dbContext.Categorias.ToListAsync();
        }
        public async Task<Categoria> BuscarPorId(int id)
        {
            return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }
        

        public async Task<Categoria> Adicionar(Categoria categoria)
        {
            var existingCategory = await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Nome == categoria.Nome);

            if (existingCategory != null)
            {
                throw new Exception("Já existe uma categoria com esse nome");
            }

            categoria.Excluido = false;

            _dbContext.Categorias.Add(categoria);
            await _dbContext.SaveChangesAsync();
            return categoria;
        }
        public async Task<Categoria> Atualizar(Categoria categoria, int id)
        {
            Categoria categoriaPorId = await BuscarPorId(id);
            if(categoriaPorId == null)
            {
                throw new Exception($"A categoria para o ID: {id} não foi encontrada no banco de dados");
            }
            categoriaPorId.Nome= categoria.Nome;
            categoriaPorId.Url = categoria.Url;
            categoriaPorId.Ativo = categoria.Ativo;
            categoriaPorId.Excluido = categoria.Excluido;

            _dbContext.Categorias.Update(categoriaPorId);
            await _dbContext.SaveChangesAsync();

            return categoriaPorId;
        }
        public async Task<bool> Apagar(int id)
        {
            Categoria categoriaPorId = await BuscarPorId(id);
            if (categoriaPorId == null)
            {
                throw new Exception($"A categoria para o ID {id} não foi encontrada no banco de dados.");
            }
            _dbContext.Categorias.Remove(categoriaPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Categoria> BuscarPorUrl(string url)
        {
            return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Url == url);
        }



    }
}
