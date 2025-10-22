//Thanh Vỹ - Helper kết nối cơ sở dữ liệu - 8/10/2025
using System.Data;
// Bạn nên dùng Microsoft.Data.SqlClient thay vì System.Data.SqlClient cho các project .NET Core/.NET 5+ mới
using Microsoft.Data.SqlClient;
using ShopBanHang.Models; // Giả sử namespace Models của bạn là đây

namespace ShopBanHang.Models
{
    public class DbHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            // Lấy chuỗi kết nối từ file appsettings.json
            _connectionString = _configuration.GetConnectionString("dbShopLight");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = null; // Khởi tạo là null
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Dùng tham số @id để tránh lỗi bảo mật SQL Injection
                string sql = "SELECT * FROM produc WHERE MaSP = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Thêm tham số @id và giá trị của nó
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Chỉ đọc một dòng duy nhất nếu tìm thấy
                        if (await reader.ReadAsync())
                        {
                            product = new Product
                            {
                                MaSP = (int)reader["MaSP"],
                                TenSP = reader["TenSP"].ToString(),
                                DonGia = (decimal)reader["DonGia"],
                                DonGiaKhuyenMai = reader["DonGiaKhuyenMai"] != DBNull.Value ? (decimal?)reader["DonGiaKhuyenMai"] : null,
                                HinhAnh = reader["HinhAnh"].ToString(),
                                MoTa = reader["MoTa"].ToString(), // Giả sử bạn có cột MoTa
                                maLoaiDen = (int)reader["maLoaiDen"],
                                mauSac = reader["mauSac"].ToString(),
                                so_Luong_Ton = (int)reader["so_Luong_Ton"]

                                // Thêm các thuộc tính khác nếu cần: mauSac, kichThuoc...
                            };
                        }
                    }
                }
            }
            return product; // Sẽ trả về null nếu không tìm thấy sản phẩm
        }

        // Phương thức lấy tất cả sản phẩm
        public async Task<List<Product>> GetAllProducts(int take = 0)
        {
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Cập nhật câu SQL
                string sql = "SELECT ";
                if (take > 0)
                {
                    sql += $"TOP {take} ";
                }
                sql += "MaSP, TenSP, DonGia, DonGiaKhuyenMai, HinhAnh, maLoaiDen FROM produc ORDER BY MaSP DESC"; // Thêm ORDER BY để lấy sản phẩm mới nhất

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Các phần còn lại giữ nguyên...
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                MaSP = (int)reader["MaSP"],
                                TenSP = reader["TenSP"].ToString(),
                                DonGia = (decimal)reader["DonGia"],
                                DonGiaKhuyenMai = reader["DonGiaKhuyenMai"] != DBNull.Value ? (decimal?)reader["DonGiaKhuyenMai"] : null,
                                HinhAnh = reader["HinhAnh"].ToString(),
                                maLoaiDen = (int)reader["maLoaiDen"]
                            });
                        }
                    }
                }
            }
            return products;
        }

        // Phương thức lấy tất cả loại sản phẩm
        public async Task<List<Category>> GetAllCategories()
        {
            var categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT maLoaiSP, tenLoaiSP FROM loaiSanPham";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new Category
                            {
                                // SỬA LỖI TẠI ĐÂY: Tên cột trong reader[...] phải giống hệt 100% với tên cột trong câu SELECT
                                MaLoaiSp = (int)reader["maLoaiSP"],
                                TenLoaiSp = reader["tenLoaiSP"].ToString()
                            });
                        }
                    }
                }
            }
            return categories;
        }
        public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
        {
            var products = new List<Product>();
            if (ids == null || !ids.Any())
            {
                return products; // Trả về danh sách rỗng nếu không có id nào
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Tạo chuỗi tham số an toàn: @id0, @id1, @id2,...
                var parameters = new List<string>();
                var command = new SqlCommand();
                for (int i = 0; i < ids.Count; i++)
                {
                    var paramName = $"@id{i}";
                    parameters.Add(paramName);
                    command.Parameters.AddWithValue(paramName, ids[i]);
                }

                // Tạo câu lệnh SQL với mệnh đề IN
                command.CommandText = $"SELECT MaSP, TenSP, DonGia, DonGiaKhuyenMai, HinhAnh FROM produc WHERE MaSP IN ({string.Join(", ", parameters)})";
                command.Connection = connection;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            MaSP = (int)reader["MaSP"],
                            TenSP = reader["TenSP"].ToString(),
                            DonGia = (decimal)reader["DonGia"],
                            DonGiaKhuyenMai = reader["DonGiaKhuyenMai"] != DBNull.Value ? (decimal?)reader["DonGiaKhuyenMai"] : null,
                            HinhAnh = reader["HinhAnh"].ToString()
                        });
                    }
                }
            }
            return products;
        }


        // ✅ SỬA ĐỔI: Cập nhật phương thức để INSERT đầy đủ các trường
        public async Task<int> AddProductWithImagesAsync(Product product, List<string> imageUrls)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Thêm sản phẩm vào bảng `Produc` và lấy ra ID vừa tạo
                        // Câu lệnh INSERT đã được cập nhật với đầy đủ các cột
                        string productSql = @"
                    INSERT INTO Produc (
                        TenSP, DonGia, DonGiaKhuyenMai, HinhAnh, MoTa, mauSac, 
                        maLoaiDen, kichThuoc, thuongHieu, so_Luong_Ton, trangThai
                    ) VALUES (
                        @TenSP, @DonGia, @DonGiaKhuyenMai, @HinhAnh, @MoTa, @mauSac, 
                        @maLoaiDen, @kichThuoc, @thuongHieu, @so_Luong_Ton, @trangThai
                    );
                    SELECT SCOPE_IDENTITY();"; // Lấy ID vừa tạo

                        var command = new SqlCommand(productSql, connection, transaction);

                        // Gán giá trị cho các tham số
                        command.Parameters.AddWithValue("@TenSP", product.TenSP);
                        command.Parameters.AddWithValue("@DonGia", product.DonGia);
                        command.Parameters.AddWithValue("@HinhAnh", (object)product.HinhAnh ?? DBNull.Value); // Lưu ảnh đại diện
                        command.Parameters.AddWithValue("@maLoaiDen", product.maLoaiDen);
                        command.Parameters.AddWithValue("@so_Luong_Ton", product.so_Luong_Ton);
                        command.Parameters.AddWithValue("@trangThai", product.trangThai);

                        // Xử lý các giá trị có thể là NULL
                        command.Parameters.AddWithValue("@DonGiaKhuyenMai", (object)product.DonGiaKhuyenMai ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MoTa", (object)product.MoTa ?? DBNull.Value);
                        command.Parameters.AddWithValue("@mauSac", (object)product.mauSac ?? DBNull.Value);
                        command.Parameters.AddWithValue("@kichThuoc", (object)product.kichThuoc ?? DBNull.Value);
                        command.Parameters.AddWithValue("@thuongHieu", (object)product.thuongHieu ?? DBNull.Value);

                        // Thực thi và lấy ID sản phẩm vừa tạo
                        int newProductId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        // 2. Thêm các ảnh chi tiết vào bảng `ProductImage`
                        if (imageUrls != null && imageUrls.Any())
                        {
                            string imageSql = "INSERT INTO ProductImage (MaSP, UrlAnh) VALUES (@MaSP, @UrlAnh)";
                            foreach (var url in imageUrls)
                            {
                                var imageCommand = new SqlCommand(imageSql, connection, transaction);
                                imageCommand.Parameters.AddWithValue("@MaSP", newProductId);
                                imageCommand.Parameters.AddWithValue("@UrlAnh", url);
                                await imageCommand.ExecuteNonQueryAsync();
                            }
                        }

                        transaction.Commit();
                        return newProductId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<Category> AddCategoryAsync(string categoryName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
            INSERT INTO loaiSanPham (tenLoaiSP) VALUES (@TenLoai);
            SELECT * FROM loaiSanPham WHERE maLoaiSP = SCOPE_IDENTITY();";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TenLoai", categoryName);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Category
                            {
                                MaLoaiSp = (int)reader["maLoaiSP"],
                                TenLoaiSp = reader["tenLoaiSP"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Trả về null nếu có lỗi
        }

    }
}