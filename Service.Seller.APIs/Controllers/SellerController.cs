using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Category.Service;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Service;
using Service.Shared.DTOs;

namespace Service.Seller.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SellerController : ControllerBase
    {
        private readonly SellerService _SellerService;

        public SellerController(SellerService SellerService)
        {
            _SellerService = SellerService;
        }

        [HttpPut]
        [Route("DetailUpdate")]
        public async Task<IActionResult> detailSave(SellerModel Seller)
        {
            await _SellerService.DetailUpdate(Seller);
            return Ok();
        }
        [HttpPut]
        [Route("ImageUpdate")]
        public async Task<IActionResult> ImageSave([FromForm] JsondataDTOs Seller)
        {
            var data = JsonConvert.DeserializeObject<SellerModel>(Seller.jsondata);
            if(Seller.file!=null)
            {
                await _SellerService.ImageUpdate(data, Seller.file);
            }
            return Ok();
        }

        [HttpPut]
        [Route("PasswordUpdate")]
        public async Task<IActionResult> Update(SellerModel obj)
        {
           await _SellerService.Update(obj);
            return Ok();
        }



        [HttpGet]
        [Route("DataGETbyToken")]
        public async Task<IActionResult> GETData()
        {
            var userClaims = User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id");
            var userId = userIdClaim?.Value;
            var data = await _SellerService.GetbyIdd(Convert.ToInt32(userId));
            return Ok(data);

        }

        //[HttpGet]
        //[Route("DataGetbyid")]
        //public async Task<IActionResult> GetbyId(int Id)
        //{
        //    return Ok(await _SellerService.GetbyIdd(Id));
        //}
        [HttpGet]
        [Route("Graphsdata{Sellerid}")]
        public async Task<IActionResult> graphs(int Sellerid)
        {
            return Ok(await _SellerService.Graphsdata(Sellerid));
        }
    }
}
