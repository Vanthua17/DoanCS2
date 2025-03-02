using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyKho.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool  IsLogin { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        private string _UserName;
        public string UserName { get=>_UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public LoginViewModel() 
        {
            IsLogin = false;
            UserName = "";
            Password = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p)=> { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p)=> { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p)=> { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
                return;

            string passEndcode = MD5Hash(Base64Encode(Password));
            var user = DataProvider.Ins.DB.nguoi_dung
                 .FirstOrDefault(d => d.ten_dang_nhap == UserName && d.mat_khau == passEndcode);

            if (user != null)
            {
                // Kiểm tra vai trò người dùng
                if (user.id_quyen == 1) // Tài khoản quản trị viên
                {
                    IsLogin = true;
                    MessageBox.Show("Đăng nhập thành công với quyền Quản trị viên!");
                }
                else if (user.id_quyen != 1)
                {
                    IsLogin = true;
                    MessageBox.Show("Đăng nhập thành công với quyền thông thường!");
                }
                // Đóng form đăng nhập
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }


        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            { 
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
