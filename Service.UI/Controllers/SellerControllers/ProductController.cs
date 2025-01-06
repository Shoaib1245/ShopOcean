using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class ProductController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public ProductController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var json = await _apiService.GetByIdAsync("Product/",Convert.ToInt32(id));
            var data = JsonConvert.DeserializeObject<ProductModel>(json);
            return View(data);
        }

        public async Task<int> Savedata(ProductModel obj)
        {
            obj.SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            if (obj.ImageUrl.Count == 4)
            {
                var filebyte = new List<byte[]>();
                var fileNames = new List<string>();
                if (obj.ImageUrl != null)
                {
                    for (int i = 0; i < obj.ImageUrl.Count; i++)
                    {
                        var stream = new MemoryStream();
                        await obj.ImageUrl[i].OpenReadStream().CopyToAsync(stream);
                        filebyte.Add(stream.ToArray());
                        fileNames.Add(obj.ImageUrl[i].FileName);
                    }
                }
                var data = await _apiService.PostWithMultiFilesAsync(obj, filebyte.ToList(), fileNames, "Product/DataSave");
                if (data == "1")
                {
                    return 1;
                }
                return 2;
            }
            else return 0;
        }
        public IActionResult TableData()     
         {

            return View();
        }
        public async Task<IActionResult> TableDataPartial()
        {
            var SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var jsondata = await _apiService.GetByIdAsync("Product/DataGET",SellerId);
            var data = JsonConvert.DeserializeObject<List<ProductModel>>(jsondata);
            return PartialView("_TableDataShow", data);
        }


        public async Task<IActionResult> DescriptionPop(int id)
        {
            var jsondata = await _apiService.GetByIdAsync("Product/", id);
            var data = JsonConvert.DeserializeObject<ProductModel>(jsondata);
            return PartialView("_Description", data);
        }

        public async Task<int> DeleteProduct(int Id)
        {
            await _apiService.DeleteAsync("Product/DataDelete", Id);
            return 1;
        }
    }
}
