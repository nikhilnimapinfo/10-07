using JWT_Program.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWT_Program.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {

            _config = configuration;

        }
        private Users AuthinticateUser(Users user)
        {
            Users _user = null;
            if (user.UserName == "admin" && user.Password == "1234")
            {
                _user = new Users { UserName = "Nikhil" };
               
            }
            return _user;
        }

        private string GenerateToken(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,
                expires:DateTime.Now.AddMinutes(1),
                signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult responce = Unauthorized();
            var user_=AuthinticateUser(user);
            if (user_ != null)
            {
                var token=GenerateToken(user_);
                responce = Ok(new {token=token});
            }

            return responce;
        }
    }
}
