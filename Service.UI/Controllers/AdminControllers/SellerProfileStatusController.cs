using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.EmailSender;
using Service.UI.Models;
using Service.UI.Utilize;
using System.Security.AccessControl;

namespace Service.UI.Controllers.AdminControllers
{
    public class SellerProfileStatusController : Controller
    {
        private readonly IApiServiceAdminn _apiservice;
        private readonly EmailCustTemplate _Emailsend;
        public SellerProfileStatusController(IApiServiceAdminn apiservice, EmailCustTemplate Emailsend)
        {
            _apiservice = apiservice;
            _Emailsend = Emailsend;
        }
        public async Task<IActionResult> Index()
        {
           var jsondata= await _apiservice.GetJsonAsync("SellerProfileStatus/GetSellerData");
            var data=JsonConvert.DeserializeObject<List<SellerModel>>(jsondata);
            return View(data);
        }
        public async Task<int> SellerStatusUpdate(int Status,int Id,string Email,string SellerName)
        {
            SellerModel model=new SellerModel();
            model.Status = Status;
            model.Id = Id;
            var jsondata = await _apiservice.PostAsync(model, "SellerProfileStatus/SellerStatusUpdate");
            var data = JsonConvert.DeserializeObject<List<SellerModel>>(jsondata);
            var statusdata = "";
            if (Status == 1)
            {
                statusdata = "Pending";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Experience to our Users.", Email ?? "", "Seller Status", $"Dear {SellerName} Your Request Is on {statusdata}");


            }
            else if (Status == 2)
            {
                statusdata = "Accepts";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Experience to our Users.", Email ?? "", "Seller Status", $"Dear {SellerName} Your Request Is on {statusdata}");
            }

            else
            {
                statusdata = "Rejected";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Experience to our Users.", Email ?? "", "Seller Status", $"Dear {SellerName} Your Request Is on {statusdata}");
            }
           return 1;
        }
        public async Task<int> Delete(int Id)
        {
            var data = await _apiservice.DeleteAsync("SellerProfileStatus/", Id);
            if (data == true)
            {
                return 1;
            }
            return 0;
        }
    }
}
