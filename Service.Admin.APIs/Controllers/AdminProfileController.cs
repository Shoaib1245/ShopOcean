using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Admin.APIs.Features.Admin.Core;
using Service.Admin.APIs.Features.Admin.Service;

namespace Service.Admin.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminProfileController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminProfileController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        [Route("GetAdmindata")]
        public async Task<AdminModel> GetAdmindata()
        {
            var userClaims = User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id");
            var AdminId = userIdClaim?.Value;
            var data = await _adminService.GetAdmindata(Convert.ToInt32(AdminId));
            return data;
        }
    }
}
