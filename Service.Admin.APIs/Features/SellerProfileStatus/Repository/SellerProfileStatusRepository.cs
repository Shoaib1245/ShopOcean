using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.SellerProfileStatus.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.DTOs;
using Service.Shared.Generic;

namespace Service.Admin.APIs.Features.SellerProfileStatus.Repository
{
    public class SellerProfileStatusRepository : GenericRepository<SellerModel, AdminDBContextt>, ISellerProfileStatusRepository
    {
        private readonly AdminDBContextt _adminDBContextt;
        public SellerProfileStatusRepository(DbFactory<AdminDBContextt> dbFactory, AdminDBContextt adminDBContextt) : base(dbFactory)
        {
            _adminDBContextt = adminDBContextt;
        }
       public async Task<bool> SellerStatusUpdate(SellerModel obj)
        {

             await ProfileDataUpdate<SellerModel>("Update Tbl_Seller set Status=@Status where Id=@Id",obj);
            return true;
        }
        public async Task<bool> SellerDeleted(int Id)
        {
            //I have used this for Delete Wishlist
            var parameter = new SellerModel { Id = Id };
            await ProfileDataUpdate<SellerModel>("delete from Tbl_Seller where Id=@Id", parameter);
            return true;
        }
        public async Task<AdminGraphsDTOs> GetGraphsdata()
        {

            var data = await StoreProceduredata("Sp_AdminGraphs");
            return data;
        }
    }
}
