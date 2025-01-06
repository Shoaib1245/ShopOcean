using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.EmailSender;
using Service.UI.Models;
using Service.UI.Utilize;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class ChatSystemController : Controller
    {
        private readonly IApiServiceAdminn _apiservice;
        public ChatSystemController(IApiServiceAdminn apiservice)
        {
            _apiservice = apiservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SellerChat()
        {
            return View();
        }
        public async Task<IActionResult> AdminChat()
        {
            var jsondata = await _apiservice.GetJsonAsync("SellerProfileStatus/GetSellerData");
            var data = JsonConvert.DeserializeObject<List<SellerModel>>(jsondata);
            return View(data);
        }

        public async Task<IActionResult> SaveChat(string userName, string userType, string Message)
        {
            ChatSystemModel chatSystem = new ChatSystemModel();
            chatSystem.UserName = userName;
            chatSystem.UserType = userType;
            chatSystem.Message = Message;
            chatSystem.MessageTime = DateTime.Now;
            await _apiservice.PostAsync(chatSystem, "ChatSystem/MessageSave");
            return View();
        }

        public async Task<IActionResult> AdminChatPartial(string userName, string userType)
        {
            ChatSystemModel chatSystem = new ChatSystemModel();
            chatSystem.UserName = userName;
            chatSystem.UserType = userType;
            var jsondata = await _apiservice.PostAsync(chatSystem, "ChatSystem/GetMessages");

            var data = JsonConvert.DeserializeObject<List<ChatSystemModel>>(jsondata);
            return PartialView("_ADMinPartial", data);
        }
    }

    }

