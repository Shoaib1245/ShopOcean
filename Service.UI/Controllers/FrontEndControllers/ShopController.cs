using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.Utilize;
using System;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class ShopController : Controller
    {
        AlldataModel Alldatamodel = new AlldataModel();
        private readonly IApiService _apiService;

        public ShopController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int? Id)
        {
            ViewBag.SubCategoryId = Id; // This ViewBag is used to pass the selected SubCategory ID, so the products on the next page will be updated accordingly.
            var Catdata = await _apiService.GetJsonAsync("Category/DataGET");
            Alldatamodel.CategoryData = JsonConvert.DeserializeObject<List<CategoryModel>>(Catdata);

            var SubCatdata = await _apiService.GetJsonAsync("SubCategory/DataGET");
            Alldatamodel.SubCategoryData = JsonConvert.DeserializeObject<List<SubCategoryModel>>(SubCatdata);
            return View(Alldatamodel);
        }      
         public async Task<IActionResult> ProductShowBySubId(int id)
         {
            TempData["SubId"] = id;  ///This sub id is use for different things
            var proddata = await _apiService.GetByIdAsync("Product/GetProductbysubId",id);
            Alldatamodel.ProductData = JsonConvert.DeserializeObject<List<ProductModel>>(proddata);
            return PartialView("_ProductItems", Alldatamodel);
         }

        
         public async Task<IActionResult> GetProductbyPriceRange(int id,string Range)
        {
            var Subcategoryid = Convert.ToInt32(TempData["SubId"]);
            int subId = Subcategoryid != 0 ? Subcategoryid : id;// when id is equall to 0 then give the temp value other wise give the id value

            var minPrice = Range.Split('-')[0].Replace("$", "").Trim();
            var maxPrice = Range.Split('-')[1].Replace("$", "").Trim();


            string apiEndPoint = $"Product/GetProductByPriceRange?id={subId}&minPrice={minPrice}&maxPrice={maxPrice}";
            var jsondata =await _apiService.GetJsonAsync(apiEndPoint);
            Alldatamodel.ProductData = JsonConvert.DeserializeObject<List<ProductModel>>(jsondata);
            return PartialView("_ProductItems", Alldatamodel);
        }

    }
}
