using Service.FrontEnd.APIs.Features.Customer.Core;
using Service.FrontEnd.APIs.Features.Customer.Repository;
using Service.FrontEnd.APIs.Utility;
using Service.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Service.FrontEnd.APIs.Features.Customer.Service
{
    public class CustomerService
    {
        private readonly FileUploader _FileUploader;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository,FileUploader fileUploader)
        {
            _customerRepository = customerRepository;
            _FileUploader = fileUploader;
        }

        public async Task<CustomerModel> CustomerLogin(string Email,string Password)
        {
            var data = await _customerRepository.CustomerLogin(Email,Password);
            return data;
        }

        public async Task<CustomerModel> GetdatabyId(int id)
        {
            var data = await _customerRepository.GetbyId(id);
            return data;
        }
        public async Task<int> CustomerSignupSave(CustomerModel entity)
        {
            var data = await _customerRepository.CheckEmail(entity);
            if (entity.Id == 0 && data == null)
            {
                return await _customerRepository.SaveData<CustomerModel>("Tbl_Customer", entity, "Id");
            }
            return 0;

        }
        public async Task<int> CustomerProfile(CustomerModel entity, IFormFile file)
        {
            entity.Image = _FileUploader.savefile(file);
            if (entity.Id != 0)
            {
                return await _customerRepository.ProfileDataUpdate<CustomerModel>("Update Tbl_Customer set FirstName=@FirstName,LastName=@LastName,Email=@Email,Password=@Password,Contact=@Contact,Address=@Address,ZipCode=@ZipCode,Image=@Image,Status='true',IsDeleted='false' where Id=@Id", entity);
            }
            return 0;

        }
    }
}
