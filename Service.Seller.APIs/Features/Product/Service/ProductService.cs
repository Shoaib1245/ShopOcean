using Service.Seller.APIs.Features.Category.Core;
using Service.Seller.APIs.Features.Category.Repository;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Product.Core;
using Service.Seller.APIs.Features.Product.Repository;
using Service.Seller.APIs.Features.SubCategory.Core;
using Service.Seller.APIs.Utility;
using Service.Shared.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace Service.Seller.APIs.Features.Product.Service
{
    public class ProductService
    {
        private readonly IProductRepository _IProductRepository;
        private readonly IFileUploader _IFileUploader;
        public ProductService(IProductRepository IProductRepository, IFileUploader IFileUploader)
        {
            _IProductRepository = IProductRepository;
            _IFileUploader = IFileUploader;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _IProductRepository.GetbyId(id);
            if (data != null)
            {
                _IProductRepository.Delete(data);
                return true;
            }
            return false;
        }

        public async Task<ProductModel> GetbyIdd(int id)
        {
            return await _IProductRepository.GetbyId(id);
        }

        public async Task<List<ProductModel>> Getdata()
        {
            return await _IProductRepository.GetProductData();
        }
        public async Task<List<ProductModel>> Getdata(int SellerId)
        {
            return await _IProductRepository.GetProductData(SellerId);
        }

        public async Task<int> Save(ProductModel entity, List<IFormFile> file)
        {
            for (int i = 0; i < file.Count; i++)
            {
                entity.Image += _IFileUploader.savefile(file[i])+",";
            }
            entity.Image=entity.Image?.TrimEnd(',');
            entity.CreatedDate = DateTime.Now;
            entity.IsDeleted = false;
            entity.IsNewArrival = true;
            if (entity.Id > 0)
            {
                 _IProductRepository.Update(entity);
                return 1;
            }
            else
            {
                await _IProductRepository.Add(entity);
                return 2;
            }

        }

        public async Task<bool> Update(int Id)
        {
            var data = await _IProductRepository.GetbyId(Id);
            if (data != null)
            {
                _IProductRepository.Update(data);
                return true;
            }
            return false;
        }

        public async Task<List<ProductModel>> GetProductbySubId(int Id)
        {
            var data = await _IProductRepository.GetProductbySubId(Id);
            return data;
        }
        public async Task<List<ProductModel>> GetProducttwithprice()
        {
            var data = await _IProductRepository.GetProductwithPrice();
            return data;
        }

        public async Task<ProductModel> GetProducttwithProdId(int id)
        {
            var data = await _IProductRepository.GetProductwithProductId(id);
            return data;
        }
        public async Task<ProductModel> getPricebySizeId(ProductModel obj)
        {
            var data = await _IProductRepository.ChangePriceWithSizeId(obj);
            return data;
        }
        public async Task<List<ProductModel>> GetProductbyRange(int id,decimal min,decimal max)
        {
            var data = await _IProductRepository.GetProductbyRange(id, min, max);
            return data;
        }
    }
}
