using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Review_Comments.Core;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Review_Comments.Repository
{
    public interface IReviewRepository:IGenericRepository<ReviewAndCommentsModel,FrontEndDBContext>
    {
        Task<List<ReviewAndCommentsModel>> getAlldata();
    }
}
