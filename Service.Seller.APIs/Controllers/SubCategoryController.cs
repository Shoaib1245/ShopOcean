using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Seller.APIs.Features.Category.Service;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Service;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Seller.APIs.Features.SubCategory.Repository;
using Service.Seller.APIs.Features.SubCategory.Service;
using Service.Shared.DTOs;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryService _SubCategoryService;

        public SubCategoryController(SubCategoryService SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }

        [HttpPost]
        [Route("DataSave")]
        public async Task<int> Save([FromForm] JsondataDTOs subcategory)
        {
            var data = JsonConvert.DeserializeObject<SubCategoryModel>(subcategory.jsondata);
          var d=  await _SubCategoryService.Save(data, subcategory.file);
            if (d == 1)
            {
                return 1;
            }
            return 2;

        }

       [HttpGet]
        [Route("Categorydata{Id}")]
        public async Task<IActionResult> Categorydata(int Id)
        {
           var data= await _SubCategoryService.GetCategorydata(Id);
            return Ok(data);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> deletedata(int id)
        {
            await _SubCategoryService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("DataGET{SellerId}")]
        public async Task<IActionResult> GETData(int SellerId)
        {
            return Ok(await _SubCategoryService.Getdata(SellerId));
        }
        [HttpGet]
        [Route("DataGET")]
        public async Task<IActionResult> GETData()
        {
            return Ok(await _SubCategoryService.Getdata());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            return Ok(await _SubCategoryService.GetbyIdd(Id));
        }
    }
}
