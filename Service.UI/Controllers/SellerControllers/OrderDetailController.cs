using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.UI.EmailSender;
using Service.UI.Models;
using Service.UI.SecurityClass;
using Service.UI.Utilize;

namespace Service.UI.Controllers.SellerControllers
{
    public class OrderDetailController : SecurityBaseController
    {
        private readonly IApiServiceFrontEnd _apiService;
        private readonly EmailCustTemplate _Emailsend;
        public OrderDetailController(IApiServiceFrontEnd apiService, EmailCustTemplate Emailsend)
        {
            _apiService = apiService;
            _Emailsend = Emailsend;
        }
        public async Task<IActionResult> Index()
        {
            var SellerId = Convert.ToInt32(HttpContext.Session.GetString("SellerId"));
            var jsondata = await _apiService.GetByIdAsync("Cart/GetOrderData",SellerId);
            var data = JsonConvert.DeserializeObject<List<OrderDataModel>>(jsondata);
            return View(data);
        }
        public async Task<int> ChangeStatus(int Id,int Status ,string Email,string customername)
        {
            OrderPlacedModel obj=new OrderPlacedModel();
            obj.Status=Status;
            obj.Id = Id;
            var jsondata = await _apiService.PostAsync(obj,"Cart/StatusChange");
            var statusdata = "";
            if (Status==1)
            {
                 statusdata = "Pending";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Products.", Email ?? "", "Order Status", $"Dear {customername} Your Order Is on {statusdata}");

            }
            else if (Status == 2)
            {
                statusdata = "Accepts";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Products.", Email ?? "", "Order Status", $"Dear {customername} Your Order Is on {statusdata}");

            }
            else if (Status == 3)
            {
                statusdata = "Shiped";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Products.", Email ?? "", "Order Status", $"Dear {customername} Your Order Is on {statusdata}");
            }
            else
            {
                statusdata = "Rejected";
                _Emailsend.CommonTemplate("Thanks for Your Trust .I Always Provide more better Products.", Email ?? "", "Order Status", $"Dear {customername} Your Order Is on {statusdata}");

            }

            return 1;
        }
    }
}
