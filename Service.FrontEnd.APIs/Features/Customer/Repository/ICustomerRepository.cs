using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Customer.Core;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Customer.Repository
{
    public interface ICustomerRepository : IGenericRepository<CustomerModel,FrontEndDBContext>
    {
        Task<CustomerModel> CheckEmail(CustomerModel obj);
        Task<CustomerModel> CustomerLogin(string Email, string Password);
    }
}
