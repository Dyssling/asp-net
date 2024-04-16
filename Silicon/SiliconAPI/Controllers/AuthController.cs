using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiliconAPI.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SiliconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [UseApiAndAccessKey]
        public IActionResult GetToken()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); //Instansierar en JwtSecurityTokenHandler, som sedan kommer kunna utföra saker som t.ex CreateToken
                var tokenDescriptor = new SecurityTokenDescriptor() //Skapar descriptor objektet som kommer innehålla all information som man vill ha i en token
                {
                    Issuer = "SiliconAPI",
                    Audience = "SiliconAPI",
                    Expires = DateTime.Now.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret")!)), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token)); //Eftersom CreateToken inte skapar en token i string format, så omvandlar jag den här
            }

            catch { }

            return BadRequest();
        }
    }
}
