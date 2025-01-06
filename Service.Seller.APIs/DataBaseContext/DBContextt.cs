using Microsoft.EntityFrameworkCore;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Size.Core;
using Service.Seller.APIs.Features.SubCategory.Core;

namespace Service.Seller.APIs.DataBaseContext
{
    public class DBContextt : DbContext
    {
        public DBContextt(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SellerModel> Tbl_Seller { get; set; } = null!;
        public DbSet<CategoryModel> Tbl_Category { get; set; } = null!;
        public DbSet<SubCategoryModel> Tbl_SubCategory { get; set; } = null!;
        public DbSet<ProductModel> Tbl_Product { get; set; } = null!;
        public DbSet<PriceModel> Tbl_Price { get; set; } = null!;
        public DbSet<SizeModel> Tbl_Size { get; set; } = null!;

    }
}
