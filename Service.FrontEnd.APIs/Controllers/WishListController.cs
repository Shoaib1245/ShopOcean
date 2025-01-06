using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FrontEnd.APIs.Features.Cart.Service;
using Service.FrontEnd.APIs.Features.WishList.Core;

namespace Service.FrontEnd.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly CartService _cartService;
        public WishListController(CartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        [Route("SaveWishlist")]
        public async Task<IActionResult> SaveWishlist([FromBody] WishListModel obj)
        {
            await _cartService.SaveWishlist(obj);
            return Ok();
        }
        [HttpGet]
        [Route("GetWishList{CustomerId}")]
        public async Task<IActionResult> GetWishList(int CustomerId)
        {
            var data=await _cartService.WishlistGet(CustomerId);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> deletedata(int id)
        {
            await _cartService.WishlistDelete(id);
            return Ok();
        }
    }
}
