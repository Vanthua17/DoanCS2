using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PMQuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<don_vi> _List;
        public ObservableCollection<don_vi> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private don_vi _SelectedItem;
        public don_vi SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ten_don_vi = SelectedItem.ten_don_vi;
                    ngay_tao = SelectedItem.ngay_tao ?? DateTime.Now;
                    mo_ta = SelectedItem.mo_ta;
                }
            }
        }

        private string _ten_don_vi;
        public string ten_don_vi { get => _ten_don_vi; set { _ten_don_vi = value; OnPropertyChanged(); } }

        private string _mo_ta;
        public string mo_ta { get => _mo_ta; set { _mo_ta = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _ngaytao;
        public Nullable<System.DateTime> ngay_tao { get => _ngaytao; set { _ngaytao = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public UnitViewModel()
        {
            List = new ObservableCollection<don_vi>(DataProvider.Ins.DB.don_vi);

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ten_don_vi)) return false;

                return true;
            }, (p) =>
            {
                var Unit = new don_vi() { ten_don_vi = ten_don_vi, ngay_tao = ngay_tao ?? DateTime.Now, mo_ta = mo_ta };
                DataProvider.Ins.DB.don_vi.Add(Unit);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Unit);

                RefreshData(); // Làm mới dữ liệu
                MessageBox.Show("Thêm dữ liệu thành công!");
            });
            //sua dvd
            EditCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra nếu tên đơn vị không rỗng
                if (string.IsNullOrEmpty(ten_don_vi)) return false;

                return true;
            }, (p) =>
            {
                // Lấy đơn vị cần cập nhật từ cơ sở dữ liệu
                var Unit = DataProvider.Ins.DB.don_vi.Where(x => x.id == SelectedItem.id).SingleOrDefault();

                // Nếu tìm thấy đơn vị
                if (Unit != null)
                {
                    // Cập nhật thông tin
                    Unit.ten_don_vi = ten_don_vi;
                    Unit.ngay_tao = ngay_tao ?? DateTime.Now;
                    Unit.mo_ta = mo_ta;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    DataProvider.Ins.DB.SaveChanges();

                    // Thông báo cập nhật thành công
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    // Nếu không tìm thấy đơn vị
                    MessageBox.Show("Không tìm thấy đơn vị để cập nhật.");
                }
            });

            // Xóa Unit
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra nếu chưa chọn 
                if (SelectedItem == null) return false;

                // Kiểm tra xem có Object nào đang sử dụng vat_tu này hay không
                var relatedObjects = DataProvider.Ins.DB.vat_tu.Where(obj => obj.id_don_vi == SelectedItem.id).Count();
                if (relatedObjects > 0) return false; // Nếu có liên kết, không cho phép xóa

                return true; // Nếu không có liên kết, cho phép xóa
            }, (p) =>
            {
                try
                {
                    // Xóa Unit khỏi cơ sở dữ liệu
                    var unitToDelete = DataProvider.Ins.DB.don_vi.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    if (unitToDelete != null)
                    {
                        DataProvider.Ins.DB.don_vi.Remove(unitToDelete);
                        DataProvider.Ins.DB.SaveChanges();

                        // Cập nhật danh sách
                        List.Remove(unitToDelete);
                        MessageBox.Show("Xóa thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        }
        private void RefreshData()
        {
            // Tải lại danh sách từ cơ sở dữ liệu
            List = new ObservableCollection<don_vi>(DataProvider.Ins.DB.don_vi.ToList());
        }

    }
}
