using System.ComponentModel.DataAnnotations;

namespace TesteWebApi.Models
{
    public class Login
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string LastToken { get; internal set; }
    }
}
