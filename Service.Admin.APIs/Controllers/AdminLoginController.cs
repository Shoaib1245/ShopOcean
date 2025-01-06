using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Admin.APIs.Features.Admin.Core;
using Service.Admin.APIs.Features.Admin.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Admin.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AdminService _adminService;
        public AdminLoginController(IConfiguration configuration,AdminService adminService)
        {

            _configuration = configuration;
            _adminService = adminService;
        }
        [HttpPost]
        [Route("logindata")]
        public async Task<IActionResult> Logindata(AdminModel obj)
        {
           
            IActionResult Respone = Unauthorized();
            var data = await _adminService.MatchEmailpassword(obj.Email, obj.Password);
            if (data != null)
            {
                var Token = CreateToken(data);
                return Ok(Token);
            }
            return BadRequest(Respone);
        }
        [NonAction]
        public string CreateToken(AdminModel obj)
        {
            var issu = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");

      var subject = new ClaimsIdentity(new[]
      {
      new Claim("Id",Convert.ToString( obj.Id)),
      new Claim(JwtRegisteredClaimNames.Email,obj.Email ?? ""),
      new Claim(JwtRegisteredClaimNames.Name, obj.Password ?? ""),
              });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = issu,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
 

      }
    }

