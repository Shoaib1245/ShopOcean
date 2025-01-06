using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.SellerProfileStatus.Core;
using Service.Shared.DTOs;
using Service.Shared.Generic;

namespace Service.Admin.APIs.Features.SellerProfileStatus.Repository
{
    public interface ISellerProfileStatusRepository:IGenericRepository<SellerModel,AdminDBContextt>
    {
        Task<bool> SellerStatusUpdate(SellerModel obj);
        Task<bool> SellerDeleted(int Id);
        Task<AdminGraphsDTOs> GetGraphsdata();
    }
}
