using Service.Admin.APIs.Features.Admin.Core;
using Service.Admin.APIs.Features.Admin.Repository;

namespace Service.Admin.APIs.Features.Admin.Service
{
    public class AdminService
    {
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository adminRepository)
        {
            _repository = adminRepository;
        }
        
        public async Task<AdminModel> MatchEmailpassword(string email,string password)
        {
            return await _repository.Matching(email, password);
        }
        public async Task<AdminModel> GetAdmindata(int Id)
        {
            return await _repository.GETAdminData(Id);
        }
    }
}
