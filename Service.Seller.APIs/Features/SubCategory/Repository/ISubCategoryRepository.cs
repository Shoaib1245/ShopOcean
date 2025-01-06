using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.SubCategory.Repository
{
    public interface ISubCategoryRepository : IGenericRepository<SubCategoryModel, DBContextt>
    {

        Task<List<SubCategoryModel>> GetCategoryDatabyId(int id);
        Task<List<SubCategoryModel>> GEtSubCategorybySellerId(int sellerId);
    }
}
