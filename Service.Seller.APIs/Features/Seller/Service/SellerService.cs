using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Category.Repository;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Product.Repository;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Seller.APIs.Features.Seller.Repository;
using Service.Seller.APIs.Utility;
using Service.Shared.DTOs;
using static Dapper.SqlMapper;

namespace Service.Seller.APIs.Features.Seller.Service
{
    public class SellerService
    {
        private readonly ISellerRepository _ISellerRepository;
        private readonly IFileUploader _IFileUploader;
        public SellerService(ISellerRepository ISellerRepository, IFileUploader IFileUploader)
        {
            _ISellerRepository = ISellerRepository;
            _IFileUploader = IFileUploader;
        }

        public async Task<SellerModel> SellerLogin(SellerLoginDTO obj)
        {
            var data =await _ISellerRepository.SellerLogin(obj);
            return data;
        }

        public async Task<SellerModel> GetbyIdd(int id)
        {
            return await _ISellerRepository.GetbyId(id);
        }


        public async Task<bool> PasswordUpdatee(SellerDTOs obj)
        {          
           await _ISellerRepository.DataUpdate<SellerDTOs>("Tbl_Seller", obj,"Email");
            return true;
        }
        //public async Task<bool> Save(SellerModel entity)
        //{
        //    await _ISellerRepository.Add(entity);
        //    return true;
        //}

        public async Task<bool> Update(SellerModel obj)
        {
        await _ISellerRepository.ProfileDataUpdate<SellerModel>("Update Tbl_Seller set Password=@Password where Id=@Id", obj);
                return true;
        }
        public async Task<bool> DetailUpdate(SellerModel obj)
        {
            await _ISellerRepository.ProfileDataUpdate<SellerModel>("Update Tbl_Seller set Name=@Name,Cnic=@Cnic,Contact=@Contact,DOB=@DOB,Address=@Address,Email=@Email,Status=@Status,ZipCode=@ZipCode,IsDeleted=0 where Id=@Id", obj);
            return true;
        }
        public async Task<bool> ImageUpdate(SellerModel obj,IFormFile file)
        {
            obj.Image = _IFileUploader.savefile(file);
            await _ISellerRepository.ProfileDataUpdate<SellerModel>("Update Tbl_Seller set Image=@Image where Id=@Id", obj);
            return true;
        }
        public async Task<int> SignupSave(SellerLoginDTO obj)
        {
            var data=await _ISellerRepository.CheckEmail(obj);
            if (data == null)
            {
                return await _ISellerRepository.SaveData<SellerLoginDTO>("Tbl_Seller", obj, "Id");
            }
            return 0;
        }
        
        public async Task<GraphsDTOs> Graphsdata(int Sellerid)
        {           
            return await _ISellerRepository.GetGraphsdata(Sellerid);
        }
    }
}
