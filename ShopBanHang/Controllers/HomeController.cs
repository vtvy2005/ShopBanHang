using System.Diagnostics;
using ShopBanHang.Models;
using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // S?A: G?p hai constructor th�nh M?T constructor duy nh?t
        // Constructor n�y s? nh?n T?T C? c�c d?ch v? m� Controller c?n
        public HomeController(ILogger<HomeController> logger)
        {
            // Kh?i t?o c? hai d?ch v?
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action n�y d�ng ?? hi?n th? trang l?i khi c� v?n ??
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}