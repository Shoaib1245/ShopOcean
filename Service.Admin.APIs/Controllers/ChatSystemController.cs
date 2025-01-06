using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Admin.APIs.Features.ChatSystem.Core;
using Service.Admin.APIs.Features.ChatSystem.Service;

namespace Service.Admin.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSystemController : ControllerBase
    {
        private readonly ChatSystemService _chatSystemService;
        public ChatSystemController(ChatSystemService chatSystemService)
        {
            _chatSystemService = chatSystemService;
        }
        [HttpPost]
        [Route("MessageSave")]
        public async Task<int> MessageSave(ChatSystemModel model)
        {
            await _chatSystemService.MessageSave(model);
            return 1;
        }
        [HttpPost("GetMessages")]
        //here are issue
        public async Task<List<ChatSystemModel>> GetMessages(ChatSystemModel model)
        {
            var data = await _chatSystemService.getmessages(model);
            return data;
        }

    }
}
