using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FrontEnd.APIs.Features.Cart.Core;
using Service.FrontEnd.APIs.Features.Cart.Service;

namespace Service.FrontEnd.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        [Route("CartDataSave")]
        public async Task<IActionResult> Datasave([FromBody] CartModel obj)
        {
           await _cartService.Save(obj);
            return Ok();
        }
        [HttpGet("Getdata{customerId}")]
        public async Task<IActionResult> dataGEt(int customerId)
        {
            
            return Ok(await _cartService.Getdata(customerId));
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<bool> deletedata(int id)
        {
            await _cartService.Delete(id);
            return true;
        }

        //OrderNow
        [HttpPost]
        [Route("OrderNow")]
        public async Task<int> OrderSave([FromBody] OrderPlacedModel obj)
        {
           var d= await _cartService.SaveOrder(obj);
            if (d == 1)
            {
                return 1;
            }
            return 0;
        }
        
        [HttpGet("GetOrderData{Sellerid}")]
        public async Task<IActionResult> OrderdataGEt(int Sellerid)
        {

            return Ok(await _cartService.OrderdataGet(Sellerid));
        }
        
        [HttpPost("StatusChange")]
        public async Task<IActionResult> UpdateOrderStatus(OrderPlacedModel obj)
        {

            return Ok(await _cartService.OrderStatus(Convert.ToInt16(obj.Status),obj.Id));
        }

        [HttpGet("GETInvoiceData")]
        public async Task<IActionResult> GETInvoiceData(int SellerId, int OrderId)
        {
            return Ok(await _cartService.GetInvoiceData(SellerId, OrderId));
        }
    }
}
