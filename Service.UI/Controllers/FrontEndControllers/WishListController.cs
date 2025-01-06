using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class WishListController : Controller
    {
        private readonly IApiServiceFrontEnd _apiService;
        public WishListController(IApiServiceFrontEnd apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var Customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if (Customerid != 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("CustomerLogin", "CustomerRegistration");
            }
        }

        
        public async Task<IActionResult> WishListShow()
        {
            var Customerid = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
                var jsondata = await _apiService.GetByIdAsync("WishList/GetWishList", Convert.ToInt32(Customerid));
                var data = JsonConvert.DeserializeObject<List<WishList>>(jsondata);
                return PartialView("_WishList",data);
        }

        public async Task<int> SaveWishList(string DIscount, int ProductId,string Price)
        {
            WishList model = new WishList();
            model.ProductId = ProductId;
            model.Price = Price;
            model.Discount = DIscount;

            model.CustomerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            if (model.CustomerId > 0)
            {
                await _apiService.PostAsync(model, "WishList/SaveWishlist");

                return 1;
            }
            else
            {
               return 0;
            }
        }
       public async Task<int> Delete(int Id)
        {
            var data = await _apiService.DeleteAsync("WishList/", Id);
            if (data == true)
            {
                return 1;
            }
            return 0;
        }
    }
}
