using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using BCrypt.Net; // ✅ THÊM DÒNG NÀY

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("admin") != null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string connectionString = _configuration.GetConnectionString("dbShopLight");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Chỉ tìm tài khoản theo UserName
                string sql = "SELECT * FROM Account WHERE UserName = @username AND TrangThai = 1";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // 2. Nếu tìm thấy tài khoản...
                    if (reader.Read())
                    {
                        // Lấy chuỗi hash từ database
                        string storedHash = reader["Pass"].ToString();

                        // 3. Dùng BCrypt để so sánh mật khẩu người dùng nhập với chuỗi hash
                        bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, storedHash);

                        if (isPasswordCorrect)
                        {
                            // Mật khẩu chính xác, tiến hành đăng nhập
                            string role = reader["Role"].ToString();
                            HttpContext.Session.SetString("admin", username);
                            HttpContext.Session.SetString("role", role);

                            reader.Close();
                            conn.Close();
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                    }
                }

                // 4. Nếu không tìm thấy tài khoản HOẶC mật khẩu không khớp
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                conn.Close();
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
    }
}