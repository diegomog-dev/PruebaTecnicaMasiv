using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMasiv.Helpers;
using PruebaTecnicaMasiv.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaMasiv.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        // Example Users
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id= 1,
                Email = "diego.mogollon@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id= 2,
                Email = "user@gmail.com",
                Name = "User",
                Password = "user"
            }
        };
        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        Username = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Credentials");
                }
                return Ok(Token);
            }catch(Exception ex)
            {
                throw new Exception("Get Token Error", ex);
            }
        }
    }
}
