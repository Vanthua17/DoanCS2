   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PMQuanyKho.ViewModelPMQuanyKho.ViewModel  5   	   )clr-namespace:PMQuanLyKhoPMQuanLyKho  5
   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.OutputWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   2	viewmodel"clr-namespace:PMQuanyKho.ViewModel  5   	   %localclr-namespace:PMQuanLyKho  5
   	   /uc&clr-namespace:PMQuanLyKho.UserControlT  5   	   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      	XuatKhoVM � DataContext# [  5   	    � Title$ 
Xuất kho��5   	   $	��500��6   $
��1020��6'   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6N   5      � 5   
   $��1��6    5      �� 5       $
��auto �6    60   �� 5!      $
��auto �6    60   �� 5"      6"   .+�5&      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5'      � $��0 0 0 10q�6   .+�5)      �� $��5 5 5 -3q�6   	 �� DisplayMemberPath$	 vat_tu.ten_vat_tu��5*      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Tên Vật tư��5-      .  (MaterialDesignThemes.Wpf.TextFieldAssist   
PrefixText$  ��5.       �� 
IsEditable . ��5+      �� *
nhap_kho+ �� SelectedItem 5,      �� *SelectedInputInfo+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5/      �� 51      $��Center=�52      $��5 5 15 5q�53      $
 
Mã xuất��54       % !MaterialDesignFloatingHintTextBox#��[ 55      ��51      �� *	id_hoadon+$ TwoWay=�$ PropertyChanged=�55      �� 5:      $��5 5 5 -3q�6   $	 ten_nguoi_dung��5;      $
 Tên nhân viên��5>      $  ��5?       . ��5<      �� *
nguoi_dung+ 5=      �� *Selectednguoi_dung+$ TwoWay=�$ PropertyChanged=�5@      (  "System.Windows.Controls.DatePicker  5C      $	��100��6!   $��Center=�5E      $��5 5 15 5q�5F      $
 Ngày xuất��5G         HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 5I         SelectedDate 5D      �� *	ngay_xuat+ �� StringFormat$ 
dd/MM/yyyy��5I      �� 5L      $��Center=�5M      $��5 5 15 5q�5N      $
 Số lượng��5O      #��[ 5P      ��5L      �� *Count+$ TwoWay=�$ PropertyChanged=�5P      �� 5R      $��Center=�5S      $��5 5 15 5q�5T       �� 
IsReadOnly . $
 Giá xuất��5V      #��[ 5W      ��5R      �� *SelectedInputInfo.gia_ban+$ TwoWay=�$ PropertyChanged=� �� TargetNullValue$ 0��5W      �� 5Y      $��5 5 5 -3q�6   $	 ten_khach_hang��5Z      $
 Khách hàng��5]      $  ��5^       . ��5[      �� *
khach_hang+ 5\      �� *Selectedkhach_hang+$ TwoWay=�$ PropertyChanged=�5_      �� 5a      $��Center=�5b      $��5 5 15 5q�5c      $
 Trạng thái nhập��5d      #��[ 5e      ��5a      �� *mota+$ TwoWay=�$ PropertyChanged=�5e      5f      5h        5l      $��1��6"   $��5q�6/   .��5m      � .+�5n      �� $��Xuất��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 5o      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 5p      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   �� 5q      $��5 0 0 0q�6   $��Xuất file PDF��6M   $	��120��6e   ��6.   �� *
PDFCommand+6e   �� 5r      $��10 0 0 0q�6   $��Xuất file Excel��6P   $	��120��6j   ��6/   �� *ExcelCommand+6j   5s      5t        5v      $��2��6"   $��5,5,0,5q�6/   .��5w      �� $
��Auto=�6   .��5x      � .+�5y      �� ��6#   �� *List+$ TwoWay=�$ PropertyChanged=� 5z      �� *SelectedItem+$ TwoWay=� �� View 5{      �� 5|   "   .P�5~   &   ��  �� Width$	 130��65   $��Tên vật tư��6A    �� DisplayMemberBinding 6U   �� *nhap_kho.vat_tu.ten_vat_tu+�� 5�   &   $	 130��65   $��
Mã xuất��6A    6R   �� *	id_hoadon+�� 5�   &   $��Tên nhân viên��65    6L   �� *nguoi_dung.ten_nguoi_dung+�� 5�   &   $��Ngày xuất��65    6H   �� *	ngay_xuat+�� 5�   &   $��Số lượng��65    6G   �� *Count+�� 5�   &   $��Giá xuất��65    6G   �� *nhap_kho.gia_ban+�� 5�   &   $��Khách hàng��65    6I   �� *khach_hang.ten_khach_hang+�� 5�   &   $	 300��65   $��Mô tả��6A    6P   �� *mo_ta+5�   #   5�      5�      5�      5�      5�      5�      5�      