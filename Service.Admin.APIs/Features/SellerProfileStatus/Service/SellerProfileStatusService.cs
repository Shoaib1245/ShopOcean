using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.SellerProfileStatus.Core;
using Service.Admin.APIs.Features.SellerProfileStatus.Repository;
using Service.Shared.DTOs;

namespace Service.Admin.APIs.Features.SellerProfileStatus.Service
{
    public class SellerProfileStatusService
    {
        private readonly ISellerProfileStatusRepository _Sellerstatus;
        public SellerProfileStatusService(ISellerProfileStatusRepository Sellerstatus)
        {
            _Sellerstatus = Sellerstatus;
        }
        public async Task<List<SellerModel>> GetAllSellerdata()
        {
          var data=  await _Sellerstatus.Getdata();
            return data;
        }
        
        public async Task<bool> StatusUpdate(SellerModel obj)
        {
            var data = await _Sellerstatus.SellerStatusUpdate(obj);
            return data;
        }
        
       public async Task<bool> SellerAccountDelete(int Id)
        {
            return await _Sellerstatus.SellerDeleted(Id);
        }
        public async Task<AdminGraphsDTOs> Graphsdata()
        {
            return await _Sellerstatus.GetGraphsdata();
        }
    }
}
