using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Repository;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;
using System.Reflection.Metadata;

namespace Service.Seller.APIs.Features.Product.Repository
{
    public class ProductRepository : GenericRepository<ProductModel, DBContextt>, IProductRepository
    {
        private readonly DbFactory<DBContextt> _dbContext;
        public ProductRepository(DbFactory<DBContextt> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductModel>> GetProductData()
        {

            var data = await SimpleQuery<ProductModel>("select p.*,C.Name,S.SubCategoryName from Tbl_Product p inner join Tbl_Category C on C.Id=p.CategoryId inner join Tbl_SubCategory S on S.Id=p.SubCategoryId");
            return data.ToList();
        }
        public async Task<List<ProductModel>> GetProductData(int SellerId)
        {
            var parameter = new ProductModel
            {
                SellerId = SellerId,

            };

            var data = await GetDatabyId<ProductModel>("select p.*,C.Name,S.SubCategoryName from Tbl_Product p inner join Tbl_Category C on C.Id=p.CategoryId inner join Tbl_SubCategory S on S.Id=p.SubCategoryId where p.SellerId=@SellerId", parameter);
            return data.ToList();
        }


        public async Task<List<ProductModel>> GetProductbySubId(int id)
        {
            var parameter = new ProductModel
            {
                SubCategoryId = id,

            };
            var data = await GetDatabyId<ProductModel>("select prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image,prod.SubCategoryId,IsNull(prod.Discount,0)as Discounts,STRING_AGG(CAST(pric.Price as nvarchar),',') as Prices,STRING_AGG(Siz.SizeName,',')as Sizes from Tbl_Product prod inner join Tbl_Price pric on pric.ProductId=prod.Id inner join Tbl_Size Siz on Siz.Id=pric.SizeId where prod.SubCategoryId=@SubCategoryId Group by prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image,prod.SubCategoryId", parameter);
            return data.ToList();
        }
        public async Task<List<ProductModel>> GetProductwithPrice()
        {
            var data = await SimpleQuery<ProductModel>("select p.Id,p.ProductName,p.Image,IsNull(p.Discount,0) as Discount,ISNULL(p.IsNewArrival,'false') as isnewArrival,STRING_AGG(CAST(price.Price AS VARCHAR), ', ') AS Prices,STRING_AGG(size.sizeName, ', ') AS Sizes from Tbl_Product p inner join Tbl_Price price on price.ProductId=p.Id inner join Tbl_Size Size on Size.Id=price.SizeId GROUP BY  p.Id, p.ProductName,p.Discount,p.isnewArrival,p.Image");
            return data.ToList();
        }
        public async Task<ProductModel> GetProductwithProductId(int id)
        {
            var parameter = new ProductModel
            {
                Id = id,

            };
            var data = await GetSingleDatabyId<ProductModel>("select prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image,prod.Description,prod.SubCategoryId,IsNull(prod.Discount,0)as Discounts,STRING_AGG(CAST(pric.Price as nvarchar),',') as Prices,STRING_AGG(CAST(Siz.Id AS nvarchar) + ':' + Siz.SizeName, ',') as Sizes from Tbl_Product prod inner join Tbl_Price pric on pric.ProductId=prod.Id inner join Tbl_Size Siz on Siz.Id=pric.SizeId where prod.Id=@Id Group by prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image,prod.Description,prod.SubCategoryId", parameter); 
            return data;
        }
        public async Task<ProductModel> ChangePriceWithSizeId(ProductModel obj)
        {
            var parameter = new ProductModel
            {
                Id = obj.Id,
                SizeId = obj.SizeId,

            };
            var data = await GetSingleDatabyId<ProductModel>("SELECT p.Id,pr.SizeId,ISNULL(p.Discount,0) as Discount,ISNULL(pr.Price,0) as Prices FROM Tbl_Product p JOIN Tbl_Price pr ON p.Id = pr.ProductId WHERE p.Id = @Id AND pr.SizeId = @SizeId", parameter);
            
            return data;
        }
        public async Task<List<ProductModel>> GetProductbyRange(int id, decimal min, decimal max)
        {
     
            var data = await SimpleQuery<ProductModel>($"select prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image,STRING_AGG(CAST(pric.Price as nvarchar),',') as Prices,STRING_AGG(Siz.SizeName,',')as Sizes from Tbl_Product prod inner join Tbl_Price pric on pric.ProductId=prod.Id inner join Tbl_Size Siz on Siz.Id=pric.SizeId where pric.Price>{min} and pric.Price<={max} and SubCategoryId={id} Group by prod.Id,prod.ProductName,prod.Discount,prod.IsNewArrival,prod.Image");

            return data.ToList();
        }
        
    }
}
