using Microsoft.AspNetCore.Mvc;
using Service.Shared.DTOs;
using Service.UI.EmailSender;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class LoginController : Controller
    {
        private readonly IApiService _apiService;
        private readonly EmailTemplate _Emailsend;
        SellerDTOs obj=new SellerDTOs();
        public LoginController(IApiService apiService, EmailTemplate emailtem)
        {
            _apiService = apiService;
            _Emailsend = emailtem;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<int> LoginData(SellerLoginDTO obj)
        {
            
            var data =await _apiService.PostAsync(obj, "Login/Logindata");
            HttpContext.Session.SetString("Token", data);
          
            if(data != "" && data != "{\"statusCode\":401}")
            {
                return 1;
            }
            return 0;
        }


        public IActionResult LoginVerification()
        {
            return View();
        }
        public IActionResult EmailSender()
        {
            _Emailsend.CommonTemplate("Dear Its Your Verification Code.", HttpContext.Session.GetString("Email") ?? "", "4 Digit Verification Code", HttpContext.Session.GetString("Code") ?? "");
            return View();
        }

        public int Forgetdata(string Email)
        {
            Random random = new Random();
            var Code = "";
            for (int i = 0; i < 4; i++)
            {
                Code += random.Next(0, 9);
            }
            HttpContext.Session.SetString("Code", Code);
            HttpContext.Session.SetString("Email", Email);
            return 1;
        }

        public int EmailCodeMatching(string Code)
        {
            if(Code== HttpContext.Session.GetString("Code"))
            {
                return 1;
            }
            return 0;
        }
        public IActionResult ResetPassword()
        {
            return View();
        }

        public int RestPassSave(string newPassword, string ConformPassword)
        {
            if(newPassword==ConformPassword)
            {
                obj.Password = ConformPassword;
                obj.Email= HttpContext.Session.GetString("Email");
                
                _apiService.PostAsync(obj, "Login/PasswordUpdate");
                return 1;
            }
            return 0;
        }

        public IActionResult Signup()
        {
            return View();
        }
        public async Task<int> SignupData(SellerLoginDTO obj)
        {
          var data=  await _apiService.PostAsync(obj, "Login/SignupData");
            if (data=="1")
            {
                return 1;
            }
            return 0;
        }
    }

}
