using LiveCharts.Wpf;
using LiveCharts;
using PMQuanLyKho;
using PMQuanLyKho.Model;
using PMQuanLyKho.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Documents;

namespace PMQuanyKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        private SeriesCollection _series;
        private List<string> _labels;

        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand PhieuNhapCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public ICommand PhieuXuatCommand { get; set; }

        private int _slHangNhap;
        public int SLHangNhap
        {
            get => _slHangNhap;
            set
            {
                if (_slHangNhap != value)
                {
                    _slHangNhap = value;
                    OnPropertyChanged(); // Cập nhật giao diện khi giá trị thay đổi
                }
            }
        }

        private int _SLHangXuat;
        public int SLHangXuat
        {
            get => _SLHangXuat;
            set
            {
                if (_SLHangXuat != value)
                {
                    _SLHangXuat = value;
                    OnPropertyChanged(); // Cập nhật giao diện khi giá trị thay đổi
                }
            }
        }

        private int _SLHangTon;
        public int SLHangTon
        {
            get => _SLHangTon;
            set
            {
                if (_SLHangTon != value)
                {
                    _SLHangTon = value;
                    OnPropertyChanged(); // Cập nhật giao diện khi giá trị thay đổi
                }
            }
        }

        public SeriesCollection Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public List<string> Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        // code behis
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var LoginVM = loginWindow.DataContext as LoginViewModel;
                if (LoginVM.IsLogin)
                {
                    p.Show();
                    ThongKeDoanhThu();
                    SLHangNhap = CalculateSLHangNhap();
                    SLHangXuat = CalculateSLHangXuat();
                    SLHangTon = CalculateSLTon();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
                p.Show();
            });

            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            UnitCommand = new RelayCommand<object>((p) => true, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            PhieuNhapCommand = new RelayCommand<object>((p) => { return true; }, (p) => { PhieuNhapWindow wd = new PhieuNhapWindow(); wd.ShowDialog(); });
            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });

            DataProvider.Ins.DB.nhap_kho.Local.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                    e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    UpdateTonKho();
                }
            };

            DataProvider.Ins.DB.nhap_kho.Local.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                    e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    // Sử dụng Dispatcher để thực hiện cập nhật ngoài sự kiện
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UpdateTonKho();
                    });
                }
            };

            DataProvider.Ins.DB.xuat_Kho.Local.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                    e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    // Sử dụng Dispatcher để thực hiện cập nhật ngoài sự kiện
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UpdateTonKho();
                    });
                }
            };
        }

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();
            var objectlist = DataProvider.Ins.DB.vat_tu;
            int i = 1;

            foreach (var item in objectlist)
            {
                var inputList = DataProvider.Ins.DB.nhap_kho.Where(p => p.id_vat_tu == item.id);
                var outputList = DataProvider.Ins.DB.xuat_Kho
                    .Where(p => p.nhap_kho.id_vat_tu == item.id && (p.nhap_kho.gia_ban ?? 0) > 0); // Bỏ qua sản phẩm có OutputPrice = 0

                int suminput = 0;
                int sumoutput = 0;

                // Tính tổng số lượng nhập
                if (inputList != null && inputList.Any())
                {
                    suminput = inputList.Sum(p => p.so_luong ?? 0);
                }

                // Tính tổng số lượng xuất (chỉ các sản phẩm có giá xuất > 0)
                if (outputList != null && outputList.Any())
                {
                    sumoutput = outputList.Sum(p => p.Count ?? 0);
                }

                // Tạo đối tượng TonKho
                TonKho tonKho = new TonKho
                {
                    STT = i,
                    Count = suminput - sumoutput,
                    vat_tu = item
                };

                // Thêm vào danh sách
                TonKhoList.Add(tonKho);
                i++;
            }
        }

        // cap nhat ton kho load
        private void UpdateTonKho()
        {
            LoadTonKhoData();
            SLHangNhap = CalculateSLHangNhap();
            SLHangXuat = CalculateSLHangXuat();
            SLHangTon = CalculateSLTon();
        }
        // Hàm tính tổng số lượng hàng đã nhập
        public int CalculateSLHangNhap()
        {
            int suminput = 0;

            // Lấy danh sách các đối tượng từ cơ sở dữ liệu
            var objectList = DataProvider.Ins.DB.vat_tu.ToList();

            // Duyệt qua tất cả các đối tượng
            foreach (var item in objectList)
            {
                // Lấy danh sách các InputInfo theo IdObject của từng item
                var inputList = DataProvider.Ins.DB.nhap_kho.AsNoTracking() // Sử dụng AsNoTracking nếu không cần tracking
                                    .Where(p => p.id_vat_tu == item.id)
                                    .ToList();

                // Tính tổng số lượng
                suminput += inputList.Sum(p => p.so_luong ?? 0);
            }

            return suminput;
        }

        // Hàm tính tổng số lượng hàng đã xuất
        public int CalculateSLHangXuat()
        {
            int input = 0;

            // Lấy danh sách các đối tượng từ cơ sở dữ liệu
            var objectList = DataProvider.Ins.DB.vat_tu.ToList();

            // Duyệt qua tất cả các đối tượng
            foreach (var item in objectList)
            {
                // Lấy danh sách các OutputInfo theo IdObject của từng item
                var inputList = DataProvider.Ins.DB.xuat_Kho
                    .Where(p => p.nhap_kho.id_vat_tu == item.id && (p.nhap_kho.gia_ban ?? 0) > 0) // Bỏ qua nếu OutputPrice = 0
                    .ToList();

                // Kiểm tra nếu inputList không null và có dữ liệu
                if (inputList != null && inputList.Any())
                {
                    input += inputList.Sum(p => p.Count ?? 0);
                }
            }

            return input;
        }

        // Hàm tính tổng số lượng hàng tồn
        public int CalculateSLTon()
        {
            int sumInput = 0;
            int sumOutput = 0;
            int count;

            // Lấy danh sách các đối tượng từ cơ sở dữ liệu
            var objectList = DataProvider.Ins.DB.vat_tu.ToList();

            foreach (var item in objectList)
            {
                // Lấy danh sách các InputInfo theo IdObject của từng item
                var inputList = DataProvider.Ins.DB.nhap_kho
                                    .AsNoTracking()
                                    .Where(p => p.id_vat_tu == item.id)
                                    .ToList();

                var outputList = DataProvider.Ins.DB.xuat_Kho
                                     .AsNoTracking()
                                     .Where(p => p.nhap_kho.id_vat_tu == item.id)
                                     .ToList();

                // Tính tổng nhập
                if (inputList != null && inputList.Any())
                {
                    sumInput += inputList.Sum(p => p.so_luong ?? 0);
                }

                // Tính tổng xuất
                if (outputList != null && outputList.Any())
                {
                    sumOutput += outputList.Sum(p => p.Count ?? 0);
                }
            }

            // Tính số lượng tồn
            count = sumInput - sumOutput;

            return count;
        }

        public void ThongKeDoanhThu()
        {
            var outputInfoList = DataProvider.Ins.DB.xuat_Kho
                .Where(output => output.id != null && output.nhap_kho.gia_ban > 0) // Bỏ qua OutputInfo có giá xuất bằng 0 hoặc null
                .ToList();

            var objectList = DataProvider.Ins.DB.vat_tu.ToList();

            // Doanh thu theo tháng
            var revenueByMonth = outputInfoList
                .Join(objectList, // Kết hợp với bảng Object theo IdObject
                      output => output.nhap_kho.id_vat_tu, // Khóa trong OutputInfo
                      obj => obj.id, // Khóa trong Object
                      (output, obj) => new { output, obj }) // Kết hợp OutputInfo và Object
                .GroupBy(data => data.output.ngay_xuat.Value.ToString("yyyy-MM")) // Gom nhóm theo tháng (yyyy-MM)
                .Select(group => new
                {
                    Month = group.Key, // Tháng (dạng chuỗi yyyy-MM)
                    Revenue = group.Sum(data => (data.output.Count ?? 0) * (data.output.nhap_kho.gia_nhap ?? 0)) // Tính tổng doanh thu theo tháng
                })
                .OrderBy(data => data.Month) // Sắp xếp theo tháng
                .ToList();

            // Doanh thu theo năm
            var revenueByYear = outputInfoList
                .Join(objectList, // Kết hợp với bảng Object theo IdObject
                      output => output.nhap_kho.id_vat_tu, // Khóa trong OutputInfo
                      obj => obj.id, // Khóa trong Object
                      (output, obj) => new { output, obj }) // Kết hợp OutputInfo và Object
                .GroupBy(data => data.output.ngay_xuat.Value.Year) // Gom nhóm theo năm
                .Select(group => new
                {
                    Year = group.Key, // Năm
                    Revenue = group.Sum(data => (data.output.Count ?? 0) * (data.output.nhap_kho.gia_ban ?? 0)) // Tính tổng doanh thu của năm
                })
                .OrderBy(data => data.Year) // Sắp xếp theo năm
                .ToList();

            // Tạo Series cho biểu đồ với 2 cột (1 cột cho tháng, 1 cột cho năm)
            Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Doanh Thu Theo Tháng",
            Values = new ChartValues<double>(revenueByMonth.Select(data => data.Revenue)) // Doanh thu theo tháng
        },
        new ColumnSeries
        {
            Title = "Doanh Thu Theo Năm",
            Values = new ChartValues<double>(revenueByYear.Select(data => data.Revenue)) // Doanh thu theo năm
        }
    };

            // Gán nhãn cho trục X (tháng và năm)
            Labels = revenueByMonth.Select(data => data.Month).ToList(); // Nhãn cho tháng
                                                                         // Thêm nhãn cho năm nếu cần
            Labels.AddRange(revenueByYear.Select(data => "Năm " + data.Year));
        }


    }
}
