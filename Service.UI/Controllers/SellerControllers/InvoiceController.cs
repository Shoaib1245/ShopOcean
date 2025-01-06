using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class InvoiceController : Controller
    {
        private readonly IApiServiceFrontEnd _apiService;
        public InvoiceController(IApiServiceFrontEnd apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int? OrderId)
        {
            var SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var queryString = $"SellerId={SellerId}&OrderId={OrderId}";
            var jsondata = await _apiService.GetJsonAsync($"Cart/GETInvoiceData?{queryString}");


            var data = JsonConvert.DeserializeObject<List<OrderDataModel>>(jsondata);

            return View(data);  
        }

    }
}
