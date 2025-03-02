using Microsoft.Win32;
using OfficeOpenXml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace PMQuanLyKho.ViewModel
{
    public class NguoiDungViewModel : BaseViewModel
    {
        private ObservableCollection<nguoi_dung> _List;
        public ObservableCollection<nguoi_dung> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<quyen_nguoi_dung> _UserRole;
        public ObservableCollection<quyen_nguoi_dung> UserRole { get => _UserRole; set { _UserRole = value; OnPropertyChanged(); } }

        private nguoi_dung _SelectedItem;
        public nguoi_dung SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ten_nguoi_dung = SelectedItem.ten_nguoi_dung;
                    ten_dang_nhap = SelectedItem.ten_dang_nhap;
                    mat_khau = SelectedItem.mat_khau;
                    ngay_tao = SelectedItem.ngay_tao ?? DateTime.Now;
                    // Gán quyền dựa trên id_quyen của người dùng
                    SelectedUserRole = UserRole.FirstOrDefault(x => x.id == SelectedItem.id_quyen);
                    OnPropertyChanged();
                }
            }

        }

        private Model.quyen_nguoi_dung _SelectedUserRole;
        public Model.quyen_nguoi_dung SelectedUserRole
        {
            get => _SelectedUserRole;
            set
            {
                _SelectedUserRole = value;
                OnPropertyChanged();
                if (SelectedItem != null && SelectedUserRole != null)
                {
                    SelectedItem.id_quyen = SelectedUserRole.id;
                }

            }
        }

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get => _ConfirmPassword;
            set
            {
                _ConfirmPassword = value;
                OnPropertyChanged();
            }
        }


        private string _ten_nguoi_dung;
        public string ten_nguoi_dung { get => _ten_nguoi_dung; set { _ten_nguoi_dung = value; OnPropertyChanged(); } }
        
        private string _ten_dang_nhap;
        public string ten_dang_nhap { get => _ten_dang_nhap; set { _ten_dang_nhap = value; OnPropertyChanged(); } }

        private string _mat_khau;
        public string mat_khau { get => _mat_khau; set { _mat_khau = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _ngay_tao;
        public Nullable<System.DateTime> ngay_tao { get => _ngay_tao; set { _ngay_tao = value; OnPropertyChanged(); } }
        
        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChangedPassword { get; set; }
        public NguoiDungViewModel()
        {
            UserRole = new ObservableCollection<quyen_nguoi_dung>(DataProvider.Ins.DB.quyen_nguoi_dung);
            List = new ObservableCollection<nguoi_dung>(DataProvider.Ins.DB.nguoi_dung);

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                var Users = DataProvider.Ins.DB.nguoi_dung.Where(x => x.ten_dang_nhap == ten_dang_nhap);
                if (Users == null || Users.Count() != 0) return false;

                // Kiểm tra nếu có thông tin hợp lệ để thêm
                return !string.IsNullOrEmpty(ten_dang_nhap) && !string.IsNullOrEmpty(ten_dang_nhap) && SelectedUserRole != null;
            }, (p) =>
            {
                string passEndcode = MD5Hash(Base64Encode(mat_khau));
                var users = new nguoi_dung()
                {
                    ten_nguoi_dung = ten_nguoi_dung,
                    ten_dang_nhap = ten_dang_nhap,
                    mat_khau = passEndcode,
                    ngay_tao =  ngay_tao ?? DateTime.Now,
                    id_quyen = SelectedUserRole.id,
                };

                // Thêm người dùng vào cơ sở dữ liệu
                DataProvider.Ins.DB.nguoi_dung.Add(users);
                DataProvider.Ins.DB.SaveChanges();

                // Thêm vào danh sách ObservableCollection để cập nhật UI
                RefreshData();
                MessageBox.Show("Thêm dữ liệu thành công!");
            });

            // Sửa thông tin người dùng
            EditCommand = new RelayCommand<object>((p) =>
            {

                // Kiểm tra xem người dùng đã được chọn chưa
                return SelectedItem != null && !string.IsNullOrEmpty(ten_dang_nhap) && !string.IsNullOrEmpty(ten_dang_nhap) && SelectedUserRole != null;
            }, (p) =>
            {
                string passEndcode = MD5Hash(Base64Encode(mat_khau));
                if (SelectedItem != null)
                {
                    // Cập nhật thông tin người dùng trong cơ sở dữ liệu
                    SelectedItem.ten_dang_nhap = ten_dang_nhap;
                    SelectedItem.ten_nguoi_dung = ten_nguoi_dung;
                    SelectedItem.id_quyen = SelectedUserRole.id;
                    ngay_tao = ngay_tao ?? DateTime.Now;
                    SelectedItem.mat_khau = passEndcode; // Nếu cần cập nhật mật khẩu

                    DataProvider.Ins.DB.SaveChanges();

                    // Cập nhật lại thông tin trong danh sách ObservableCollection
                    OnPropertyChanged(nameof(List));
                }
                RefreshData();
                MessageBox.Show("Cập nhậtz dữ liệu thành công!");
            });

            // Xóa người dùng
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra xem người dùng đã được chọn chưa
                return SelectedItem != null;
            }, (p) =>
            {
                if (SelectedItem != null)
                {
                    // Kiểm tra số lượng người dùng có cùng IdRole
                    int countIdRole = DataProvider.Ins.DB.nguoi_dung.Count(u => u.id_quyen == SelectedItem.id);

                    // Nếu số lượng người dùng có cùng IdRole > 0, không cho phép xóa
                    if (countIdRole > 1)
                    {
                        // Hiển thị thông báo hoặc xử lý nếu muốn
                        MessageBox.Show("Không thể xóa người dùng này vì có người dùng khác cùng quyền.");
                        return;
                    }

                    // Nếu không còn người dùng nào khác cùng quyền, tiến hành xóa
                    var userToDelete = DataProvider.Ins.DB.nguoi_dung.FirstOrDefault(u => u.id == SelectedItem.id);
                    if (userToDelete != null)
                    {
                        DataProvider.Ins.DB.nguoi_dung.Remove(userToDelete);
                        DataProvider.Ins.DB.SaveChanges();

                        // Xóa người dùng khỏi danh sách ObservableCollection
                        List.Remove(SelectedItem);
                    }
                }
            });

            
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
        private void RefreshData()
        {
            // Tải lại danh sách từ cơ sở dữ liệu
            List = new ObservableCollection<Model.nguoi_dung>(DataProvider.Ins.DB.nguoi_dung.ToList());
        }

        

    }

}
