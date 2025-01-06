using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Service;
using Service.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SellerService _sellerService;
        public LoginController(SellerService sellerService,IConfiguration config)
        {
            _configuration = config;
            _sellerService = sellerService;
        }
        [HttpPost]
        [Route("Logindata")]
        public async Task<IActionResult> UserLogin([FromBody] SellerLoginDTO obj)
        {
            IActionResult Respone = Unauthorized();
            SellerModel data = await _sellerService.SellerLogin(obj);
            if (data != null)
            {
                var Token = CreateToken(data);
                return Ok(Token);
            }
            return BadRequest(Respone);
        }
        [NonAction]
        [AllowAnonymous]
        public string CreateToken(SellerModel user)
        {
            var issu = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");


            var subject = new ClaimsIdentity(new[]
            {
      new Claim("Id",Convert.ToString( user.Id)),
      new Claim(JwtRegisteredClaimNames.Email,user.Email ?? ""),
      new Claim(JwtRegisteredClaimNames.Name, user.Password ?? ""),
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
        [HttpPost]
        [Route("PasswordUpdate")]
        public async Task<IActionResult> PasswordUpdate([FromBody] SellerDTOs obj)
        {          
             await _sellerService.PasswordUpdatee(obj);
             return Ok();
        }

        [HttpPost]
        [Route("SignupData")]
        public async Task<int> Signupdata([FromBody] SellerLoginDTO obj)
        {
           var data= await _sellerService.SignupSave(obj);
            if (data == 1)
            {
                return 1;
            }
            return 0;
        }
    }
}
