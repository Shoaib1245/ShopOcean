using Microsoft.EntityFrameworkCore;
using Service.Admin.APIs.Features.Admin.Core;
using Service.Admin.APIs.Features.ChatSystem.Core;
using Service.Admin.APIs.Features.SellerProfileStatus.Core;

namespace Service.Admin.APIs.AdminDBContext
{
    public class AdminDBContextt:DbContext
    {
        public AdminDBContextt(DbContextOptions<AdminDBContextt> option):base(option)
        {
            
        }
        public DbSet<SellerModel> Tbl_Seller { get; set; } = null!;
        public DbSet<AdminModel> Tbl_Admin { get; set; } = null!;
        public DbSet<ChatSystemModel> Tbl_ChatSystem { get; set; } = null!;

    }
}
