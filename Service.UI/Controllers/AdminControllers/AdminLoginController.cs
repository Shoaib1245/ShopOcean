using Microsoft.AspNetCore.Mvc;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.AdminControllers
{
    public class AdminLoginController : Controller
    {
        private readonly IApiServiceAdminn _apiserviceadmin;
        public AdminLoginController(IApiServiceAdminn apiServiceAdmin)
        {
            _apiserviceadmin = apiServiceAdmin;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminLoginn()
        {
            return View();
        }
        public async Task<int> LoginData(AdminModel model)
        {

            var Tokendata = await _apiserviceadmin.PostAsync(model,"AdminLogin/logindata");
            HttpContext.Session.SetString("Tokendata", Tokendata);

            if (Tokendata != "" && Tokendata != "{\"statusCode\":401}")
            {
                return 1;
            }
            return 0;
        }
    }
}
