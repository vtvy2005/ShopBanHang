// Trong file Controllers/CartController.cs
using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models; // Thay bằng namespace của bạn
using System.Threading.Tasks;
using System.Collections.Generic;

public class CartController : Controller
{
    private readonly DbHelper _dbHelper;

    public CartController(DbHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    // Action này chỉ trả về View, JavaScript sẽ lo phần còn lại
    public IActionResult Index()
    {
        return View();
    }

    // API nhận danh sách ID và trả về chi tiết sản phẩm
    [HttpPost]
    [Route("api/cart/products")]
    public async Task<IActionResult> GetCartProducts([FromBody] List<int> productIds)
    {
        if (productIds == null || productIds.Count == 0)
        {
            return Ok(new List<Product>());
        }

        var products = await _dbHelper.GetProductsByIdsAsync(productIds);
        return Ok(products);
    }
}