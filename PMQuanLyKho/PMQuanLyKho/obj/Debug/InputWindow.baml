   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PMQuanyKho.ViewModelPMQuanyKho.ViewModel  5   	   )clr-namespace:PMQuanLyKhoPMQuanLyKho  5
   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.InputWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   2	viewmodel"clr-namespace:PMQuanyKho.ViewModel  5   	   %localclr-namespace:PMQuanLyKho  5
   	   /uc&clr-namespace:PMQuanLyKho.UserControlT  5   	   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      	NhapKhoVM � DataContext# [  5   
    � Title$ 
Nhập kho��5   	   $	��500��6   $
��1020��6'   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6N   5      � 5   
   $��1��6    5       �� 5!      $
��auto �6    60   �� 5"      $
��auto �6    60   �� 5#      6"   .+�5'      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5(      � $��0 0 0 10q�6   .+�5*      �� $��5,5,5,-3q�6   	 �� DisplayMemberPath$	 ten_loai��5+      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Loại vật tư��5.       �� 
IsEditable . ��5,      �� *loai+ �� SelectedItem 5-      �� *Selectedloai+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5/      �� 51      $��5,5,5,-3q�6   $	 
ten_vat_tu��54      $
 Loại vật tư��55       . ��52      �� *vat_tu+ 53      �� *selectedvat_tu+$ TwoWay=�56      �� 58      $��5 5 5 -3q�6   $	 	ten_quyen��59      $
 Chức vụ��5<      .  (MaterialDesignThemes.Wpf.TextFieldAssist   
PrefixText$  ��5=       . ��5:      �� *quyen_nguoi_dung+ 5;      �� *SelectedUserRole+$ TwoWay=�$ PropertyChanged=�5>      (  "System.Windows.Controls.DatePicker  5A      $	��100��6!   $��Center=�5C      $��5 5 15 5q�5D      $
 Ngày nhập��5E         HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 5G         SelectedDate 5B      �� *	ngay_nhap+ �� StringFormat$ 
dd/MM/yyyy��5G      �� 5I      $��Center=�5J      $��5 5 15 5q�5K      $
 Số lượng��5L       % !MaterialDesignFloatingHintTextBox#��[ 5M      ��5I      �� *
so_luong+$ PropertyChanged=�5M      �� 5O      $��Center=�6^   $��5 5 15 5q�6y   $
 Giá nhập��6�   #��[ 6�   ��6   �� *
gia_nhap+$ PropertyChanged=�6�   �� 5P      $��Center=�6]   $��5 5 15 5q�6x   $
 
Giá xuát��6�   #��[ 6�   ��6   �� *	gia_ban+$ PropertyChanged=�6�   �� 5Q      $��Center=�6[   $��5 5 15 5q�6v   $
 Mô tả��6�   #��[ 6�   ��6   �� *mo_ta+$ PropertyChanged=�6�   5S      5T        5X      $��1��6"   $��5q�6/   .��5Y      � .+�5Z      �� $��Thêm��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 5[      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 5\      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   5]      5^        5`      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5a      �� $
��Auto=�6   .��5b      � .+�5c      �� ��6#   �� *List+ 6@   �� *SelectedItem+$ TwoWay=� �� View 5d      �� 5e   "   .P�5f   &   ��  �� Width$	 150��65   $��Loại��6A    �� DisplayMemberBinding 6O   �� *loai.ten_loai+�� 5g   &   $	 150��65   $��Tên vật tư��6A    6U   �� *vat_tu.ten_vat_tu+�� 5h   &   $��Chức vụ��65    6F   �� *'%nguoi_dung.quyen_nguoi_dung.ten_quyen+6�   �� 5i   &   $��Ngày nhập��65    6H   �� *	ngay_nhap+$ 
dd/MM/yyyy��6�   �� 5j   &   $��Số lượng��65    6G   �� *
so_luong+6s   �� 5k   &   $��Giá nhập��65    6G   �� *
gia_nhap+6s   �� 5l   &   $��Giá xuất��65    6G   �� *	gia_ban+6r   �� 5m   &   $��Mô tả��65    6D   �� *mo_ta+6m   5n   #   5p      5q      5r      5s      5u      5v      5w      