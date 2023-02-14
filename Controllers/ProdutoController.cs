using Microsoft.AspNetCore.Mvc;

using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio, ICategoriaRepositorio categoriaRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
        }

        ///<summary>
        ///Lista todos os produtos cadastrados no banco.
        ///</summary> 
        [HttpGet] 
        public async Task<ActionResult<List<Produto>>> BuscarTodosProdutos()
        {
            List<Produto> produtos = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }

        ///<summary>
        ///Lista as categorias com produto cadastrado no banco.
        ///</summary> 
        [HttpGet("categorias-produtos-ativos")]
        public async Task<ActionResult<List<Categoria>>> BuscarCategoriasComProdutosAtivos()
        {
            var categorias = await _categoriaRepositorio.BuscarTodasCategorias();
            var produtos = await _produtoRepositorio.BuscarTodosProdutos();

            var categoriasComProdutosAtivos = categorias
                .Where(c => produtos.Any(p => p.CategoriaId == c.Id && p.Quantidade > 0 && p.Ativo))
                .ToList();

            return categoriasComProdutosAtivos;
        }

        ///<summary>
        ///Lista o produto cadastrado no banco pelo id.
        ///</summary> 
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> BuscarPorId(int id)
        {
            Produto produto = await _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado ou não disponível");
            }
            return Ok(produto);
        }

        ///<summary>
        ///Lista o produto cadastrado no banco pela url.
        ///</summary>
        [HttpGet]
        [Route("produto-url/{url}")]
        public async Task<ActionResult<Produto>> BuscarPorUrl(string url)
        {
            Produto produto = await _produtoRepositorio.BuscarPorUrl(url);
            if (produto == null)
            {
                return NotFound("Produto não encontrado ou não disponível");
            }
            return Ok(produto);
        }

        ///<summary>
        ///Lista os produtos cadastrados no banco pelo id da sua categoria.
        ///</summary>
        [HttpGet("categoria/{id}")]
        public async Task<ActionResult<List<Produto>>> BuscarProdutosPorCategoria(int id)
        {
            List<Produto> produtos = await _produtoRepositorio.BuscarProdutosPorCategoria(id);
            return Ok(produtos);
        }

        ///<summary>
        ///Lista os produtos cadastrados no banco pela url da sua categoria.
        ///</summary>
        [HttpGet("produtos-por-categoria-url/{categoriaUrl}")]
        public async Task<ActionResult<List<Produto>>> BuscarProdutosPorCategoriaUrl(string categoriaUrl)
        {
            Categoria categoria = await _categoriaRepositorio.BuscarPorUrl(categoriaUrl);
            if (categoria == null || !categoria.Ativo)
                return NotFound("Categoria não disponível");

            List<Produto> produtos = await _produtoRepositorio.BuscarProdutosPorCategoria(categoria.Id);
            return Ok(produtos);
        }

        ///<summary>
        ///Cadastra um novo produto.
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<Produto>> Adicionar([FromBody] Produto produto)
        {
            Produto adicionado = await _produtoRepositorio.Adicionar(produto);
            return Ok(adicionado);
        }

        ///<summary>
        ///Atualiza um produto, buscando pelo id.
        ///</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Atualizar([FromBody] Produto produto, int id)
        {
            produto.Id = id;
            Produto atualizado = await _produtoRepositorio.Atualizar(produto, id);
            return Ok(atualizado);
        }

        ///<summary>
        ///Deleta um produto, buscando pelo id.
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _produtoRepositorio.Apagar(id);
            return Ok(apagado);
        }

        
    }

}

