using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.Admin.Core;
using Service.Shared.Generic;

namespace Service.Admin.APIs.Features.Admin.Repository
{
    public interface IAdminRepository : IGenericRepository<AdminModel, AdminDBContextt>
    {
        Task<AdminModel> Matching(string Email, string Password);
        Task<AdminModel> GETAdminData(int Id);
    }
}
