using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Windows;

namespace PMQuanLyKho.ViewModel
{   
 
    public class NhapKhoViewModel : BaseViewModel
    {
        private ObservableCollection<Model.nhap_kho> _List;
        public ObservableCollection<Model.nhap_kho> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.vat_tu> _vat_tu;
        public ObservableCollection<Model.vat_tu> vat_tu { get => _vat_tu; set { _vat_tu = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.quyen_nguoi_dung> _quyen_nguoi_dung;
        public ObservableCollection<Model.quyen_nguoi_dung> quyen_nguoi_dung { get => _quyen_nguoi_dung; set { _quyen_nguoi_dung = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.loai> _loai;
        public ObservableCollection<Model.loai> loai { get => _loai; set { _loai = value; OnPropertyChanged(); } }


        private Model.nhap_kho _SelectedItem;
        public Model.nhap_kho SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged();

                    if (_SelectedItem != null)
                    {
                        UpdateRelatedProperties();
                    }

                    // Đảm bảo cập nhật giao diện
                    OnPropertyChanged(nameof(selectedvat_tu));
                    OnPropertyChanged(nameof(SelectedUserRole)); // Thêm dòng này
                    OnPropertyChanged(nameof(Selectedloai));
                    OnPropertyChanged(nameof(so_luong));
                    OnPropertyChanged(nameof(gia_nhap));
                    OnPropertyChanged(nameof(gia_ban));
                    OnPropertyChanged(nameof(mo_ta));
                    OnPropertyChanged(nameof(ngay_nhap));
                }
            }
        }

        private void UpdateRelatedProperties()
        {
            if (_SelectedItem != null)
            {
                // Cập nhật selectedvat_tu từ id_vat_tu
                selectedvat_tu = vat_tu.FirstOrDefault(x => x.id == _SelectedItem.id_vat_tu);

                // Cập nhật Selectedloai từ id_loai
                Selectedloai = loai.FirstOrDefault(x => x.id == _SelectedItem.id_loai);

                // Cập nhật SelectedUserRole từ id_nguoi_dung
                SelectedUserRole = quyen_nguoi_dung.FirstOrDefault(x => x.id == _SelectedItem.id_nguoi_dung);

                // Cập nhật các giá trị khác
                so_luong = _SelectedItem.so_luong;
                gia_nhap = _SelectedItem.gia_nhap;
                gia_ban = _SelectedItem.gia_ban;
                mo_ta = _SelectedItem.mo_ta;
                ngay_nhap = _SelectedItem.ngay_nhap ?? DateTime.Now;

                // Không lọc danh sách vat_tu ở đây!
            }
            else
            {
                // Reset các thuộc tính khi không có SelectedItem
                selectedvat_tu = null;
                Selectedloai = null;
                SelectedUserRole = null;
                so_luong = null;
                gia_nhap = null;
                gia_ban = null;
                mo_ta = null;
                ngay_nhap = null;
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
            }
        }

        private Model.loai _Selectedloai;
        public Model.loai Selectedloai
        {
            get => _Selectedloai;
            set
            {
                if (_Selectedloai != value)
                {
                    _Selectedloai = value;
                    OnPropertyChanged();

                    // Lọc danh sách vật tư theo loại mới
                    FilterVatTuByLoai();

                    // Đảm bảo `selectedvat_tu` hợp lệ
                    if (Selectedloai != null && (selectedvat_tu == null || selectedvat_tu.id_loai != Selectedloai.id))
                    {
                        selectedvat_tu = vat_tu.FirstOrDefault(); // Đặt vật tư đầu tiên nếu không hợp lệ
                    }
                }
            }
        }



        private Model.vat_tu _selectedvat_tu;
        public Model.vat_tu selectedvat_tu
        {
            get => _selectedvat_tu;
            set
            {
                if (_selectedvat_tu != value)
                {
                    _selectedvat_tu = value;
                    OnPropertyChanged();

                    // Nếu `Selectedloai` không trùng với loại của vật tư được chọn, cập nhật loại
                    if (selectedvat_tu != null && Selectedloai != null && Selectedloai.id != selectedvat_tu.id_loai)
                    {
                        Selectedloai = loai.FirstOrDefault(l => l.id == selectedvat_tu.id_loai);
                    }
                }
            }
        }

        private void FilterVatTuByLoai()
        {
            if (Selectedloai != null)
            {
                // Lọc danh sách vat_tu theo loại được chọn
                var filteredVatTu = DataProvider.Ins.DB.vat_tu.Where(vt => vt.id_loai == Selectedloai.id).ToList();
                vat_tu = new ObservableCollection<Model.vat_tu>(filteredVatTu);

                // Kiểm tra `selectedvat_tu` hiện tại
                if (!vat_tu.Contains(selectedvat_tu))
                {
                    selectedvat_tu = vat_tu.FirstOrDefault(); // Reset về item đầu tiên nếu không còn hợp lệ
                }
            }
            else
            {
                // Nếu không chọn loại nào, hiển thị toàn bộ vat_tu
                vat_tu = new ObservableCollection<Model.vat_tu>(DataProvider.Ins.DB.vat_tu.ToList());
            }

            OnPropertyChanged(nameof(vat_tu));
            OnPropertyChanged(nameof(selectedvat_tu));
        }


        private Nullable<System.DateTime> _ngay_nhap;
        public Nullable<System.DateTime> ngay_nhap { get => _ngay_nhap; set { _ngay_nhap = value; OnPropertyChanged(); } }

        private Nullable<int> _so_luong;
        public Nullable<int> so_luong { get => _so_luong; set { _so_luong = value; OnPropertyChanged(); } }

        private Nullable<double> _gia_nhap;
        public Nullable<double> gia_nhap { get => _gia_nhap; set { _gia_nhap = value; OnPropertyChanged(); } }

        private Nullable<double> _gia_ban;
        public Nullable<double> gia_ban { get => _gia_ban; set { _gia_ban = value; OnPropertyChanged(); } }

        private string _mo_ta;
        public string mo_ta { get => _mo_ta; set { _mo_ta = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public NhapKhoViewModel()
        {
            List = new ObservableCollection<Model.nhap_kho>(DataProvider.Ins.DB.nhap_kho);
            vat_tu = new ObservableCollection<Model.vat_tu>(DataProvider.Ins.DB.vat_tu);
            quyen_nguoi_dung = new ObservableCollection<Model.quyen_nguoi_dung>(DataProvider.Ins.DB.quyen_nguoi_dung);
            loai = new ObservableCollection<Model.loai>(DataProvider.Ins.DB.loai);

            // Khởi tạo danh sách lọc ban đầu (hiển thị toàn bộ vật tư)

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (so_luong <= 0 || gia_ban <= 0 || gia_nhap<= 0)
                    return false;

                if ( selectedvat_tu == null || SelectedUserRole == null || Selectedloai == null)
                    return false;
                

                return true;
            }, (p) =>
            {
                try
                {
                    var inputInfo = new Model.nhap_kho()
                    {
                        id_vat_tu = selectedvat_tu.id,
                        id_nguoi_dung = (int)SelectedUserRole.id,
                        id_loai = (int)Selectedloai.id,
                        so_luong = so_luong ,
                        gia_nhap = gia_nhap,
                        gia_ban = gia_ban,
                        ngay_nhap =ngay_nhap ?? DateTime.Now,
                        mo_ta = mo_ta ?? string.Empty,
                        id = Guid.NewGuid().ToString()
                    };
                    
                    DataProvider.Ins.DB.nhap_kho.Add(inputInfo);
                    DataProvider.Ins.DB.SaveChanges();

                    SelectedItem = inputInfo; // Cập nhật đối tượng được chọn
                    OnPropertyChanged(nameof(List)); // Thông báo giao diện cập nhật
                    RefreshData(); // Thêm vào danh sách    

                    MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });


            //sua dvd
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (so_luong <= 0 || gia_ban <= 0 || gia_nhap <= 0)
                    return false;

                if (selectedvat_tu == null || SelectedUserRole == null || Selectedloai == null)
                    return false;

                return true;
            }, (p) =>
            {
                try
                {
                    // Lấy đối tượng cần sửa từ cơ sở dữ liệu
                    var inputInfo = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(x => x.id == SelectedItem.id);

                    if (inputInfo != null)
                    {
                        // Kiểm tra và cập nhật từng thuộc tính
                        if (inputInfo.id_vat_tu != selectedvat_tu.id)
                            inputInfo.id_vat_tu = selectedvat_tu.id;

                        if (inputInfo.vat_tu.id_loai != selectedvat_tu.loai.id)
                            inputInfo.vat_tu.id_loai = selectedvat_tu.loai.id; // Sửa ngày nhập

                        if (inputInfo.id_nguoi_dung != (int)SelectedUserRole.id)
                            inputInfo.id_nguoi_dung = (int)SelectedUserRole.id;
                        inputInfo.so_luong = so_luong ?? 0;
                        inputInfo.gia_nhap = gia_nhap.GetValueOrDefault(0);
                        inputInfo.gia_ban = gia_ban.GetValueOrDefault(0);
                        inputInfo.mo_ta = mo_ta ?? string.Empty;
                        inputInfo.ngay_nhap = ngay_nhap ?? DateTime.Now;

                        // Đặt trạng thái thực thể thành "Modified" để Entity Framework nhận biết
                        DataProvider.Ins.DB.Entry(inputInfo).State = System.Data.Entity.EntityState.Modified;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        DataProvider.Ins.DB.SaveChanges();
                        // Cập nhật danh sách hiển thị
                        int index = List.IndexOf(SelectedItem);
                        if (index >= 0)
                        {
                            // Làm mới đối tượng hiển thị từ database
                            List[index] = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(x => x.id == inputInfo.id);
                        }
                        OnPropertyChanged(nameof(List));
                        RefreshData();
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });




            DeleteCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra điều kiện xóa (phải chọn một mục)
                if (SelectedItem == null)
                    return false;

                return true;
            }, (p) =>
            {
                // Tìm đối tượng trong cơ sở dữ liệu
                var inputInfo = DataProvider.Ins.DB.nhap_kho.Where(x => x.id == SelectedItem.id).SingleOrDefault();

                if (inputInfo != null)
                {
                    // Xóa khỏi cơ sở dữ liệu
                    DataProvider.Ins.DB.nhap_kho.Remove(inputInfo);
                    DataProvider.Ins.DB.SaveChanges();

                    // Xóa khỏi danh sách hiển thị
                    List.Remove(SelectedItem);
                    RefreshData();
                    // Reset SelectedItem
                    SelectedItem = null;
                }
            });
            
        }

        private void RefreshData()
        {
            // Lấy dữ liệu từ cơ sở dữ liệu và sắp xếp theo tên loại
            List = new ObservableCollection<Model.nhap_kho>(
                DataProvider.Ins.DB.nhap_kho
                .OrderBy(x => x.loai.ten_loai) // Sắp xếp theo tên loại
                .ToList()
            );
        }

    }
}
