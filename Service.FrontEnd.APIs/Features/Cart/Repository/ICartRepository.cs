using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Cart.Core;
using Service.FrontEnd.APIs.Features.WishList.Core;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Cart.Repository
{
    public interface ICartRepository : IGenericRepository<CartModel, FrontEndDBContext>
    {
        Task<bool> AddCart(CartModel model);
        Task<bool> Deletedata(int Id);
        Task<List<CartModel>> GetbyCustomerId(int Customerid);
        
        //OrderData
        Task<int> SaveData(OrderPlacedModel model);
        Task<List<OrderDataModel>> GetOrderdata(int Sellerid);
        Task<int> UpdateOrderStatus(int StatusId, int OrderId);
        Task<List<OrderDataModel>> GetInvoice(int Sellerid, int OrderId);
        //WishList
        Task<int> SaveWishlist(WishListModel model);
        Task<List<ExtraWistlistdataModel>> GetWishList(int CustomerId);
        Task<bool> WishlistDeleted(int Id);
    }
}