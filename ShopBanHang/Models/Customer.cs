using System.ComponentModel.DataAnnotations;

namespace ShopBanHang.Models
{
    public class Customer
    {
        [Required]
        [Display(Name = "Mã khách hàng")]
        public int maKH { get; set; }

        [Display(Name = "Mã Code khách hàng")]
        [StringLength(50, ErrorMessage = "Mã khách hàng không vượt quá 50 ký tự")]
        public string? maKH_Code { get; set; }

        [Required]
        [Display(Name = "Mã tài khoản")]
        public int maAccount { get; set; }

        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string? tenKH { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(100)]
        public string? email { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string? address { get; set; }

        [Display(Name = "Phản hồi")]
        [StringLength(500)]
        public string? feedback { get; set; }

        [Display(Name = "Trạng thái hoạt động")]
        public bool trangThai { get; set; }
    }
}
