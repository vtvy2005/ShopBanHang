using System.Diagnostics;
using ShopBanHang.Models;
using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // S?A: G?p hai constructor thành M?T constructor duy nh?t
        // Constructor này s? nh?n T?T C? các d?ch v? mà Controller c?n
        public HomeController(ILogger<HomeController> logger)
        {
            // Kh?i t?o c? hai d?ch v?
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action này dùng ?? hi?n th? trang l?i khi có v?n ??
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}