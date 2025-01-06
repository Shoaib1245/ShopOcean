using Microsoft.AspNetCore.Mvc;
using Service.UI.Utilize;
using System.Security.AccessControl;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class ReviewAndCommentsController : Controller
    {
        private readonly IApIServiceFrontEnd _apIServiceFrontEnd;
        public ReviewAndCommentsController(IApIServiceFrontEnd apIServiceFrontEnd)
        {
            _apIServiceFrontEnd = apIServiceFrontEnd;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetReview()
        {
           var data= await _apIServiceFrontEnd.GetJsonAsync("Review&Comments/Getdata");
            return View();
        }
    }
}
