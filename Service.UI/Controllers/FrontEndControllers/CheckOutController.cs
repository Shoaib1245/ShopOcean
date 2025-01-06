using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.EmailSender;
using Service.UI.Models;
using Service.UI.Utilize;
using System.Diagnostics.Metrics;
using static Service.UI.Models.CountriesLoadModel;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class CheckOutController : Controller
    {
        private readonly IApiServiceFrontEnd _apiServiceFrontEnd;
        private readonly EmailCustTemplate _Emailsend;
        public CheckOutController(IApiServiceFrontEnd apiServiceFrontEnd, EmailCustTemplate Emailsend)
        {
            _apiServiceFrontEnd = apiServiceFrontEnd;
            _Emailsend = Emailsend;
        }
        public async Task<IActionResult> Index()
        {
            var customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if(customerid>0)
            {
                var jsondata = await _apiServiceFrontEnd.GetByIdAsync("Cart/Getdata", customerid);
                var data = JsonConvert.DeserializeObject<List<CartModel>>(jsondata);
                return View(data);
            }
            else
            {
                return RedirectToAction("CustomerLogin", "CustomerRegistration");
            }
        }
        public async Task<IActionResult> LoadCheckout()
        {
            HttpClient _Client = new HttpClient();
            var response = await _Client.GetAsync("https://api.first.org/data/v1/countries");
                var jsondata = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CountryResponse>(jsondata);
               ViewBag.Countries= data.Data;


            return PartialView("_Checkout");
        }
        public async Task<int> Savedata(OrderPlacedModel obj)
        {
              obj.CustomerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId")); 
              var CustomerName = HttpContext.Session.GetString("CustomerName");
            _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Products.", obj.Email ?? "", "Order Status", $"Dear {CustomerName} Your Order Is on Pending");
            obj.OrderDate = DateTime.Now;
           var data= await _apiServiceFrontEnd.PostAsync(obj, "Cart/OrderNow");
            if (data == "1")
            {
                return 1;
            }
            return 0;
        }
    }
}
