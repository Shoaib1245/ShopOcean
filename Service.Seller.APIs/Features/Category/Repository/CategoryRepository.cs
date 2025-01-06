using Microsoft.EntityFrameworkCore;
using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Product.Repository;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Category.Repository
{
    public class CategoryRepository : GenericRepository<CategoryModel, DBContextt>, ICategoryRepository
    {
        private readonly DBContextt _dbContext;
        public CategoryRepository(DbFactory<DBContextt> dbContext,DBContextt dBContextt) : base(dbContext)
        {
            _dbContext = dBContextt;
        }
        public async Task<List<CategoryModel>> GEtCategorydatabySellerId(int sellerId)
        {
            var data= await _dbContext.Tbl_Category.Where(a => a.SellerId == sellerId).ToListAsync();
            return data;
        }
    }
}
