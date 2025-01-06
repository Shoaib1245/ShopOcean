using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class PriceController : SecurityBaseController
    {
        private readonly IApiService _apiService;
        public PriceController(IApiService apiService)
        {
            _apiService = apiService;
        }
        PriceModel pricemodel = new PriceModel();
        public IActionResult AddPrice(int id)
        {
            pricemodel.ProductId = id;
            return PartialView("_AddPrice",pricemodel);
        }
        public async Task<int> Savedata(PriceModel obj)
        {
            await _apiService.PostAsync(obj, "Price/SaveData");           
            return 1;
        }
        public async Task<IActionResult> PriceShow(int id)
        {
            var jsondata = await _apiService.GetByIdAsync("Price/", id);
            var data = JsonConvert.DeserializeObject<List<PriceModel>>(jsondata);
            return PartialView("_PriceShow",data);
        }

    }
}
