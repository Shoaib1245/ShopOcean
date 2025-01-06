using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Seller.APIs.Features.SubCategory.Repository;
using Service.Seller.APIs.Utility;

namespace Service.Seller.APIs.Features.SubCategory.Service
{
    public class SubCategoryService
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;
        private readonly IFileUploader _IFileUploader;
        public SubCategoryService(ISubCategoryRepository SubCategoryRepository, IFileUploader IFileUploader)
        {
            _SubCategoryRepository = SubCategoryRepository;
            _IFileUploader = IFileUploader;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _SubCategoryRepository.GetbyId(id);
            if (data != null)
            {
                _SubCategoryRepository.Delete(data);
                return true;
            }
            return false;
        }

        public async Task<SubCategoryModel> GetbyIdd(int id)
        {
            return await _SubCategoryRepository.GetbyId(id);
        }

        public async Task<List<SubCategoryModel>> Getdata()
        {
            return await _SubCategoryRepository.Getdata();
        }
        public async Task<List<SubCategoryModel>> Getdata(int Sellerid)
        {
            return await _SubCategoryRepository.GEtSubCategorybySellerId(Sellerid);
        }
        public async Task<int> Save(SubCategoryModel entity, IFormFile file)
        {
            entity.SubCategoryImage = _IFileUploader.savefile(file);
            entity.IsDeleted = false;
            if (entity.Id > 0)
            {
                _SubCategoryRepository.Update(entity);
                return 1;
            }
            else
            {
                await _SubCategoryRepository.Add(entity);
                return 2;
            }
        }

        public async Task<List<SubCategoryModel>> GetCategorydata(int Id)
        {
            var data=await _SubCategoryRepository.GetCategoryDatabyId(Id);
            return data;
        }
    }
}
