using System.ComponentModel.DataAnnotations;

namespace ShopBanHang.Models
{
    public class Cart
    {
        [Required]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Tên khách hàng")]
        [StringLength(100)]
        public string? tenKH { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [StringLength(150)]
        public string? TenSP { get; set; }

        [Display(Name = "Hình ảnh sản phẩm")]
        [StringLength(250)]
        public string? HinhAnh { get; set; }

        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        [DataType(DataType.Currency)]
        public decimal? DonGia { get; set; }

        [Display(Name = "Đơn giá khuyến mãi")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải lớn hơn hoặc bằng 0")]
        [DataType(DataType.Currency)]
        public decimal? DonGiaKhuyenMai { get; set; }

        [Display(Name = "Kích thước")]
        [StringLength(50)]
        public string? kichThuoc { get; set; }

        [Display(Name = "Mã loại đèn")]
        public int maLoaiDen { get; set; }

        [Display(Name = "Thương hiệu")]
        [StringLength(100)]
        public string? thuongHieu { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int? soLuong { get; set; }

        [Display(Name = "Tổng tiền")]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal? TongTien { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }
}
