   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PMQuanyKho.ViewModelPMQuanyKho.ViewModel  5   	   )clr-namespace:PMQuanLyKhoPMQuanLyKho  5
   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.ObjectWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   2	viewmodel"clr-namespace:PMQuanyKho.ViewModel  5   	   %localclr-namespace:PMQuanLyKho  5
   	   /uc&clr-namespace:PMQuanLyKho.UserControlT  5   	   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      ObjectVM � DataContext# [  5   
    � Title$ 	Vật tư��5   	   $	��450��6   $
��1000��6%   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6N   5      � 5   
   $��1��6    5       �� 5!      $
��auto �6    60   �� 5"      $
��auto �6    60   �� 5#      $
��auto �6    60   �� 5$      6"   .+�5(      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5)      � $��0 0 0 10q�6   .+�5+      �� $��5 5 5 -3q�5,      	 �� DisplayMemberPath$	 ten_loai��5-      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Loại��50       �� 
IsEditable . ��5.      �� *loai+ �� SelectedItem 5/      �� *Selectedloai+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�51      �� 54      $��Center=�56      $��5 5 15 5q�57      $
 Tên vật tư��58       % !MaterialDesignFloatingHintTextBox#��[ 59      ��55      �� *
ten_vat_tu+$ PropertyChanged=�59      �� 5<      $��5 5 5 -3q�5=      $	 
ten_don_vi��5>      $
 Đơn vị đo��5A       . ��5?      �� *don_vi+ 5@      �� *SelectedUnit+$ TwoWay=�$ PropertyChanged=�5B      �� 5E      $��5 5 5 -3q�5F      $	 	ten_nhacc��5G      $
 Nhà cung cấp��5J       . ��5H      �� *nha_cung_cap+ 5I      �� *Selectednha_cung_cap+$ TwoWay=�$ PropertyChanged=�5K      �� 5N      $��Center=�5P      $��5 5 15 5q�5Q      $
 QR Code��5R      #��[ 5S      ��5O      �� *QRCode+$ PropertyChanged=�5S      �� 5V      $��Center=�5X      $��5 5 15 5q�5Y      $
 Bar Code��5Z      #��[ 5[      ��5W      �� *	BarCode+$ PropertyChanged=�5[      (  "System.Windows.Controls.DatePicker  5^      $	��100��5_      $��Center=�5a      $��5 5 15 5q�5b       � Language$ vi-VN�5c      $
 Ngày tạo��5d      .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 5f         SelectedDate 5`      �� *
ngay_tao+5f      �� 5i      $��Center=�5k      $��5 5 15 5q�5l      $
 Mô tả��5m      #��[ 5n      ��5j      �� *mo_ta+$ PropertyChanged=�5n      5o      5p        5t      $��1��6"   $��5q�6/   .��5u      � .+�5v      �� $��Thêm��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 5w      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 5x      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   5y      5z      � 5{      $��2��6   .+�5|      � $��0��6   $��5q�6$    � ColumnDefinitions 5}      �� 5~      $��* �6+   67   �� 5      $
��auto �6+   6:   .+�5�      � .+�5�      �� $��Center=�5�      $��5q�69   $
 Tìm Kiếm��5�      #��[ 5�      ��5�   "   �� *SearchKeyword+$ PropertyChanged=�$ TwoWay=�5�      5�      � 5�      $��1��6   .+�5�      �� $��Lọc��6C   $��5q�6Q   $	��100��6\   ��6!   �� *SearchCommand+6j   5�      5�      5�        5�      $��3��6"   $��5q�6/   $��Stretch=�6:   .��5�      �� $
��Auto=�6   .��5�      � .+�5�      �� ��6#   �� *List+ 6A   �� *SelectedItem+$ TwoWay=�$ PropertyChanged=� �� View 6�   �� 5�   "   .P�5�   &   �� $��Loại��65    �� DisplayMemberBinding 6C   �� *loai.ten_loai+�� 5�   &   $��	Vật tư��65    6E   �� *
ten_vat_tu+�� 5�   &   $��Đơn vị đo��65    6H   �� *don_vi.ten_don_vi+�� 5�   &   $��Nhà cung cấp��65    6K   �� *nha_cung_cap.ten_nhacc+�� 5�   &   $��QRCode��65    6E   �� *QRCode+�� 5�   &   $��BarCode��65    6F   �� *	BarCode+�� 5�   &   $��Ngày tạo��65    6G   �� *
ngay_tao+ �� StringFormat$ 
dd/MM/yyyy���� 5�   &   $��Mô tả��65    6D   �� *mo_ta+5�   #   5�      5�      5�      5�      5�      5�      5�      