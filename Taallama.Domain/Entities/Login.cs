using System.ComponentModel.DataAnnotations;

namespace Taallama.Domain.Entities
{
    public class Login
    {
        [MaxLength(64), MinLength(5)]
        public string Username { get; set; }

        [MaxLength(32), MinLength(6)]
        public string Password { get; set; }
    }
}