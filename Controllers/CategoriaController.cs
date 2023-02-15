
using Microsoft.AspNetCore.Mvc;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        ///<summary>
        ///Lista todas as categorias salvas no banco de dados.
        ///</summary>
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> BuscarTodasCategorias()
        {
            List<Categoria> categorias = await _categoriaRepositorio.BuscarTodasCategorias();
            return Ok(categorias);
        }

        ///<summary>
        ///Busca a categoria pelo id.
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Categoria>>> BuscarPorId(int id)
        {
            Categoria categoria = await _categoriaRepositorio.BuscarPorId(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrado.");
            }
            return Ok(categoria);
        }

        ///<summary>
        ///Cria uma nova categoria no banco de dados.
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<Categoria>> Adicionar([FromBody] Categoria categoria)
        {
            Categoria cadastrada = await _categoriaRepositorio.Adicionar(categoria);
            return Ok(cadastrada);
        }

        ///<summary>
        ///Atualiza os dados da categoria, buscando pelo id.
        ///</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> Atualizar([FromBody] Categoria categoria, int id)
        {
            categoria.Id = id;
            Categoria atualizada = await _categoriaRepositorio.Atualizar(categoria, id);
            return Ok(atualizada);
        }

        ///<summary>
        ///Deleta a categoria, buscando pelo id.
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Apagar(int id)
        {

            bool apagado = await _categoriaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}
