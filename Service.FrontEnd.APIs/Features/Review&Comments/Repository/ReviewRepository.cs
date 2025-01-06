using Service.FrontEnd.APIs.DatabaseContext;
using Service.FrontEnd.APIs.Features.Review_Comments.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;

namespace Service.FrontEnd.APIs.Features.Review_Comments.Repository
{
    public class ReviewRepository : GenericRepository<ReviewAndCommentsModel, FrontEndDBContext>, IReviewRepository
    {
        public ReviewRepository(DbFactory<FrontEndDBContext> dbFactory) : base(dbFactory)
        {

        }

        public async Task<List<ReviewAndCommentsModel>> getAlldata()
        {
            var data = await SimpleQuery<ReviewAndCommentsModel>("select Review.*,Cus.FirstName+Cus.LastName as CustomerName from tbl_ReviewAndComments Review inner join Tbl_Customer Cus on Cus.Id=Review.CustomerId");
            return  data.ToList();
        }
    }
}
