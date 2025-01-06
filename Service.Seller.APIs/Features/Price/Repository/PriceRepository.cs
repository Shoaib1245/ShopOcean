using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Size.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Price.Repository
{
    public class PriceRepository : GenericRepository<PriceModel, DBContextt>, IPriceRepository
    {
       // private readonly DBContextt _dbContextt;
        public PriceRepository(DbFactory<DBContextt> dbFactory) : base(dbFactory)
        {
           // _dbContextt = dBContextt;
        }
        public async Task<List<PriceModel>> GetPriceDatabyId(int id)
        {
            var parameter = new PriceModel
            {
                ProductId = id,
            };
            var data = await GetDatabyId<PriceModel>("select S.SizeName,p.* from Tbl_Price p inner join Tbl_Size S on s.Id=p.SizeId where ProductId=@ProductId", parameter);
            return data.ToList();
        }
        public async Task<List<SizeModel>> GetSizeData()
        {
            var data = await SimpleQuery<SizeModel>("select * from Tbl_Size");
            return data.ToList();
        }
    }
}
