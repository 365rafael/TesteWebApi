using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public UsuarioRepositorio(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            var existingUser = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Login == usuario.Login);

            if (existingUser != null)
            {
                throw new Exception("Já existe um usuário com esse login");
            }

            usuario.Ativo = true;
            usuario.Excluido = false;
            usuario.IsVerificado = false;
            usuario.ChaveVerificacao = Guid.NewGuid().ToString();

            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> VerificarEmail(string email, string chaveVerificacao)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if (usuario.ChaveVerificacao != chaveVerificacao)
            {
                throw new Exception("Chave de verificação inválida");
            }

            usuario.IsVerificado = true;
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario, int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário para o ID {id} não foi encontrado no banco de dados.");
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Login = usuario.Login;
            usuarioPorId.Senha = usuario.Senha;
            usuarioPorId.ChaveVerificacao = usuario.ChaveVerificacao;
            usuarioPorId.LastToken = usuario.LastToken;
            usuarioPorId.IsVerificado = usuario.IsVerificado;
            usuarioPorId.Ativo = usuario.Ativo;
            usuarioPorId.Excluido = usuario.Excluido;

            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"O usuário para o ID {id} não foi encontrado no banco de dados.");
            }
            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }
}
