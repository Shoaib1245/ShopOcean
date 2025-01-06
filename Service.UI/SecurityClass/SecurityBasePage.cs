using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;

namespace Service.UI.SecurityClass
{
    public class SecurityBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (HttpContext.Session.GetString("Token") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
    }
}
