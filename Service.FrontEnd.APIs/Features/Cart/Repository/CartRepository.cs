using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Cart.Core;
using Service.FrontEnd.APIs.Features.WishList.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Cart.Repository
{
    public class CartRepository : GenericRepository<CartModel, FrontEndDBContext>, ICartRepository
    {

        private readonly FrontEndDBContext _dbContext;
        public CartRepository(DbFactory<FrontEndDBContext> dbFactory, FrontEndDBContext dbContext) : base(dbFactory)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddCart(CartModel model)
        {
            var parameters = new
            {
                ProductId = model.ProductId,
                SizeId = model.SizeId,
                Quantity = model.Quantity,
                CustomerId = model.CustomerId,
                OrderId = model.OrderId,
            };

            var isSuccess = await ExecuteStoreProcedureQuery("Sp_cart", parameters);
            return isSuccess;
        }
        public async Task<List<CartModel>> GetbyCustomerId(int Customerid)
        {
            var parameter = new CartModel
            {
                CustomerId = Customerid
            };
           var data = await GetDatabyId<CartModel>("Select Cart.*,prod.ProductName,prod.Image from Tbl_Cart Cart inner join Tbl_Product prod on prod.Id=cart.ProductId where CustomerId=@CustomerId", parameter);
            return data.ToList();
        }

        public async Task<bool> Deletedata(int Id)
        {
            //I have used this for Delete Cart
            var parameter=new CartModel { ProductId = Id };
             await ProfileDataUpdate<CartModel>("delete from Tbl_Cart where ProductId=@ProductId", parameter);
            return true;
        }


        //public async Task<int> SaveData(OrderPlacedModel model)
        //{
        //    await SaveData<OrderPlacedModel>("Tbl_OrderPlaced", model, "Id");
        //    return 1;
        //}




        //OrderData
        public async Task<int> SaveData(OrderPlacedModel model)
        {
            await ExecuteStoreProcedureQuery("Sp_ADDCartOrder", model);
            return 1;
        }

        
        public async Task<List<OrderDataModel>> GetOrderdata(int Sellerid)
        {
            var parameter = new
            {
                SellerId = Sellerid
            };
           var data= await GetDatabyId<OrderDataModel>("select Cart.*,orders.*,orders.Id as OrderId, customer.FirstName+LastName as CustomerName,customer.Image from Tbl_Cart cart inner join Tbl_Customer customer on customer.Id=cart.CustomerId inner join Tbl_OrderPlaced orders on orders.Id=cart.OrderId inner join Tbl_Product Prod on Cart.ProductId=Prod.Id where prod.SellerId=@SellerId", parameter);
            return data.ToList();
        }
       
        public async Task<int> UpdateOrderStatus(int StatusId,int OrderId)
        {
            var parameter = new
            {
                Status = StatusId,
                Id = OrderId
            };
            return  await ProfileDataUpdate("Update Tbl_OrderPlaced set Status=@Status where Id=@Id", parameter);
             
        }
        public async Task<List<OrderDataModel>> GetInvoice(int Sellerid,int OrderId)
        {
            var parameter = new
            {
                SellerId = Sellerid,
                OrderId = OrderId
            };
            var data = await GetDatabyId<OrderDataModel>("select * from Tbl_Cart Cart inner join Tbl_Product Prod on Cart.ProductId=Prod.Id where Prod.SellerId= @SellerId and Cart.OrderId= @OrderId", parameter);
            return data.ToList();
        }
        //WushList

        public async Task<int> SaveWishlist(WishListModel model)
        {

            await SaveData<WishListModel>("Tbl_WishList", model, "Id");
            return 1;
        }
        public async Task<List<ExtraWistlistdataModel>> GetWishList(int CustomerId)
        {
            var parameter = new
            {
                CustomerId = CustomerId
            };
            var data = await GetDatabyId<ExtraWistlistdataModel>("select Wishlist.*,prod.ProductName,prod.Image from Tbl_WishList Wishlist inner join Tbl_Product prod on Wishlist.ProductId=prod.Id where CustomerId=@CustomerId", parameter);
            return data.ToList();
        }
        public async Task<bool> WishlistDeleted(int Id)
        {
            //I have used this for Delete Wishlist
            var parameter = new WishListModel { Id = Id };
            await ProfileDataUpdate<WishListModel>("delete from Tbl_WishList where Id=@Id", parameter);
            return true;
        }
    }
}
