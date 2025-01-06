using Microsoft.EntityFrameworkCore;
using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.SubCategory.Repository
{
    public class SubCategoryRepository : GenericRepository<SubCategoryModel, DBContextt> , ISubCategoryRepository
    {
        private readonly DBContextt _dbContext;
        public SubCategoryRepository(DbFactory<DBContextt> dbContext,DBContextt dBContextt) : base(dbContext)
        {
            _dbContext = dBContextt;
        }

        public async Task<List<SubCategoryModel>> GetCategoryDatabyId(int id)
        {
            var parameter = new SubCategoryModel
            {
                CategoryId = id,
            };
           var data= await GetDatabyId<SubCategoryModel>("select * from Tbl_SubCategory where CategoryId=@CategoryId", parameter);
       return data.ToList();
        }
        public async Task<List<SubCategoryModel>> GEtSubCategorybySellerId(int sellerId)
        {
            var data = await _dbContext.Tbl_SubCategory.Where(a => a.SellerId == sellerId).ToListAsync();
            return data;
        }
    }
}
