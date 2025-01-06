using Newtonsoft.Json;
using Service.FrontEnd.APIs.Features.Customer.Repository;
using Service.FrontEnd.APIs.Features.Review_Comments.Core;
using Service.FrontEnd.APIs.Features.Review_Comments.Repository;
using Service.FrontEnd.APIs.Utility;
using Service.Shared.DTOs;

namespace Service.FrontEnd.APIs.Features.Review_Comments.Service
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly FileUploader _FileUploader;
        public ReviewService(IReviewRepository reviewRepository, FileUploader fileUploader)
        {
            _reviewRepository = reviewRepository;
            _FileUploader = fileUploader;
        }

        public async Task<List<ReviewAndCommentsModel>> GetData()
        {
            return await _reviewRepository.getAlldata();
        }
        
        public async Task<bool> CommentsSave(ReviewAndCommentsModel obj)
        {
            return await _reviewRepository.Add(obj);
        }
        public async Task<int> CommentsSave(JsondataDTOs obj, IFormFile file)
        {
           ReviewAndCommentsModel entity= JsonConvert.DeserializeObject<ReviewAndCommentsModel>(obj.jsondata);
            entity.Image = _FileUploader.savefile(file);
            if (entity.Id == 0)
            {
                await _reviewRepository.Add(entity);
                return 1;
            }
            return 0;

        }
    }
}
