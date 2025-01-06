using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Seller.APIs.Features.Category.Service;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Product.Service;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Seller.APIs.Features.SubCategory.Service;
using Service.Shared.DTOs;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _ProductService;

        public ProductController(ProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpPost]
        [Route("DataSave")]
        public async Task<int> Save([FromForm] ProductJsondata product)
        {
            var data = JsonConvert.DeserializeObject<ProductModel>(product.jsondata);
            var d = await _ProductService.Save(data, product.file);
            if (d == 1)
            {
                return 1;
            }
            return 2;
        }
        //[HttpGet]
        //[Route("Categorydata{Id}")]
        //public async Task<IActionResult> Categorydata(int Id)
        //{
        //    var data = await _ProductService.GetCategorydata(Id);
        //    return Ok(data);
        //}

        [HttpPut]
        [Route("DataUpdate")]
        public async Task<IActionResult> Update(int Id)
        {
           await _ProductService.Update(Id);
            return Ok();
        }

        [HttpDelete]
        [Route("DataDelete{Id}")]
        public async Task<IActionResult> deletedata(int Id)
        {
            await _ProductService.Delete(Id);
            return Ok();
        }

        [HttpGet]
        [Route("DataGET")]
        public async Task<IActionResult> GETData()
        {
            return Ok(await _ProductService.Getdata());
        }
        [HttpGet]
        [Route("DataGET{SellerId}")]
        public async Task<IActionResult> GETData(int SellerId)
        {
            return Ok(await _ProductService.Getdata(SellerId));
        }

        [HttpGet]
        [Route("DataProductwithPrice")]
        public async Task<IActionResult> getdatawithprice()
        {
            return Ok(await _ProductService.GetProducttwithprice());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            return Ok(await _ProductService.GetbyIdd(Id));
        }
        [HttpGet]
        [Route("GetProductbysubId{Id}")]
        public async Task<IActionResult> GetProductbysubId(int Id)
        {
            return Ok(await _ProductService.GetProductbySubId(Id));
        }       
        [HttpGet]
        [Route("GetProducttwithProdId{Id}")]
        public async Task<IActionResult> GetProducttwithProdIdd(int Id)
        {
            return Ok(await _ProductService.GetProducttwithProdId(Id));
        }
        [HttpPost]
        [Route("GetProducttwithProdIdd")]
        public async Task<IActionResult> GetPricetwithSizeId([FromBody] ProductModel obj)
        {
            return Ok(await _ProductService.getPricebySizeId(obj));
        }
        [HttpGet("GetProductByPriceRange")]
        public async Task<IActionResult> GetProductByPriceRange(int id, decimal minPrice, decimal maxPrice)
        {
            var data = await _ProductService.GetProductbyRange(id,minPrice,maxPrice);
            return Ok(data);
        }
    }

}
