using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class SubCategoryController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public SubCategoryController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Categorydata(int catId)
        {
            var jsondata = await _apiService.GetByIdAsync("SubCategory/Categorydata",catId);
           var data = JsonConvert.DeserializeObject<List<SubCategoryModel>>(jsondata);

            return Ok(data);
        }
        public async Task<IActionResult> TablePartial()
       {
            var SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var jsondata = await _apiService.GetByIdAsync("SubCategory/DataGET",SellerId);
            var data = JsonConvert.DeserializeObject<List<SubCategoryModel>>(jsondata);
            return PartialView("_TableShow", data);
        }
        public IActionResult ModelPopShow()
        {
            return PartialView("_SubCatModelPOP");
        }



        public async Task<int> Savedata(SubCategoryModel obj)
        {
            obj.SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var stream = new MemoryStream();
            var filebyte = stream.ToArray();
            if (obj.ImageUrl != null)
            {
                await obj.ImageUrl.OpenReadStream().CopyToAsync(stream);
                filebyte = stream.ToArray();
            }
            var data = await _apiService.PostWithFileAsync(obj, filebyte, obj.ImageUrl.FileName, "SubCategory/DataSave");
            if (data == "1")
            {
                return 1;
            }
            return 2;
        }
        public async Task<int> Delete(int Id)
        {
            var data = await _apiService.DeleteAsync("SubCategory/", Id);
            if (data == true)
            {
                return 1;
            }
            return 0;
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var json = await _apiService.GetByIdAsync("SubCategory/", Id);
            var data = JsonConvert.DeserializeObject<SubCategoryModel>(json);
            return PartialView("_SubCatModelPOP", data);
        }
    }
}
