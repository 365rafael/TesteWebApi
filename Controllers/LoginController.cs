using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteWebApi.Models;
using TesteWebApi.Repositorios.Interfaces;
using System;
using System.Threading.Tasks;


namespace TesteWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepositorio _loginRepositorio;
        private readonly IConfiguration _config;
        public LoginController(ILoginRepositorio loginRepositorio, IConfiguration config)
        {
            _loginRepositorio = loginRepositorio;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            var user = await _loginRepositorio.Authenticate(login.Username, login.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Username,
                LastToken = tokenString
            });
        }
    }
}
