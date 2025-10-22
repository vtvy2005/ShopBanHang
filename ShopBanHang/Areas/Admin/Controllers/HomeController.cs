using ShopBanHang.Areas.Admin.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            // Chặn cache từ trình duyệt
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            ViewBag.AdminName = HttpContext.Session.GetString("admin");
            return View();
        }

        public IActionResult Blank()
        {
            return View();
        }

        public IActionResult Buttons()
        {
            return View();
        }

        public IActionResult Flot()
        {
            return View();
        }

        public IActionResult Morris()
        {
            return View();
        }

        public IActionResult Forms()
        {
            return View();
        }

        public IActionResult Grid()
        {
            return View();
        }

        public IActionResult Icons()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Panels()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult Tygography()
        {
            return View();
        }

        public IActionResult ManageProduct()
        {
            return View();
        }
    }
}
