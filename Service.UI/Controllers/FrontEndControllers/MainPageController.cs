using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class MainPageController : Controller
    {
        AlldataModel Alldatamodel = new AlldataModel();
        private readonly IApiService _apiService;

        public MainPageController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var Catdata = await _apiService.GetJsonAsync("Category/DataGET");
            Alldatamodel.CategoryData = JsonConvert.DeserializeObject<List<CategoryModel>>(Catdata);

            var SubCatdata = await _apiService.GetJsonAsync("SubCategory/DataGET");
            Alldatamodel.SubCategoryData = JsonConvert.DeserializeObject<List<SubCategoryModel>>(SubCatdata);

            var proddata = await _apiService.GetJsonAsync("Product/DataProductwithPrice");
            Alldatamodel.ProductData = JsonConvert.DeserializeObject<List<ProductModel>>(proddata);

            return View(Alldatamodel);
        }
        public async Task<IActionResult> GEtSubcategorybyId(int id)
        {
            var SubCatdata = await _apiService.GetByIdAsync("SubCategory/Categorydata", id);
            var SubCategory = JsonConvert.DeserializeObject<List<SubCategoryModel>>(SubCatdata);
            return PartialView("_Getsubcategory", SubCategory);
        }
        
    }
}
