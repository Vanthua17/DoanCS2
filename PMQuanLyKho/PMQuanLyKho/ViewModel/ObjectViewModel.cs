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
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Model.vat_tu> _List;
        public ObservableCollection<Model.vat_tu> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.don_vi> _don_vi;
        public ObservableCollection<Model.don_vi> don_vi { get => _don_vi; set { _don_vi = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.loai> _loai;
        public ObservableCollection<Model.loai> loai { get => _loai; set { _loai = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.nha_cung_cap> _nha_cung_cap;
        public ObservableCollection<Model.nha_cung_cap> nha_cung_cap { get => _nha_cung_cap; set { _nha_cung_cap = value; OnPropertyChanged(); } }

        private Model.vat_tu _SelectedItem;
        public Model.vat_tu SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    // Gán các thuộc tính từ SelectedItem
                    ten_vat_tu = SelectedItem.ten_vat_tu;
                    QRCode = SelectedItem.QRCode;
                    BarCode = SelectedItem.BarCode;
                    mo_ta = SelectedItem.mo_ta;
                    ngay_tao = SelectedItem.ngay_tao ?? DateTime.Now;

                    // Lấy tên đơn vị, nhà cung cấp, loại từ các bảng liên quan
                    SelectedUnit = don_vi.FirstOrDefault(x => x.id == SelectedItem.id_don_vi);
                    Selectednha_cung_cap = nha_cung_cap.FirstOrDefault(x => x.id == SelectedItem.id_nhacc);
                    Selectedloai = loai.FirstOrDefault(x => x.id == SelectedItem.id_loai);

                    // Gọi OnPropertyChanged để cập nhật View
                    OnPropertyChanged(nameof(SelectedUnit));
                    OnPropertyChanged(nameof(Selectednha_cung_cap));
                    OnPropertyChanged(nameof(Selectedloai));
                }
            }
        }


        private Model.don_vi _SelectedUnit;
        public Model.don_vi SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
                if (SelectedItem != null && SelectedUnit != null)
                {
                    SelectedItem.id_don_vi = SelectedUnit.id;
                }
            }
        }

        private Model.loai _Selectedloai;
        public Model.loai Selectedloai
        {
            get => _Selectedloai;
            set
            {
                _Selectedloai = value;
                OnPropertyChanged();
                if (SelectedItem != null && Selectedloai != null)
                {
                    SelectedItem.id_loai = Selectedloai.id;
                }
            }
        }



        private Model.nha_cung_cap _Selectednha_cung_cap;
        public Model.nha_cung_cap Selectednha_cung_cap
        {
            get => _Selectednha_cung_cap;
            set
            {
                _Selectednha_cung_cap = value;
                OnPropertyChanged();
                if (SelectedItem != null && _Selectednha_cung_cap != null)
                {
                    SelectedItem.id_nhacc = _Selectednha_cung_cap.id;
                }
            }
        }



        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (_searchKeyword != value)
                {
                    _searchKeyword = value;
                    OnPropertyChanged(nameof(SearchKeyword));
                }
            }
        }

        private string _ten_vat_tu;
        public string ten_vat_tu { get => _ten_vat_tu; set { _ten_vat_tu = value; OnPropertyChanged(); } }

        private string _QRcode;
        public string QRCode { get => _QRcode; set { _QRcode = value; OnPropertyChanged(); } }

        private string _BarCode;
        public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

        private string _mo_ta;
        public string mo_ta { get => _mo_ta; set { _mo_ta = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _ngay_tao;
        public Nullable<System.DateTime> ngay_tao { get => _ngay_tao; set { _ngay_tao = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; }
        public ObjectViewModel()
        {
            List = new ObservableCollection<Model.vat_tu>(DataProvider.Ins.DB.vat_tu);
            don_vi = new ObservableCollection<Model.don_vi>(DataProvider.Ins.DB.don_vi);
            nha_cung_cap = new ObservableCollection<Model.nha_cung_cap>(DataProvider.Ins.DB.nha_cung_cap);
            loai = new ObservableCollection<Model.loai>(DataProvider.Ins.DB.loai);
            RefreshData();
            SearchCommand = new RelayCommand<object>(
                (p) => true,  // Điều kiện để cho phép nút hoạt động (luôn là true ở đây)
                (p) => Search()  // Gọi hàm Search khi nhấn nút
            );

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                return !string.IsNullOrEmpty(ten_vat_tu) && SelectedUnit != null && Selectednha_cung_cap != null && Selectedloai != null;
            }, (p) =>
            {
                var Object = new Model.vat_tu()
                {
                    ten_vat_tu = ten_vat_tu,
                    QRCode = QRCode,
                    BarCode = BarCode,
                    id_nhacc = Selectednha_cung_cap.id, // Sử dụng ? để tránh NullReferenceException
                    id_don_vi = SelectedUnit.id,
                    id_loai = Selectedloai.id,
                    ngay_tao = ngay_tao ?? DateTime.Now,
                    mo_ta = mo_ta,
                    id = Guid.NewGuid().ToString()
                };

                DataProvider.Ins.DB.vat_tu.Add(Object);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Thêm dữ liệu thành công!");
                RefreshData();
            });

            //sua dvd
            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && SelectedUnit != null && Selectednha_cung_cap != null && Selectedloai != null;
            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.vat_tu.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                if (Object != null)
                {
                    Object.ten_vat_tu = ten_vat_tu;
                    Object.QRCode = QRCode;
                    Object.BarCode = BarCode;
                    Object.id_nhacc = Selectednha_cung_cap.id;
                    Object.id_don_vi = SelectedUnit.id;
                    Object.id_loai = Selectedloai.id;
                    Object.ngay_tao = ngay_tao ?? DateTime.Now;
                    Object.mo_ta = mo_ta;
                    DataProvider.Ins.DB.SaveChanges();
                    RefreshData();
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }
            });


            // DeletedCommand
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                // Điều kiện để nút xóa được kích hoạt
                return SelectedItem != null; // Phải chọn một đối tượng để xóa
            }, (p) =>
            {
                // Xác nhận hành động xóa từ người dùng
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Tìm đối tượng cần xóa trong cơ sở dữ liệu
                    var objectToDelete = DataProvider.Ins.DB.vat_tu.Where(x => x.id == SelectedItem.id).SingleOrDefault();

                    if (objectToDelete != null)
                    {
                        // Xóa đối tượng khỏi cơ sở dữ liệu
                        DataProvider.Ins.DB.vat_tu.Remove(objectToDelete);
                        DataProvider.Ins.DB.SaveChanges();

                        // Cập nhật danh sách hiển thị
                        RefreshData();

                        MessageBox.Show("Xóa dữ liệu thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Đối tượng không tồn tại hoặc đã bị xóa!");
                    }
                }
            });

        }
        private void Search()
        {
            try
            {
                // Kiểm tra nếu DB không khả dụng
                if (DataProvider.Ins.DB?.vat_tu == null)
                {
                    MessageBox.Show("Dữ liệu không khả dụng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Kiểm tra từ khóa và lọc dữ liệu
                var filteredItems = string.IsNullOrEmpty(SearchKeyword)
                    ? DataProvider.Ins.DB.vat_tu
                    : DataProvider.Ins.DB.vat_tu.Where(obj =>
                        !string.IsNullOrEmpty(obj.ten_vat_tu) &&
                        obj.ten_vat_tu.ToLower().Contains(SearchKeyword.ToLower()));

                // Làm mới danh sách
                List.Clear();
                foreach (var item in filteredItems)
                {
                    List.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshData()
        {
            // Tải lại danh sách vật tư
            List = new ObservableCollection<Model.vat_tu>(DataProvider.Ins.DB.vat_tu.ToList());

            // Tải lại danh sách đơn vị, nhà cung cấp, loại nếu cần
            don_vi = new ObservableCollection<Model.don_vi>(DataProvider.Ins.DB.don_vi.ToList());
            nha_cung_cap = new ObservableCollection<Model.nha_cung_cap>(DataProvider.Ins.DB.nha_cung_cap.ToList());
            loai = new ObservableCollection<Model.loai>(DataProvider.Ins.DB.loai.ToList());
        }


    }
}
