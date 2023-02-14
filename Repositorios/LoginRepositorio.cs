using Microsoft.EntityFrameworkCore;
using TesteWebApi.Context;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;

namespace TesteWebApi.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly ApplicationDbContext _context;

        public LoginRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Login> Authenticate(string username, string password)
        {
            var user = await _context.Logins.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful
            return user;
        }

       
    }
}
