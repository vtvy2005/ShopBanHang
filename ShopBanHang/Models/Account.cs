using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBanHang.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã tài khoản")]
        public string? IdCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập tối đa 50 ký tự")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu tối đa 100 ký tự")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Pass { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        [StringLength(20)]
        [Display(Name = "Vai trò")]
        public string Role { get; set; } = null!;

        [Display(Name = "Trạng thái hoạt động")]
        public bool TrangThai { get; set; }

        [StringLength(200)]
        [Display(Name = "Phiên đăng nhập")]
        public string? Session { get; set; }


    }
}
