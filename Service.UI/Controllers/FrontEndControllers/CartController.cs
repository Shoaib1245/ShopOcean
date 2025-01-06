using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class CartController : Controller
    {
        private readonly IApiServiceFrontEnd _apiService;
        public CartController(IApiServiceFrontEnd apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        { 
            var customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if(customerid != 0 )
            {
                return View();
            }
            else
            {
                return RedirectToAction("CustomerLogin","CustomerRegistration");
            }
  
        }
        public async Task<int> CartDataSave(int SizeId,int Quantity,int ProductId)
        {

           CartModel model = new CartModel();
            model.CustomerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if (model.CustomerId>0)
            {
                model.SizeId = SizeId;
                model.Quantity = Quantity;
                model.ProductId = ProductId;
                await _apiService.PostAsync(model, "Cart/CartDataSave");

                return 1;
            }
           return 0;
        }

        public async Task<IActionResult> CartCount()
        {
            var customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if (customerid != 0)
            {
                var jsondata = await _apiService.GetByIdAsync("Cart/Getdata", customerid);
                var data = JsonConvert.DeserializeObject<List<CartModel>>(jsondata);
                return Ok(data.Count());
            }
            else { return Ok(null); }
        }
        public async Task<IActionResult> CartShow()
        {
            var customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            var jsondata = await _apiService.GetByIdAsync("Cart/Getdata", customerid);
            var data = JsonConvert.DeserializeObject<List<CartModel>>(jsondata);
            return PartialView("_Cart",data);
        }
        public async Task<int> DeletefromCart(int Id)
        {
            var data = await _apiService.DeleteAsync("Cart/", Id);
            if (data == true)
            {
                return 1;
            }
            return 0;
        }

    }
}
