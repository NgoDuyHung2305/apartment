using Apartment_Management.Model;
using FirebaseAdmin.Auth.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Apartment_Management.Service
{
    public class UserService
    {
            private readonly FirebaseService _firebaseService;

            public UserService()
            {
                _firebaseService = new FirebaseService();
            }

            // Hàm mã hóa mật khẩu bằng SHA-256
            private string HashPassword(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Chuyển mật khẩu thành byte[]
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Chuyển byte[] thành chuỗi hex
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            // Hàm xác minh mật khẩu (so sánh mật khẩu nhập vào với mật khẩu đã mã hóa)
            private bool VerifyPassword(string password, string hashedPassword)
            {
            // Mã hóa mật khẩu nhập vào và so sánh với mật khẩu đã mã hóa
                string hashedInputPassword = HashPassword(password);
                return hashedInputPassword == hashedPassword;
            }

            // Đăng nhập
            public async Task<User> Login(string email, string password)
            {
                // Lấy danh sách người dùng
                var users = await _firebaseService.GetDataAsync<User>("Users");

                // Tìm người dùng theo email
                var user = users.FirstOrDefault(u => u.UserMail == email);
                // Kiểm tra mật khẩu
                if (user != null && VerifyPassword(password, user.UserPassword))
                {
                UserAccount.Instance.User= new User(user);
                    return user; // Đăng nhập thành công
                }

                return null; // Đăng nhập thất bại
            }

            // Lấy toàn bộ danh sách người dùng
            public async Task<List<User>> GetAllUsers()
            {
                return await _firebaseService.GetDataAsync<User>("Users");
            }
        }
    }

