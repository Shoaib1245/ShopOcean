using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Repository;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Product.Repository
{
    public interface IProductRepository : IGenericRepository<ProductModel, DBContextt>
    {
        Task<List<ProductModel>> GetProductData();
        Task<List<ProductModel>> GetProductData(int SellerId);
        Task<List<ProductModel>> GetProductbySubId(int id);
        Task<List<ProductModel>> GetProductwithPrice();
        Task<ProductModel> GetProductwithProductId(int id);
        Task<ProductModel> ChangePriceWithSizeId(ProductModel obj);
        Task<List<ProductModel>> GetProductbyRange(int id,decimal min,decimal max);
        
    }
}
