using Microsoft.EntityFrameworkCore;
using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Customer.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.DTOs;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Customer.Repository
{
    public class CustomerRepository : GenericRepository<CustomerModel, FrontEndDBContext>, ICustomerRepository
    {
        private readonly FrontEndDBContext _dbContext;
        public CustomerRepository(DbFactory<FrontEndDBContext> dbFactory, FrontEndDBContext dbContext) : base(dbFactory)
        {
            _dbContext = dbContext;
        }
        public async Task<CustomerModel> CheckEmail(CustomerModel obj)
        {
            return await _dbContext.Tbl_Customer.Where(a => a.Email == obj.Email).FirstOrDefaultAsync();
        }
        public async Task<CustomerModel> CustomerLogin(string Email,string Password)
        {
            var data = await _dbContext.Tbl_Customer.Where(a => a.Email == Email && a.Password == Password).FirstOrDefaultAsync();
            return data;
        }

    }
}
