using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Apartment_Management.Helper;
using Apartment_Management.Model;
using Apartment_Management.Service;
using Apartment_Management.View;

namespace Apartment_Management.ViewModel
{
    internal class LoginViewModel : Base_View_Model
    {
        private readonly UserService _userService;
        private string _email;
        private string _password;
        private string _message;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new RelayCommand(async _ => await Login());
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public async Task Login()
        {
            if (ValidateInputs())
            {
                var user = await _userService.Login(Email, Password);

                if (user != null)
                {
                    var currentWindow = Application.Current.MainWindow;

                    // Khởi động cửa sổ MainWindow
                    var mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                    // Tắt cửa sổ hiện tại
                    currentWindow.Close();
                }
                else
                {
                    MessageBox.Show("Email hoặc mật khẩu không đúng.");
                }
            }
        }
        private bool ValidateInputs()
        {
            // Kiểm tra định dạng email
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Vui lòng nhập Email và Mật khẩu để đăng nhập!");
                return false;
            }
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Email không hợp lệ!");
                return false;
            }
            return true;
        }
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Mã hóa mật khẩu bằng SHA-256


        // Kiểm tra mật khẩu đã mã hóa có khớp với mật khẩu gốc hay không

        private class Credentials
        {
            public string Email { get; set; }
            public string EncryptedPassword { get; set; }
        }
        
    }
}
