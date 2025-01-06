using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;

namespace Service.UI.SecurityClass
{
    public class BasepageforCustomer : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var customerToken = HttpContext.Session.GetString("CustomerToken");
            if (customerToken == null)
            {
                // Log to check if this branch is always hit
                Console.WriteLine("Session expired or not set.");
                context.Result = new RedirectToActionResult("Index", "CustomerRegistration", null);
            }

            base.OnActionExecuting(context);
        }

    }
}
