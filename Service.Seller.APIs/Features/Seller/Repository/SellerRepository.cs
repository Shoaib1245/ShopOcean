using Microsoft.EntityFrameworkCore;
using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.DTOs;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Seller.Repository
{
    public class SellerRepository: GenericRepository<SellerModel, DBContextt>, ISellerRepository
    {

        private readonly DBContextt _database;
        public SellerRepository(DbFactory<DBContextt> dbContext, DBContextt database) : base(dbContext)
        {
           _database = database;
        }
        public async Task<SellerModel> SellerLogin(SellerLoginDTO obj)
        {
           var data=await _database.Tbl_Seller.Where(a=>a.Email == obj.Email && a.Password==obj.Password).FirstOrDefaultAsync();
                return data;
        }
       public async Task<SellerModel> CheckEmail(SellerLoginDTO obj)
        {
        return await _database.Tbl_Seller.Where(a=>a.Email==obj.Email).FirstOrDefaultAsync();
        }



        public async Task<GraphsDTOs> GetGraphsdata(int Sellerid)
        {
            var parameters = new
            {
                SellerId = Sellerid,

            };
            var data =  await StoreProcedureQuery("Sp_Graphs",parameters);
          return data;
        }
    }
}
