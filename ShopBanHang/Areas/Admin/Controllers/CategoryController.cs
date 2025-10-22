using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/admin/[controller]")] // Route chung cho controller này
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DbHelper _dbHelper;

        public CategoryController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public class CategoryCreateDto
        {
            public string TenLoaiSP { get; set; }
        }

        [HttpPost("create")] // Route sẽ là /api/admin/category/create
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.TenLoaiSP))
            {
                return BadRequest("Tên danh mục không được để trống.");
            }

            try
            {
                var newCategory = await _dbHelper.AddCategoryAsync(dto.TenLoaiSP);
                if (newCategory != null)
                {
                    return Ok(newCategory); // Trả về đối tượng danh mục vừa tạo
                }
                return StatusCode(500, "Không thể tạo danh mục mới.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }
        }
    }
}
