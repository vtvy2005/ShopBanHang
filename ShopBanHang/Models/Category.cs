using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBanHang.Models
{
    [Table("loaiSanPham")]
    public partial class Category
    {
        [Key]
        [Display(Name = "Mã loại sản phẩm")]
        public int MaLoaiSp { get; set; }

        [Display(Name = "Mã code loại sản phẩm")]
        [StringLength(50)]
        public string? MaLoaiSpCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm")]
        [Display(Name = "Tên loại sản phẩm")]
        [StringLength(150)]
        public string TenLoaiSp { get; set; } = null!;

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }
}
