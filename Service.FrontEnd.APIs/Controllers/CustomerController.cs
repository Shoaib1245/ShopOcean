using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Service.FrontEnd.APIs.Features.Customer.Core;
using Service.FrontEnd.APIs.Features.Customer.Service;
using Service.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.FrontEnd.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IConfiguration _configuration;
        public CustomerController(CustomerService customerService,IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CustomerSignupData")]
        public async Task<int> Signupdata([FromBody] CustomerModel obj)
        {
            var d = await _customerService.CustomerSignupSave(obj);
            if (d == 1)
            {
                return 1;
            }
            return 0;
        }
        [HttpPost]
        [Route("CustomerProfile")]
        public async Task<int> CustomerProfile([FromForm] JsondataDTOs obj)
        {
            var data = JsonConvert.DeserializeObject<CustomerModel>(obj.jsondata);
            var d = await _customerService.CustomerProfile(data, obj.file);
            if (d == 1)
            {
                return 1;
            }
            return 0;
        }
        [HttpGet]
        [Route("Logindata")]
        public async Task<IActionResult> UserLogin(string Email,string Password)
        {
            IActionResult Respone = Unauthorized();
            var data = await _customerService.CustomerLogin(Email,Password);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest(Respone);
        }

        [HttpGet]
        [Route("GetdatabyId{CustomerId}")]
        public async Task<IActionResult> getdatabyid(int CustomerId)
        {
            var data = await _customerService.GetdatabyId(CustomerId);
                return Ok(data);

        }
    }
}
