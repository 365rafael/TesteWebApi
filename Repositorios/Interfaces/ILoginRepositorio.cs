using TesteWebApi.Models;

namespace TesteWebApi.Repositorios.Interfaces
{
    public interface ILoginRepositorio
    {
        Task<Login> Authenticate(string username, string password);
        
    }
}
