using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class SellerProfileController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public SellerProfileController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> ProfilePartial()
        {
            var Token= HttpContext.Session.GetString("Token");
           var jsondata = await _apiService.GetByIdToken("Seller/DataGETbyToken", Token ?? "");
            var data=JsonConvert.DeserializeObject<SellerModel>(jsondata);
            HttpContext.Session.SetString("SellerId", Convert.ToString(data.Id)); //Session for Detail Update , Password  and Image Update
            HttpContext.Session.SetString("Password", data.Password ?? "");
            HttpContext.Session.SetString("SellerName", data.Name ?? "");
            return PartialView("_SellerProfile",data);
        }
        public async Task<IActionResult> SellerDetail()
        {
            var Token = HttpContext.Session.GetString("Token");
            var jsondata = await _apiService.GetByIdToken("Seller/DataGETbyToken", Token ?? "");
            var data = JsonConvert.DeserializeObject<SellerModel>(jsondata);
           
            return PartialView("_SellerDetail", data);
        }
        public IActionResult SellerImage()
        {
            return PartialView("_SellerImage");
        }
        public IActionResult SellerPassword()
        {
            return PartialView("_PasswordUpdate");
        }
        public async Task<int> PasswordChange(string Password)
        {
            SellerModel model = new SellerModel();
            model.Password = Password;
            model.Id = Convert.ToInt32(HttpContext.Session.GetString("SellerId")); 
            var Token = HttpContext.Session.GetString("Token");
            var data = await _apiService.PutAsync(model, "Seller/PasswordUpdate", Token ?? "");
            if(data == true)
            {
                return 1;
            }
            else
            {
               return 0;
            }
         
        }

        
           public async Task<int> DetailUpdate(SellerModel model)
            {
            var Token = HttpContext.Session.GetString("Token");
            var data = await _apiService.PutAsync(model, "Seller/DetailUpdate", Token ?? "");
            if (data == true)
            {
                return 1;
            }
            else
            {

                return 0;
            }

        }
        
           public async Task<int> ImageSave(SellerModel model)
            {
            model.Id = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var Token = HttpContext.Session.GetString("Token");
            var stream = new MemoryStream();
            var filebyte = stream.ToArray();
            if (model.ImageUrl != null)
            {
                await model.ImageUrl.OpenReadStream().CopyToAsync(stream);
                filebyte = stream.ToArray();
            }
            var data = await _apiService.PuttWithFileAsync(model, filebyte, model.ImageUrl.FileName, "Seller/ImageUpdate", Token ?? "");
                return 1;


        }
    }
}
