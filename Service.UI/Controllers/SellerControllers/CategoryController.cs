using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class CategoryController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public CategoryController(IApiService apiService)
        {
            _apiService = apiService;   
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> GETDATA()
        //{
        //    var data =await _apiService.GetJsonAsync("Category/DataGET");
        //    return View("Index",data);
        //}
        public async Task<IActionResult> TablePartial()
        {
            var SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var jsondata = await _apiService.GetByIdAsync("Category/DataGET", SellerId);
            var data=JsonConvert.DeserializeObject<List<CategoryModel>>(jsondata);
            return PartialView("_TableShow",data);
        }
        public  IActionResult ModelPopShow()
        {         
            return PartialView("_ModelPOP");
        }

        public async Task<IActionResult> DescriptionPop(int id)
        {
            var jsondata = await _apiService.GetByIdAsync("Category/",id);
            var data = JsonConvert.DeserializeObject<CategoryModel>(jsondata);
            return PartialView("_DescriptionPop",data);
        }

        public async Task<int> Savedata(CategoryModel obj)
        {
            obj.SellerId= Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var stream =new MemoryStream();
            var filebyte=stream.ToArray();
            if(obj.ImageUrl != null)
            {
                await obj.ImageUrl.OpenReadStream().CopyToAsync(stream);
                filebyte = stream.ToArray();
            }
            var data =await _apiService.PostWithFileAsync(obj, filebyte,obj.ImageUrl.FileName, "Category/DataSave");         
           if(data == "1")
            {
                return 1;
            }
           return 2;
                              
        }
        public async Task<int> Delete(int Id)
        {
            var data=await _apiService.DeleteAsync("Category/", Id);
            if (data == true)
            {
               return 1;
            }
            return 0;
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var json = await _apiService.GetByIdAsync("Category/", Id);
            var data = JsonConvert.DeserializeObject<CategoryModel>(json);
            return PartialView("_ModelPOP",data);
        }

    }
}
