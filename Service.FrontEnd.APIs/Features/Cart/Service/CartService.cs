using Service.FrontEnd.APIs.Features.Cart.Core;
using Service.FrontEnd.APIs.Features.Cart.Repository;
using Service.FrontEnd.APIs.Features.WishList.Core;
using Service.FrontEnd.APIs.Utility;

namespace Service.FrontEnd.APIs.Features.Cart.Service
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;

        }

        public async Task<int> Save(CartModel entity)
        {
            if (entity.Id > 0)
            {
                _cartRepository.Update(entity);
                return 1;
            }
            else
            {
                await _cartRepository.AddCart(entity);
                return 2;
            }
        }
        public async Task<List<CartModel>> Getdata(int Id)
        {
            return await _cartRepository.GetbyCustomerId(Id);
        }
        

        public async Task<bool> Delete(int Id)
        {
            return await _cartRepository.Deletedata(Id);
        }
        //SAVEORDER
        public async Task<int> SaveOrder(OrderPlacedModel obj)
        {
            return await _cartRepository.SaveData(obj);
        }
        
        public async Task<List<OrderDataModel>> OrderdataGet(int Sellerid)
        {
            return await _cartRepository.GetOrderdata(Sellerid);
        }
        public async Task<int> OrderStatus(int StatusId, int orderid)
        {
            return await _cartRepository.UpdateOrderStatus(StatusId,orderid);
        }
        public async Task<List<OrderDataModel>> GetInvoiceData(int Sellerid,int OrderId)
        {
            return await _cartRepository.GetInvoice(Sellerid,OrderId);
        }

        //WishList
        public async Task<int> SaveWishlist(WishListModel obj)
        {
            return await _cartRepository.SaveWishlist(obj);
        }

        public async Task<List<ExtraWistlistdataModel>> WishlistGet(int CustomerID)
        {
            return await _cartRepository.GetWishList(CustomerID);
        }
        public async Task<bool> WishlistDelete(int Id)
        {
            return await _cartRepository.WishlistDeleted(Id);
        }
    }
}
