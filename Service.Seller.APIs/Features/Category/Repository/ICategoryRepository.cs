using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Product.Repository;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Category.Repository
{
    public interface ICategoryRepository : IGenericRepository<CategoryModel, DBContextt>
    {
        Task<List<CategoryModel>> GEtCategorydatabySellerId(int sellerId);
    }
}
