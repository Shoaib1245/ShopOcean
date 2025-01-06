using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Price.Repository;
using Service.Seller.APIs.Features.Size.Core;
using Service.Seller.APIs.Features.SubCategory.Repository;
using Service.Seller.APIs.Utility;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Price.Service
{
    public class PriceService
    {
        private readonly IPriceRepository _IPriceRepository;
        public PriceService(IPriceRepository PriceRepository)
        {
            _IPriceRepository = PriceRepository;
        }
        public async Task<List<PriceModel>> GetPricedata(int Id)
        {
            var data = await _IPriceRepository.GetPriceDatabyId(Id);
            return data;
        }
        public async Task<List<SizeModel>> GetSizedata()
        {
            var data = await _IPriceRepository.GetSizeData();
            return data;
        }
        public async Task<bool> dataSave(PriceModel obj)
        {
            obj.IsDeleted = 0;
             await _IPriceRepository.Add(obj);
            return true;
        }
        
    }
}
