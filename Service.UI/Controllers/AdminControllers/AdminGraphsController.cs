using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Shared.DTOs;
using Service.UI.EmailSender;
using Service.UI.Utilize;

namespace Service.UI.Controllers.AdminControllers
{
    public class AdminGraphsController : Controller
    {
        private readonly IApiServiceAdminn _apiservice;
        public AdminGraphsController(IApiServiceAdminn apiservice)
        {
            _apiservice = apiservice;
        }
        public async Task<IActionResult> Index()
        {
            var jsondata = await _apiservice.GetJsonAsync("SellerProfileStatus/GetGRAphs");
            var data = JsonConvert.DeserializeObject<AdminGraphsDTOs>(jsondata);
            return View(data);
        }
    }
}
