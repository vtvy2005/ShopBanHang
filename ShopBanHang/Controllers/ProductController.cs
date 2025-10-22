using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models;

namespace BaiTap_Luyen.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbHelper _dbHelper;

        // Inject DbHelper thông qua constructor
        public ProductController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        // Action này chỉ trả về View rỗng, JavaScript sẽ lo phần còn lại
        public IActionResult Index()
        {
            return View();
        }

        // Action cho trang chi tiết sản phẩm
        public IActionResult Single(int id)
        {
            // Logic cho trang chi tiết sản phẩm của bạn
            ViewBag.ProductId = id;
            return View();
        }

        // === API ENDPOINTS ===


        [HttpGet]
        [Route("api/products/{id}")] // Route sẽ có dạng /api/products/5
        public async Task<IActionResult> GetProductApi(int id)
        {
            try
            {
                var product = await _dbHelper.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(); // Trả về lỗi 404 Not Found nếu không có sản phẩm
                }
                return Ok(product); // Trả về dữ liệu sản phẩm dạng JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> GetProductsApi([FromQuery] int take = 0) // Thêm [FromQuery] int take
        {
            try
            {
                // Truyền tham số 'take' vào DbHelper
                var products = await _dbHelper.GetAllProducts(take);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/categories")] // Route cho loại sản phẩm
        public async Task<IActionResult> GetCategoriesApi()
        {
            try
            {
                var categories = await _dbHelper.GetAllCategories();
                return Ok(categories); // Trả về danh sách loại sản phẩm dạng JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
