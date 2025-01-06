using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Price.Service;
using Service.Seller.APIs.Features.Product.Service;
using Service.Seller.APIs.Features.SubCategory.Service;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly PriceService _PriceService;

        public PriceController(PriceService priceService)
        {
            _PriceService = priceService;
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetdatabyproductId(int Id)
        {
        var data =await _PriceService.GetPricedata(Id);
            return Ok(data);
        }
        [HttpGet]
        [Route("DataGETt")]
        public async Task<IActionResult> GETData()
        {

            return Ok(await _PriceService.GetSizedata());
        }
        
        [HttpPost]
        [Route("SaveData")]
        public async Task<IActionResult> savedata([FromBody] PriceModel obj)
        {
            await _PriceService.dataSave(obj);
            return Ok();
        }
    }
}
