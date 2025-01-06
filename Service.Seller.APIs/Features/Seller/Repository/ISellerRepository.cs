using Service.Seller.APIs.DataBaseContext;
using Service.Seller.APIs.Features.Seller.Core;
using Service.Shared.DTOs;
using Service.Shared.Generic;

namespace Service.Seller.APIs.Features.Seller.Repository
{
    public interface ISellerRepository: IGenericRepository<SellerModel,DBContextt>
    {
        Task<SellerModel> SellerLogin(SellerLoginDTO obj);
        Task<SellerModel> CheckEmail(SellerLoginDTO obj);
        Task<GraphsDTOs> GetGraphsdata(int Sellerid);
        

    }
}
