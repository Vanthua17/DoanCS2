   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PMQuanyKho.ViewModelPMQuanyKho.ViewModel  5
   	   )clr-namespace:PMQuanLyKhoPMQuanLyKho  5   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.UserWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   2	viewmodel"clr-namespace:PMQuanyKho.ViewModel  5
   	   %localclr-namespace:PMQuanLyKho  5   	   /uc&clr-namespace:PMQuanLyKho.UserControlT  5   	   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      NguoiDungVM � DataContext# [  5   	    � Title$ Người dùng��5   	   $	��450��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6N   5      � 5    
   $��1��6    5!      �� 5"      $
��auto �6    60   �� 5#      $
��auto �6    60   �� 5$      6"   .+�5(      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5)      � $��0 0 0 10q�6   .+�5*      �� $��Center=�6c   $��5 5 15 5q�6~   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Tên đăng nhập��6�    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *ten_dang_nhap+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5+      $��Center=�6^   $��5 5 15 5q�6y   $	 Mật khẩu��6�   #��[ 6�   ��6   �� *
mat_khau+$
 PropertyChanged=�6�   �� 5,      $��5 5 5 -3q�6    �� DisplayMemberPath$ 	ten_quyen��5-      $	 Chức vụ��50      .  (MaterialDesignThemes.Wpf.TextFieldAssist   
PrefixText$  ��51       �� 
IsEditable . ��5.      �� *
UserRole+ �� SelectedItem 5/      �� *SelectedUserRole+ �� Mode$ TwoWay=�$
 PropertyChanged=�52      �� 54      $��Center=�6d   $��5 5 15 5q�6   $	 Tên hiển thị��6�   #��[ 6�   ��6   �� *ten_nguoi_dung+$
 PropertyChanged=�6�   (  "System.Windows.Controls.DatePicker  55      $	��100��6!   $��Center=�57      $��5 5 15 5q�58      $	 Ngày tạo��59         HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 5;         SelectedDate 56      �� *
ngay_tao+ �� StringFormat$ 
dd/MM/yyyy��5;      5=      5>        5B      $��1��6"   $��5q�6/   .��5C      � .+�5D      �� $��Thêm��6<   $��5q�6K   $	��120��6V   ��6   �� *
AddCommand+6d   �� 5E      $��Sửa��6=   $��5q�6K   $	��120��6V   ��6   �� *EditCommand+6d   �� 5F      $
��Xóa��6?   $��5q�6M   $	��120��6X   ��6   �� *DeleteCommand+6f   5G      5H        5J      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5K      �� $
��Auto=�6   .��5L      � .+�5M      �� ��6#   �� *List+ 6@   �� *SelectedItem+$ TwoWay=� �� View 5N      �� 5O   "   .P�5P   &   �� $	��STT��65    �� DisplayMemberBinding 6B   �� *id+6h   �� 5Q   &   $��Tên đăng nhập��65    6L   �� *ten_dang_nhap+6}   �� 5S   &   $��Chức vụ��65    6F   �� *quyen_nguoi_dung.ten_quyen+6�   �� 5T   &   $��tên hiển thị��65    6K   �� *ten_nguoi_dung+6}   �� 5U   &   $��Ngày cập nhật��65    6L   �� *
ngay_tao+$ 
dd/MM/yyyy��6�   5V   #   5X      5Y      5Z      5[      5]      5^      5_      