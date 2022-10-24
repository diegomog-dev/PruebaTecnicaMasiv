using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaMasiv.Models
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
