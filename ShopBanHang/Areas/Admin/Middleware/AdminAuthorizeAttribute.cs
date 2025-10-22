using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.Areas.Admin.Middleware
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("admin");

            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
            }
            base.OnActionExecuting(context);
        }
    }
}
