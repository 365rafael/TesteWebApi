using TesteWebApi.Models;

namespace TesteWebApi.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<List<Produto>> BuscarTodosProdutos();
        Task<Produto> BuscarPorId(int id);
        Task<List<Produto>> BuscarProdutosPorCategoria(int idCategoria);
        Task<List<Produto>> BuscarProdutosPorCategoriaUrl(string url);
        Task<Produto> BuscarPorUrl(string url);
        Task<Produto> Adicionar(Produto produto);
        Task<Produto> Atualizar(Produto produto, int id);
        Task<bool> Apagar(int id);
    }
}
