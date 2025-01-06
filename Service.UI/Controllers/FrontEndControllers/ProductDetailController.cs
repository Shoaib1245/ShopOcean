using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class ProductDetailController : Controller
    {
        private readonly IApiServiceFrontEnd _apIServiceFrontEnd;
        private readonly IApiService _apiService;

        AlldataModel alldata=new AlldataModel();

        public ProductDetailController(IApiServiceFrontEnd apIServiceFrontEnd, IApiService apiService)
        {
            _apIServiceFrontEnd = apIServiceFrontEnd;
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext.Session.SetString("ProductId",id.ToString());
           var proddata = await _apiService.GetByIdAsync("Product/GetProducttwithProdId" ,Convert.ToInt32(id));
           alldata.ProductDetail = JsonConvert.DeserializeObject<ProductModel>(proddata);
            return View(alldata);
        }

        public async Task<IActionResult> ProductShowbySizeId(int productId, int sizeId)
        {
            ProductModel obj = new ProductModel();
            obj.Id = productId;
            obj.SizeId = sizeId;

            var Proddata = await _apiService.PostAsync(obj,"Product/GetProducttwithProdIdd");
            var data = JsonConvert.DeserializeObject<ProductModel>(Proddata);

            return PartialView("_PriceShowbySizeId", data);
        }
        
        public async Task<IActionResult> ShowReletedProduct(int SubId)
        {
            var proddata = await _apiService.GetByIdAsync("Product/GetProductbysubId", SubId);
           var data = JsonConvert.DeserializeObject<List<ProductModel>>(proddata);
            return PartialView("_RelatedProducts", data);
        }
        
        public async Task<int> CommentsSave(ReviewAndComments obj)
        {

            obj.CustomerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
            obj.ProductId = Convert.ToInt16(HttpContext.Session.GetString("ProductId"));
            obj.Created = DateTime.Now;
            obj.status = 1;
            if (obj.CustomerId != 0)
            {
                var stream = new MemoryStream();
                var filebyte = stream.ToArray();
                if (obj.ImageUrl != null)
                {
                    await obj.ImageUrl.OpenReadStream().CopyToAsync(stream);
                    filebyte = stream.ToArray();
                }

                var data = await _apIServiceFrontEnd.PostWithFileAsync(obj, filebyte, obj.ImageUrl.FileName, "Review_Comments/CommentsSave");
                if (data == "1")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 2;
            }


        }
        public async Task<IActionResult> ReviewPage(ReviewAndComments obj)
        {
            return PartialView("_ReviewPage");
        }
        public async Task<IActionResult> ReviewShow()
        {
            var data = await _apIServiceFrontEnd.GetJsonAsync("Review_Comments/Getdata");
            alldata.ReviewAndComments = JsonConvert.DeserializeObject<List<ReviewAndComments>>(data);
            return PartialView("_ReviewShow",alldata);
        }
    }
}
