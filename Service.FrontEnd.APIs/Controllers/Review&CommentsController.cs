using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.FrontEnd.APIs.Features.Review_Comments.Core;
using Service.FrontEnd.APIs.Features.Review_Comments.Service;
using Service.Shared.DTOs;

namespace Service.FrontEnd.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Review_CommentsController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        public Review_CommentsController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        [Route("Getdata")]
        public async Task<IActionResult> Getdata()
        {
          var data = await _reviewService.GetData();
            return Ok(data);
        }

        [HttpPost]
        [Route("CommentsSave")]
        public async Task<int> DataSave([FromForm] JsondataDTOs obj)
        {
            var d = await _reviewService.CommentsSave(obj, obj.file);
            if (d == 1)
            {
                return 1;
            }
            return 0;
        }
    }
}
