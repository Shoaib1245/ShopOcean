using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Shared.DTOs;
using Service.UI.Models;
using Service.UI.Utilize;
using System;

namespace Service.UI.Controllers.FrontEndControllers
{
    public class CustomerRegistrationController : Controller
    {
        private readonly IApiServiceFrontEnd _apiServiceFrontEnd;
        public CustomerRegistrationController(IApiServiceFrontEnd apiServiceFrontEnd)
        {
            _apiServiceFrontEnd=apiServiceFrontEnd;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<int> Signup(CustomerModel obj)
        {

            var data = await _apiServiceFrontEnd.PostAsync(obj, "Customer/CustomerSignupData");
            if (data == "1")
            {
                return 1;
            }
            return 0;
        }

        public async Task<IActionResult> CustomerProfile()
        {
             var CustomerId =  HttpContext.Session.GetString("CustomerId");
           var jsondata=await _apiServiceFrontEnd.GetByIdAsync("Customer/GetdatabyId",Convert.ToInt32(CustomerId));
            var data=JsonConvert.DeserializeObject<CustomerModel>(jsondata);
            return View(data);
        }
        public async Task<int> SaveProfile(CustomerModel obj)
        {
            var stream = new MemoryStream();
            var filebyte = stream.ToArray();
            if (obj.ImageUrl != null)
            {
                await obj.ImageUrl.OpenReadStream().CopyToAsync(stream);
                filebyte = stream.ToArray();
            }
            var data = await _apiServiceFrontEnd.PostWithFileAsync(obj, filebyte, obj.ImageUrl.FileName, "Customer/CustomerProfile");
            if (data == "1")
            {
                return 1;
            }
            return 0;
        }

        public IActionResult CustomerLogin()
        {
            return View();
        }

 

        public async Task<int> Login(CustomerModel obj)
        {
            string apiEndPoint = $"Customer/Logindata?Email={obj.Email}&Password={obj.Password}";
            var jsondata = await _apiServiceFrontEnd.GetJsonAsync(apiEndPoint);
            var data = JsonConvert.DeserializeObject<CustomerModel>(jsondata);

            if (data !=null)
            {
                HttpContext.Session.SetString("CustomerId",Convert.ToString(data.Id));
                HttpContext.Session.SetString("CustomerName",Convert.ToString(data.FirstName+data.LastName));
                return 1;
            }
            return 0;
        }
    }
}
