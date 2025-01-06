using Microsoft.EntityFrameworkCore;
using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.Admin.Core;
using Service.Admin.APIs.Features.SellerProfileStatus.Repository;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.Admin.APIs.Features.Admin.Repository
{
    public class AdminRepository : GenericRepository<AdminModel, AdminDBContextt>, IAdminRepository
    {
        private readonly AdminDBContextt _dbContextt;
        public AdminRepository(DbFactory<AdminDBContextt> dbFactory, AdminDBContextt dbContextt) : base(dbFactory)
        {
            _dbContextt = dbContextt;
        }
        public async Task<AdminModel> Matching(string Email,string Password)
        {
            return await _dbContextt.Tbl_Admin.Where(a => a.Email == Email && a.Password == Password).FirstOrDefaultAsync();
        }
        public async Task<AdminModel> GETAdminData(int Id)
        {
            return await _dbContextt.Tbl_Admin.FindAsync(Id);
        }
    }
}
