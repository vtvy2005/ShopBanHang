using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Thư viện để dùng IFormFile
using System.Collections.Generic;

// Đảm bảo namespace khớp với project của bạn
namespace ShopBanHang.Areas.Admin.Models
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal DonGia { get; set; }

        [Display(Name = "Đơn giá khuyến mãi")]
        public decimal? DonGiaKhuyenMai { get; set; }

        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [Display(Name = "Màu sắc")]
        public string? mauSac { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
        [Display(Name = "Loại đèn")]
        public int maLoaiDen { get; set; }

        [Display(Name = "Kích thước")]
        public string? kichThuoc { get; set; }

        [Display(Name = "Thương hiệu")]
        public string? thuongHieu { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho")]
        [Display(Name = "Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không được âm")]
        public int so_Luong_Ton { get; set; }

        [Display(Name = "Kích hoạt (hiển thị)")]
        public bool trangThai { get; set; } = true;

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một ảnh")]
        [Display(Name = "Hình ảnh sản phẩm")]
        public List<IFormFile> Images { get; set; }
    }
}