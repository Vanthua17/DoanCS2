using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PMQuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;

namespace PMQuanLyKho.ViewModel
{
    public class XuatKhoViewModel : BaseViewModel
    {
        private ObservableCollection<Model.xuat_Kho> _List;
        public ObservableCollection<Model.xuat_Kho> List
        {
            get => _List;
            set { _List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.nhap_kho> _nhap_kho;
        public ObservableCollection<Model.nhap_kho> nhap_kho
        {
            get => _nhap_kho;
            set { _nhap_kho = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.nguoi_dung> _nguoi_dung;
        public ObservableCollection<Model.nguoi_dung> nguoi_dung
        {
            get => _nguoi_dung;
            set { _nguoi_dung = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Model.khach_hang> _khach_hang;
        public ObservableCollection<Model.khach_hang> khach_hang
        {
            get => _khach_hang;
            set { _khach_hang = value; OnPropertyChanged(); }
        }

        private Model.xuat_Kho _SelectedItem;
        public Model.xuat_Kho SelectedItem
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
                        Count = SelectedItem.Count;
                        ngay_xuat = SelectedItem.ngay_xuat;
                        mota = SelectedItem.mo_ta;
                        id_hoadon = SelectedItem.id_hoadon;
                        // Ensure nhap_kho and nguoi_dung are not null
                        Selectedkhach_hang = khach_hang.FirstOrDefault(x => x.id == SelectedItem.id_khach_hang);
                        SelectedInputInfo = nhap_kho.FirstOrDefault(x => x.id == SelectedItem.id_nhap_kho);
                        Selectednguoi_dung = nguoi_dung.FirstOrDefault(x => x.id == SelectedItem.id_nguoi_dung);
                        
                    }
                }
            }
        }

        private Model.nhap_kho _SelectedInputInfo;
        public Model.nhap_kho SelectedInputInfo
        {
            get => _SelectedInputInfo;
            set { _SelectedInputInfo = value; OnPropertyChanged(); }
        }

        private Model.khach_hang _Selectedkhach_hang;
        public Model.khach_hang Selectedkhach_hang
        {
            get => _Selectedkhach_hang;
            set { _Selectedkhach_hang = value; OnPropertyChanged(); }
        }

        private Model.nguoi_dung _Selectednguoi_dung;
        public Model.nguoi_dung Selectednguoi_dung
        {
            get => _Selectednguoi_dung;
            set { _Selectednguoi_dung = value; OnPropertyChanged(); }
        }
        private int? _Count;
        public int? Count
        {
            get => _Count;
            set { _Count = value; OnPropertyChanged(); }
        }

        private DateTime? _ngay_xuat;
        public DateTime? ngay_xuat
        {
            get => _ngay_xuat;
            set { _ngay_xuat = value; OnPropertyChanged(); }
        }

        private string _mota;
        public string mota
        {
            get => _mota;
            set { _mota = value; OnPropertyChanged(); }
        }

        private string _id_hoadon;
        public string id_hoadon
        {
            get => _id_hoadon;
            set { _id_hoadon = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PDFCommand { get; set; }
        public ICommand ExcelCommand { get; set; }

        public XuatKhoViewModel()
        {
            // Khởi tạo các ObservableCollection
            List = new ObservableCollection<Model.xuat_Kho>(DataProvider.Ins.DB.xuat_Kho);
            nhap_kho = new ObservableCollection<Model.nhap_kho>(DataProvider.Ins.DB.nhap_kho);
            khach_hang = new ObservableCollection<Model.khach_hang>(DataProvider.Ins.DB.khach_hang);
            nguoi_dung = new ObservableCollection<Model.nguoi_dung>(DataProvider.Ins.DB.nguoi_dung);
            // Initialize commands
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand<object>(
                (p) => true,
                (p) => AddItem());

            EditCommand = new RelayCommand<object>(
                (p) => SelectedItem != null && IsValidEdit(),
                (p) => EditItem());

            PDFCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
                (p) => ExportToPDF());

            ExcelCommand = new RelayCommand<object>(
                (p) => SelectedItem != null,
                (p) => ExportToExcel());
        }

        private void AddItem()
        {
            try
            {
                if (Selectedkhach_hang == null || SelectedInputInfo == null || !Count.HasValue || Count.Value <= 0)
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newItem = new Model.xuat_Kho
                {
                    id = Guid.NewGuid().ToString(),
                    id_khach_hang = Selectedkhach_hang.id,
                    id_nguoi_dung = Selectednguoi_dung.id,
                    id_hoadon = id_hoadon, 
                    id_nhap_kho = SelectedInputInfo.id,
                    Count = Count,
                    ngay_xuat = ngay_xuat ?? DateTime.Now,
                    mo_ta = mota
                };

                var inputInfo = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(i => i.id == SelectedInputInfo.id);
                if (inputInfo != null && Count.HasValue && Count.Value <= inputInfo.so_luong)
                {
                    inputInfo.so_luong -= Count.Value;
                    DataProvider.Ins.DB.xuat_Kho.Add(newItem);
                    DataProvider.Ins.DB.SaveChanges();
                    RefreshData();
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool IsValidEdit()
        {
            if (SelectedItem == null || !Count.HasValue) return false;
            var inputInfo = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(i => i.id == SelectedInputInfo.id);
            return inputInfo != null && Count.Value <= inputInfo.so_luong && Count.Value >= 0;
        }

        private void EditItem()
        {
            try
            {
                var itemToEdit = DataProvider.Ins.DB.xuat_Kho.SingleOrDefault(x => x.id == SelectedItem.id);
                if (itemToEdit != null)
                {
                    itemToEdit.id_khach_hang = Selectedkhach_hang.id;
                    itemToEdit.id_nhap_kho = SelectedInputInfo.id;
                    itemToEdit.id_hoadon = id_hoadon;
                    itemToEdit.id_nguoi_dung = Selectednguoi_dung.id;
                    itemToEdit.Count = Count;
                    itemToEdit.ngay_xuat = ngay_xuat ?? DateTime.Now;
                    itemToEdit.mo_ta = mota;

                    DataProvider.Ins.DB.SaveChanges();
                    RefreshData();
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToPDF()
        {
            try
            {
                if (Selectedkhach_hang == null)
                {
                    MessageBox.Show("Chưa chọn khách hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Retrieve all the purchase history for the selected customer
                var customerPurchases = DataProvider.Ins.DB.xuat_Kho
                    .Where(b => b.id_khach_hang == Selectedkhach_hang.id)
                    .ToList();

                if (!customerPurchases.Any())
                {
                    MessageBox.Show("Không có lịch sử mua hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Lịch sử mua hàng của khách hàng " + Selectedkhach_hang.ten_khach_hang;

                // Add a page
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12);

                // Set up for printing the table of purchases
                double x = 40;
                double y = 40;

                // Write Title
                gfx.DrawString("Lịch sử mua hàng của khách hàng: " + Selectedkhach_hang.ten_khach_hang, font, XBrushes.Black, x, y);
                y += 20;

                // Write table headers with adjusted column spacing
                gfx.DrawString("Mặt hàng", font, XBrushes.Black, x, y);
                gfx.DrawString("Số lượng", font, XBrushes.Black, x + 160, y);  // Reduced space for quantity
                gfx.DrawString("Ngày xuất", font, XBrushes.Black, x + 240, y);  // Adjusted "Ngày xuất" position
                gfx.DrawString("Giá trị", font, XBrushes.Black, x + 400, y);    // Adjusted "Giá trị" position
                y += 20;

                // Variable to hold total sum
                double totalSum = 0;

                // Write each item
                foreach (var purchase in customerPurchases)
                {
                    var product = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(b => b.id == purchase.id_nhap_kho);
                    if (product != null)
                    {
                        // Calculate item total: quantity * price
                        double itemTotal = (double)(purchase.Count * purchase.nhap_kho.gia_ban);

                        // Add item total to the total sum
                        totalSum += itemTotal;

                        // Printing each field for each purchase
                        gfx.DrawString(product.vat_tu.ten_vat_tu, font, XBrushes.Black, x, y);
                        gfx.DrawString(purchase.Count.ToString(), font, XBrushes.Black, x + 160, y);
                        gfx.DrawString(purchase.ngay_xuat.ToString(), font, XBrushes.Black, x + 240, y); // Date format for clarity
                        gfx.DrawString(itemTotal.ToString(), font, XBrushes.Black, x + 400, y); // Show raw value
                        y += 20;
                    }
                }


                // Write the total sum at the end
                gfx.DrawString("Tổng tiền: " + totalSum.ToString("C"), font, XBrushes.Black, x, y);

                // Save the document to a file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    document.Save(filePath);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ExportToExcel()
        {
            try
            {
                if (Selectedkhach_hang == null)
                {
                    MessageBox.Show("Chưa chọn khách hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Retrieve all the purchase history for the selected customer
                var customerPurchases = DataProvider.Ins.DB.xuat_Kho
                    .Where(x => x.id_khach_hang == Selectedkhach_hang.id)
                    .ToList();

                if (!customerPurchases.Any())
                {
                    MessageBox.Show("Không có lịch sử mua hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a new Excel package
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Lịch sử mua hàng");
                    worksheet.Cells[1, 1].Value = "Mặt hàng";
                    worksheet.Cells[1, 2].Value = "Số lượng";
                    worksheet.Cells[1, 3].Value = "Tên khách hàng";
                    worksheet.Cells[1, 4].Value = "Ngày xuất";
                    worksheet.Cells[1, 5].Value = "Tổng";

                    int row = 2;
                    double totalSum = 0;
                    foreach (var purchase in customerPurchases)
                    {
                        var product = DataProvider.Ins.DB.nhap_kho.FirstOrDefault(x => x.id == purchase.id_nhap_kho);
                        if (product != null)
                        {
                            double itemTotal = (double)(purchase.Count * purchase.nhap_kho.gia_ban);

                            // Add item total to the total sum
                            totalSum += itemTotal;

                            worksheet.Cells[row, 1].Value = product.vat_tu.ten_vat_tu;
                            worksheet.Cells[row, 2].Value = purchase.Count;
                            worksheet.Cells[row, 3].Value = purchase.khach_hang.ten_khach_hang;
                            worksheet.Cells[row, 4].Value = purchase.ngay_xuat.ToString();
                            worksheet.Cells[row, 5].Value = itemTotal.ToString();
                            row++;
                        }
                    }

                    // Write total sum at the end
                    worksheet.Cells[row, 4].Value = "Tổng tiền:";
                    worksheet.Cells[row, 5].Value = totalSum.ToString();

                    // Format header row
                    using (var headerRange = worksheet.Cells[1, 1, 1, 5])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // AutoFit all columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Save to file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
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


        private void RefreshData()
        {
            // Tải lại danh sách từ cơ sở dữ liệu
            List = new ObservableCollection<xuat_Kho>(DataProvider.Ins.DB.xuat_Kho.ToList());
        }
    }
}
