using Microsoft.AspNetCore.Mvc;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
