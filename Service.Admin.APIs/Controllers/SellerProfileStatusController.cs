using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Admin.APIs.Features.SellerProfileStatus.Core;
using Service.Admin.APIs.Features.SellerProfileStatus.Service;

namespace Service.Admin.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerProfileStatusController : ControllerBase
    {
        private readonly SellerProfileStatusService _sellerProfileStatusService;
        public SellerProfileStatusController(SellerProfileStatusService sellerProfileStatusService)
        {
            _sellerProfileStatusService = sellerProfileStatusService;
        }
        [HttpGet]
        [Route("GetSellerData")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sellerProfileStatusService.GetAllSellerdata());
        }
        [HttpPost]
        [Route("SellerStatusUpdate")]
        public async Task<IActionResult> STatusUpdate([FromBody] SellerModel obj)
        {
            return Ok(await _sellerProfileStatusService.StatusUpdate(obj));
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> deletedata(int id)
        {
            await _sellerProfileStatusService.SellerAccountDelete(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetGRAphs")]
        public async Task<IActionResult> Getdata()
        {
            return Ok(await _sellerProfileStatusService.Graphsdata());
        }
    }
}
