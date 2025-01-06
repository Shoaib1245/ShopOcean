using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Category.Service;
using Service.Shared.DTOs;
using System.Text.Json.Serialization;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("DataSave")]
        public async Task<int> Save([FromForm] JsondataDTOs category)
        { 
           var data= JsonConvert.DeserializeObject<CategoryModel>(category.jsondata);
         var d =  await _categoryService.Save(data,category.file);
            if(d==1)
            {
                return 1;
            }
            return 2;

       
        }

        //[HttpPut]
        //[Route("DataUpdate")]
        //public async Task<IActionResult> Update(JsondataDTOs category)
        //{
        //    var data = JsonConvert.DeserializeObject<CategoryModel>(category.jsondata);
        //    await _categoryService.Update(data, category.file);
        //    return Ok();
        //}

        [HttpDelete]
        [Route("{Id}")]
        public async Task<bool> deletedata (int id)
        {
             await _categoryService.Delete(id);
            return true;
        }

        [HttpGet]
        [Route("DataGET{SellerId}")]
        public async Task<List<CategoryModel>> GETData(int SellerId)
        {
            return await _categoryService.Getdata(SellerId);
        }
        [HttpGet]
        [Route("DataGET")]
        public async Task<List<CategoryModel>> GETData()
        {
            return await _categoryService.Getdata();
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            return Ok(await _categoryService.GetbyIdd(Id));
        }
    }
}
