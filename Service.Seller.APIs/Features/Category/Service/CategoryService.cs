using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Category.Repository;
using Service.Seller.APIs.Utility;

namespace Service.Seller.APIs.Features.Category.Service
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileUploader _IFileUploader;
        public CategoryService(ICategoryRepository categoryRepository, IFileUploader IFileUploader)
        {
            _categoryRepository = categoryRepository;
            _IFileUploader = IFileUploader;
        }

        public async Task<bool> Delete(int id)
        {
            var data=await _categoryRepository.GetbyId(id);
            if(data != null)
            {
                _categoryRepository.Delete(data);
                return true;
            }
            return false;
        }

        public async Task<CategoryModel> GetbyIdd(int id)
        {
           return await _categoryRepository.GetbyId(id);
        }

        public async Task<List<CategoryModel>> Getdata()
        {
            return await _categoryRepository.Getdata();
        }
        public async Task<List<CategoryModel>> Getdata(int SellerId)
        {
            return await _categoryRepository.GEtCategorydatabySellerId(SellerId);
        }

        public async Task<int> Save(CategoryModel entity,IFormFile file)
        {           
               entity.Image= _IFileUploader.savefile(file);         
               entity.CreatedDate= DateTime.Now;
               entity.IsDeleted= false;
            if (entity.Id > 0)
            {
                _categoryRepository.Update(entity);
                return 1;
            }
            else
            {
                await _categoryRepository.Add(entity);
                return 2;
            }
               
        }

        //public async Task<bool> Update(CategoryModel entity, IFormFile file)
        //{
        //    var data = await _categoryRepository.GetbyId(Id);
        //    if (data != null)
        //    {
        //        _categoryRepository.Update(data);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
