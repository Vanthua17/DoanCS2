using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace PMQuanLyKho.ViewModel
{
    public class PhieuNhapViewModel : BaseViewModel
    {
        private ObservableCollection<loai> _List;
        public ObservableCollection<loai> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private loai _SelectedItem;
        public loai SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ten_loai = SelectedItem.ten_loai;
                    mo_ta = SelectedItem.mo_ta;
                    ngay_tao = SelectedItem.ngay_tao ?? DateTime.Now;
                }
            }
        }

        private string _ten_loai;
        public string ten_loai { get => _ten_loai; set { _ten_loai = value; OnPropertyChanged(); } }

        private string _mo_ta;
        public string mo_ta { get => _mo_ta; set { _mo_ta = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _ngay_tao;
        public Nullable<System.DateTime> ngay_tao { get => _ngay_tao; set { _ngay_tao = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public PhieuNhapViewModel()
        {
            List = new ObservableCollection<loai>(DataProvider.Ins.DB.loai);

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra nếu các trường cần thiết đều có dữ liệu
                return !string.IsNullOrEmpty(ten_loai);
            }, (p) =>
            {
                // Thêm mới vào cơ sở dữ liệu
                var newInput = new loai() { ten_loai = ten_loai, ngay_tao = ngay_tao ?? DateTime.Now, mo_ta = mo_ta };
                DataProvider.Ins.DB.loai.Add(newInput);
                DataProvider.Ins.DB.SaveChanges();

                // Thêm vào danh sách ObservableCollection để UI cập nhật
                RefreshData();

                // Thông báo thành công
                MessageBox.Show("Thêm dữ liệu thành công!");
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra nếu SelectedItem là null (không có mục được chọn để sửa)

                return !string.IsNullOrEmpty(ten_loai);
                // Kiểm tra nếu Id hoặc DateInput chưa được nhập đầy đủ
            }, (p) =>
            {
                // Cập nhật thông tin trong cơ sở dữ liệu
                var inputToUpdate = DataProvider.Ins.DB.loai.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                if (inputToUpdate != null)
                {
                    inputToUpdate.ten_loai = ten_loai;
                    inputToUpdate.mo_ta = mo_ta;
                    inputToUpdate.ngay_tao = ngay_tao?? DateTime.Now;
                    DataProvider.Ins.DB.SaveChanges();
                    RefreshData();
                    // Thông báo thành công
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra nếu không có mục nào được chọn
                if (SelectedItem == null) return false;

                // Nếu có mục được chọn, cho phép xóa
                return true;
            }, (p) =>
            {
                // Xóa khỏi cơ sở dữ liệu
                var inputToDelete = DataProvider.Ins.DB.loai.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                if (inputToDelete != null)
                {
                    DataProvider.Ins.DB.loai.Remove(inputToDelete);
                    DataProvider.Ins.DB.SaveChanges();

                    // Xóa khỏi ObservableCollection để UI cập nhật
                    List.Remove(SelectedItem);
                    RefreshData();
                    // Thông báo thành công
                    MessageBox.Show("Xóa dữ liệu thành công!");
                }
            });


        }
        private void RefreshData()
        {
            // Tải lại danh sách từ cơ sở dữ liệu
            List = new ObservableCollection<Model.loai>(DataProvider.Ins.DB.loai.ToList());

        }

    }
}
