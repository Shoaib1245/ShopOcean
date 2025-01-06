using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.AdminControllers
{
    public class AdminProfileController : Controller
    {
        private readonly IApiServiceAdminn _apiServiceAdmin;
        public AdminProfileController(IApiServiceAdminn apiServiceAdmin)
        {

            this._apiServiceAdmin = apiServiceAdmin;

        }
        public async Task<IActionResult> Index()
        {
           
            var Token = HttpContext.Session.GetString("Tokendata");
           var jsondata= await _apiServiceAdmin.GetByIdToken("AdminProfile/GetAdmindata", Token);
            var data=JsonConvert.DeserializeObject<AdminModel>(jsondata);
            return View(data);
        }
    }
}
