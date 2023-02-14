using Microsoft.AspNetCore.Mvc;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        ///<summary>
        ///Lista todos os usuários cadastrados no banco de dados.
        ///</summary> 
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarTodosUsuarios() 
        {
            List<Usuario> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        ///<summary>
        ///Lista o usuário pelo seu id.
        ///</summary> 
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Usuario>>> BuscarPorId(int id)
        {
            Usuario usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);
        }

        ///<summary>
        ///Cadastra um novo usuário no banco de dados.
        ///</summary> 
        [HttpPost]
        public async Task<ActionResult<Usuario>> Cadastrar([FromBody] Usuario usuario)
        {
            Usuario cadastrado = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(cadastrado);
        }

        ///<summary>
        ///Atualiza um cadastro de usuário, buscando pelo id.
        ///</summary> 
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar([FromBody] Usuario usuario, int id)
        {
            usuario.Id = id;
            Usuario atualizado = await _usuarioRepositorio.Atualizar(usuario, id);
            return Ok(atualizado);
        }

        ///<summary>
        ///Deleta um cadastro de usuário, buscando pelo id.
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Apagar(int id)
        {
            
            bool apagado = await _usuarioRepositorio.Apagar(id);
            return Ok(apagado);
        }

        ///<summary>
        ///Faz a verificação do usuário pelo email e código de verificação.
        ///</summary>
        [HttpPost("verify")]
        public async Task<ActionResult<Usuario>> Verificar([FromBody] string email, string chaveVerificacao)
        {
            Usuario usuario = await _usuarioRepositorio.VerificarEmail(email, chaveVerificacao);

            return Ok(usuario);
        }
    }
}
