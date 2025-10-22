using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBanHang.Models;
using ShopBanHang.Areas.Admin.Models;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")] // Khai báo Controller thuộc về khu vực Admin
    public class ProductController : Controller
    {
        private readonly DbHelper _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(DbHelper dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        // [GET] /Admin/Product hoặc /Admin/Product/Index
        // Hiển thị danh sách tất cả sản phẩm
        public async Task<IActionResult> Index()
        {
            try
            {
                var allProducts = await _dbHelper.GetAllProducts();
                return View(allProducts);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi truy cập DB, hiển thị thông báo
                TempData["ErrorMessage"] = "Không thể tải danh sách sản phẩm. Lỗi: " + ex.Message;
                return View(new List<Product>()); // Trả về view với danh sách rỗng
            }
        }

        // [GET] /Admin/Product/Create
        // Hiển thị form để tạo sản phẩm mới
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesDropdown();
            return View(new ProductCreateViewModel()); // Truyền vào một ViewModel rỗng
        }

        // [POST] /Admin/Product/Create
        // Xử lý dữ liệu khi người dùng submit form
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ProductCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var imageUrls = new List<string>();
        //        string thumbnailImageUrl = null;

        //        if (model.Images != null && model.Images.Any())
        //        {
        //            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
        //            Directory.CreateDirectory(uploadFolder); // Tự động tạo thư mục nếu chưa tồn tại

        //            foreach (var imageFile in model.Images)
        //            {
        //                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        //                string filePath = Path.Combine(uploadFolder, uniqueFileName);

        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await imageFile.CopyToAsync(fileStream);
        //                }
        //                imageUrls.Add("/images/products/" + uniqueFileName);
        //            }
        //            thumbnailImageUrl = imageUrls.FirstOrDefault(); // Lấy ảnh đầu tiên làm ảnh đại diện
        //        }

        //        // Ánh xạ từ ViewModel sang Model `Product` để lưu vào DB
        //        var product = new Product
        //        {
        //            TenSP = model.TenSP,
        //            DonGia = model.DonGia,
        //            DonGiaKhuyenMai = model.DonGiaKhuyenMai,
        //            HinhAnh = thumbnailImageUrl,
        //            MoTa = model.MoTa,
        //            mauSac = model.mauSac,
        //            maLoaiDen = model.maLoaiDen,
        //            kichThuoc = model.kichThuoc,
        //            thuongHieu = model.thuongHieu,
        //            so_Luong_Ton = model.so_Luong_Ton,
        //            trangThai = model.trangThai
        //        };

        //        try
        //        {
        //            await _dbHelper.AddProductWithImagesAsync(product, imageUrls);
        //            TempData["SuccessMessage"] = "Thêm sản phẩm mới thành công!";
        //            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách (PRG Pattern)
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", "Lỗi khi lưu vào database: " + ex.Message);
        //        }
        //    }

        //    // Nếu model không hợp lệ, tải lại dropdown và hiển thị lại form với các lỗi
        //    await PopulateCategoriesDropdown(model.maLoaiDen);
        //    return View(model);
        //}

        //// Hàm helper để tránh lặp lại code tải danh sách loại sản phẩm
        //private async Task PopulateCategoriesDropdown(object selectedCategory = null)
        //{
        //    var categories = await _dbHelper.GetAllCategories();
        //    ViewBag.Categories = new SelectList(categories, "MaLoaiSp", "TenLoaiSp", selectedCategory);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var imageUrls = new List<string>();
                string thumbnailImageUrl = null;

                if (model.Images != null && model.Images.Any())
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    Directory.CreateDirectory(uploadFolder); // Tự động tạo thư mục nếu chưa tồn tại

                    var now = DateTime.Now;
                    string timestamp = now.ToString("yyyy_MM_dd_HH_mm_ss");

                    int index = 1;

                    foreach (var imageFile in model.Images)
                    {
                        string fileExtension = Path.GetExtension(imageFile.FileName);
  
                        // (năm_tháng_ngày_giờ_phút_giây_index.phần_mở_rộng)
                        string uniqueFileName = $"{timestamp}_{index}{fileExtension}";

                        index++;

                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        imageUrls.Add("/images/products/" + uniqueFileName);
                    }
                    thumbnailImageUrl = imageUrls.FirstOrDefault(); // Lấy ảnh đầu tiên làm ảnh đại diện
                }

                // Ánh xạ từ ViewModel sang Model `Product` để lưu vào DB
                var product = new Product
                {
                    TenSP = model.TenSP,
                    DonGia = model.DonGia,
                    DonGiaKhuyenMai = model.DonGiaKhuyenMai,
                    HinhAnh = thumbnailImageUrl,
                    MoTa = model.MoTa,
                    mauSac = model.mauSac,
                    maLoaiDen = model.maLoaiDen,
                    kichThuoc = model.kichThuoc,
                    thuongHieu = model.thuongHieu,
                    so_Luong_Ton = model.so_Luong_Ton,
                    trangThai = model.trangThai
                };

                try
                {
                    await _dbHelper.AddProductWithImagesAsync(product, imageUrls);
                    TempData["SuccessMessage"] = "Thêm sản phẩm mới thành công!";
                    return RedirectToAction("Index"); // Chuyển hướng về trang danh sách (PRG Pattern)
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu vào database: " + ex.Message);
                }
            }

            // Nếu model không hợp lệ, tải lại dropdown và hiển thị lại form với các lỗi
            await PopulateCategoriesDropdown(model.maLoaiDen);
            return View(model);
        }

        // Hàm helper để tránh lặp lại code tải danh sách loại sản phẩm
        private async Task PopulateCategoriesDropdown(object selectedCategory = null)
        {
            var categories = await _dbHelper.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "MaLoaiSp", "TenLoaiSp", selectedCategory);
        }
    }
}