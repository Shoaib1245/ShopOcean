using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Price.Core;
using Service.Seller.APIs.Features.Size.Core;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Price.Repository
{
    public interface IPriceRepository:IGenericRepository<PriceModel,DBContextt>
    {
        Task<List<PriceModel>> GetPriceDatabyId(int id);
        Task<List<SizeModel>> GetSizeData();
    }
}
