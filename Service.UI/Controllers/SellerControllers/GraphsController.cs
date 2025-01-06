using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Shared.DTOs;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class GraphsController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public GraphsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var Token = HttpContext.Session.GetString("Token");
            var Sellerid = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var jsondata= await _apiService.GetByIdToken("Seller/Graphsdata", Sellerid, Token ?? "");
            var data=JsonConvert.DeserializeObject<GraphsDTOs>(jsondata);
            return View(data);
        }
    }
}
