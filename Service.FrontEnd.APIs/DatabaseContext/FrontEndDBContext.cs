using Microsoft.EntityFrameworkCore;
using Service.FrontEnd.APIs.Features.Cart.Core;
using Service.FrontEnd.APIs.Features.Customer.Core;
using Service.FrontEnd.APIs.Features.Review_Comments.Core;

namespace Service.FrontEnd.APIs.DatabaseContext
{
    public class FrontEndDBContext:DbContext
    {
        public FrontEndDBContext(DbContextOptions<FrontEndDBContext> option):base(option)
        {
            
        }
        public DbSet<CartModel> Tbl_Cart { get; set; }
        public DbSet<CustomerModel> Tbl_Customer { get; set; }
        public DbSet<ReviewAndCommentsModel> tbl_ReviewAndComments { get; set; }
        public DbSet<OrderPlacedModel> Tbl_OrderPlaced { get; set; }
    }
}
