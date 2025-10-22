using ShopBanHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.ViewComponents // Thay YourProjectName bằng tên project của bạn
{
    public class ProductJsonListViewComponent : ViewComponent
    {
        // View Component này không cần DbHelper vì nó không xử lý dữ liệu
        public ProductJsonListViewComponent()
        {
        }

        // Phương thức InvokeAsync chỉ nhận tham số và truyền chúng cho View
        public IViewComponentResult Invoke(int take)
        {
            // Trả về View và truyền tham số 'take' vào đó
            return View(take);
        }
    }
}