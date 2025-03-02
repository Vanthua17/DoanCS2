using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyKho.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<nha_cung_cap> _List;
        public ObservableCollection<nha_cung_cap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private nha_cung_cap _SelectedItem;
        public nha_cung_cap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ten_nhacc = SelectedItem.ten_nhacc;
                    so_dien_thoai = SelectedItem.so_dien_thoai;
                    email = SelectedItem.email;
                    dia_chi = SelectedItem.dia_chi;
                    mo_ta = SelectedItem.mo_ta;
                    ngay_tao = SelectedItem.ngay_tao;
                    OnPropertyChanged();
                }

            }
        }

        private string _ten_nhacc;
        public string ten_nhacc { get => _ten_nhacc; set { _ten_nhacc = value; OnPropertyChanged(); } }
        private string _so_dien_thoai;
        public string so_dien_thoai { get => _so_dien_thoai; set { _so_dien_thoai = value; OnPropertyChanged(); } }

        private string _dia_chi;
        public string dia_chi { get => _dia_chi; set { _dia_chi = value; OnPropertyChanged(); } }

        private string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _mo_ta;
        public string mo_ta { get => _mo_ta; set { _mo_ta = value; OnPropertyChanged(); } }

        private DateTime? _ngay_tao;
        public DateTime? ngay_tao { get => _ngay_tao; set { _ngay_tao = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public SuplierViewModel()
        {
            List = new ObservableCollection<nha_cung_cap>(DataProvider.Ins.DB.nha_cung_cap);

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                // Điều kiện để nút "Thêm" có thể hoạt động (kiểm tra các trường không rỗng)
                return !string.IsNullOrEmpty(ten_nhacc) &&
                       !string.IsNullOrEmpty(so_dien_thoai) &&
                       !string.IsNullOrEmpty(dia_chi) &&
                       !string.IsNullOrEmpty(email);
            }, (p) =>
            {
                try
                {
                    // Tạo một đối tượng mới từ dữ liệu trong ViewModel
                    var Suplier = new nha_cung_cap()
                    {
                        ten_nhacc = ten_nhacc,
                        so_dien_thoai = so_dien_thoai,
                        dia_chi = dia_chi,
                        email = email,
                        mo_ta = mo_ta,
                        ngay_tao = ngay_tao ?? DateTime.Now // Sử dụng thời gian hiện tại nếu null
                    };

                    // Thêm đối tượng vào cơ sở dữ liệu
                    DataProvider.Ins.DB.nha_cung_cap.Add(Suplier);
                    DataProvider.Ins.DB.SaveChanges();

                    // Cập nhật lại danh sách hiển thị
                    RefreshData();

                    MessageBox.Show("Thêm dữ liệu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm dữ liệu: " + ex.Message);
                }
            });

            //sua dvd
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null) return false;

                var displayList = DataProvider.Ins.DB.nha_cung_cap.Where(x => x.id == SelectedItem.id);
                if (displayList == null || displayList.Count() != 0) return true;

                return false;
            }, (p) =>
            {
                var Suplier = DataProvider.Ins.DB.nha_cung_cap.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                Suplier.ten_nhacc = ten_nhacc;
                Suplier.so_dien_thoai = so_dien_thoai;
                Suplier.dia_chi = dia_chi;
                Suplier.email = email;
                Suplier.mo_ta= mo_ta;
                Suplier.ngay_tao= ngay_tao;
                DataProvider.Ins.DB.SaveChanges();
                RefreshData();
                SelectedItem.ten_nhacc = ten_nhacc;
            });
        }
        private void RefreshData()
        {
            List = new ObservableCollection<nha_cung_cap>(DataProvider.Ins.DB.nha_cung_cap.ToList());
        }

    }
}
