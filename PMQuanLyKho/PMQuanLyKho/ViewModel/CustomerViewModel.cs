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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyKho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<khach_hang> _List;
        public ObservableCollection<khach_hang> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private khach_hang _SelectedItem;
        public khach_hang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    ten_khach_hang = SelectedItem.ten_khach_hang;
                    so_dien_thoai = SelectedItem.so_dien_thoai;
                    email = SelectedItem.email;
                    dia_chi = SelectedItem.dia_chi;
                    mo_ta = SelectedItem.mo_ta;
                    ngay_tao = SelectedItem.ngay_tao;
                }
            }
        }

        private string _ten_khach_hang;
        public string ten_khach_hang { get => _ten_khach_hang; set { _ten_khach_hang = value; OnPropertyChanged(); } }
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
        public ICommand PDFCommand { get; set; }
        public ICommand ExcelCommand { get; set; }
        public CustomerViewModel()
        {
            List = new ObservableCollection<khach_hang>(DataProvider.Ins.DB.khach_hang);

            // them dvd
            AddCommand = new RelayCommand<object>((p) =>
            {
                // Kiểm tra điều kiện để thêm: các trường không được để trống
                return !string.IsNullOrEmpty(ten_khach_hang) &&
                       !string.IsNullOrEmpty(so_dien_thoai) &&
                       !string.IsNullOrEmpty(dia_chi) &&
                       !string.IsNullOrEmpty(email);
            }, (p) =>
            {
                try
                {
                    // Tạo đối tượng mới từ dữ liệu trong ViewModel
                    var Customer = new khach_hang()
                    {
                        ten_khach_hang = ten_khach_hang,
                        so_dien_thoai = so_dien_thoai,
                        dia_chi = dia_chi,
                        email = email,
                        mo_ta = mo_ta,
                        ngay_tao = ngay_tao ?? DateTime.Now // Sử dụng ngay_tao mặc định nếu null
                    };

                    // Thêm đối tượng vào cơ sở dữ liệu
                    DataProvider.Ins.DB.khach_hang.Add(Customer);
                    DataProvider.Ins.DB.SaveChanges();

                    // Cập nhật danh sách hiển thị
                    RefreshData();

                    // Hiển thị thông báo thành công
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

                var displayList = DataProvider.Ins.DB.khach_hang.Where(x => x.id == SelectedItem.id);
                if (displayList == null || displayList.Count() != 0) return true;

                return false;
            }, (p) =>
            {
                var khachhang = DataProvider.Ins.DB.khach_hang.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                khachhang.ten_khach_hang = ten_khach_hang;
                khachhang.so_dien_thoai = so_dien_thoai;
                khachhang.dia_chi = dia_chi;
                khachhang.email = email;
                khachhang.mo_ta = mo_ta;
                khachhang.ngay_tao = ngay_tao ?? DateTime.Now;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.ten_khach_hang = ten_khach_hang;
            });
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
                    var customerToDelete = DataProvider.Ins.DB.khach_hang.Where(x => x.id == SelectedItem.id).SingleOrDefault();

                    if (customerToDelete != null)
                    {
                        // Xóa đối tượng khỏi cơ sở dữ liệu
                        DataProvider.Ins.DB.khach_hang.Remove(customerToDelete);
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
            InitializeCommands();
        }
        private void RefreshData()
        {
            // Tải lại danh sách từ cơ sở dữ liệu
            List = new ObservableCollection<Model.khach_hang>(DataProvider.Ins.DB.khach_hang.ToList());
        }

        private void InitializeCommands()
        {
            PDFCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
                (p) => ExportToPDF());

            ExcelCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
                (p) => ExportToExcel());
        }

        public void ExportToPDF()
        {
            try
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Chưa chọn người dùng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var customerPurchases = DataProvider.Ins.DB.xuat_Kho
                    .Where(r => r.id_khach_hang == SelectedItem.id)
                    .ToList();

                if (!customerPurchases.Any())
                {
                    MessageBox.Show("Khách hàng này chưa mua sản phẩm nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Tạo file PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Danh sách sản phẩm khách hàng đã mua";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);

                double x = 40;
                double y = 40;

                gfx.DrawString($"Khách hàng: {SelectedItem.ten_khach_hang}", font, XBrushes.Black, x, y);
                y += 20;
                gfx.DrawString("Tên sản phẩm", font, XBrushes.Black, x, y);
                gfx.DrawString("Số lượng", font, XBrushes.Black, x + 200, y);
                y += 20;

                int totalQuantity = 0;

                foreach (var purchase in customerPurchases)
                {
                    var product = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(p => p.id == purchase.id_nhap_kho);
                    if (product != null)
                    {
                        gfx.DrawString(product.vat_tu.ten_vat_tu, font, XBrushes.Black, x, y);
                        gfx.DrawString(purchase.Count.ToString(), font, XBrushes.Black, x + 200, y);
                        totalQuantity += (int)purchase.Count;
                        y += 20;
                    }
                }

                gfx.DrawString($"Tổng số lượng: {totalQuantity}", font, XBrushes.Black, x, y);

                // Lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    document.Save(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file PDF thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExportToExcel()
        {
            try
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Chưa chọn người dùng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var customerPurchases = DataProvider.Ins.DB.xuat_Kho
                    .Where(x => x.id_khach_hang == SelectedItem.id)
                    .ToList();

                if (!customerPurchases.Any())
                {
                    MessageBox.Show("Khách hàng này chưa mua sản phẩm nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Danh sách sản phẩm");

                    worksheet.Cells[1, 1].Value = "Tên sản phẩm";
                    worksheet.Cells[1, 2].Value = "Số lượng";
                    worksheet.Cells[1, 3].Value = "Ngày xuất";

                    int row = 2;
                    int totalQuantity = 0;

                    foreach (var purchase in customerPurchases)
                    {
                        var product = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(p => p.id == purchase.id_nhap_kho);
                        if (product != null)
                        {
                            worksheet.Cells[row, 1].Value = product.vat_tu.ten_vat_tu;
                            worksheet.Cells[row, 2].Value = purchase.Count;
                            worksheet.Cells[row, 3].Value = purchase.ngay_xuat?.ToString("dd/MM/yyyy");
                            totalQuantity += (int)purchase.Count;
                            row++;
                        }
                    }

                    worksheet.Cells[row, 1].Value = "Tổng số lượng";
                    worksheet.Cells[row, 2].Value = totalQuantity;

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files (*.xlsx)|*.xlsx"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fileInfo);
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
