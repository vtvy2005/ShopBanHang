//Thanh Vỹ - Model sản phẩm - 8/10/2025
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBanHang.Models
{
    [Table("Produc")]
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Mã sản phẩm code")]
        [StringLength(50)]
        public string? MaSP_Code { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(150)]
        public string? TenSP { get; set; }

        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        [DataType(DataType.Currency)]
        public decimal? DonGia { get; set; }

        [Display(Name = "Đơn giá khuyến mãi")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá khuyến mãi phải lớn hơn hoặc bằng 0")]
        [DataType(DataType.Currency)]
        public decimal? DonGiaKhuyenMai { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string? HinhAnh { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string? MoTa { get; set; }

        [Display(Name = "Màu sắc")]
        [StringLength(50)]
        public string? mauSac { get; set; }

        [Required]
        [Display(Name = "Mã loại đèn")]
        public int maLoaiDen { get; set; }

        [Display(Name = "Kích thước")]
        [StringLength(50)]
        public string? kichThuoc { get; set; }

        [Display(Name = "Thương hiệu")]
        [StringLength(100)]
        public string? thuongHieu { get; set; }

        [Display(Name = "Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không được âm")]
        public int so_Luong_Ton { get; set; }

        [Display(Name = "Trạng thái")]
        public bool trangThai { get; set; }
    }
}
