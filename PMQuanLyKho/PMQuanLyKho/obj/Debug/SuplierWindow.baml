   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PMQuanyKho.ViewModelPMQuanyKho.ViewModel  5   	   )clr-namespace:PMQuanLyKhoPMQuanLyKho  5
   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.SuplierWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   2	viewmodel"clr-namespace:PMQuanyKho.ViewModel  5   	   %localclr-namespace:PMQuanLyKho  5
   	   /uc&clr-namespace:PMQuanLyKho.UserControlT  5   	   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	      	SuplierVM � DataContext# [  5   
    � Title$ Nhà cung cấp��5   	   $	��450��6   $	��950��6+   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6J   5      � 5   
   $��1��6    5      �� 5       $
��auto �6    60   �� 5!      $
��auto �6    60   �� 5"      6"   .+�5&      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5'      � $��0 0 0 10q�6   .+�5(      �� $��Center=�6^   $��5 5 15 5q�6y   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Nhà cung cấp��6�    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *	ten_nhacc+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5)      $��Center=�6]   $��5 5 15 5q�6x   $	 Địa chỉ��6�   #��[ 6�   ��6   �� *	dia_chi+$
 PropertyChanged=�6�   �� 5*      $��Center=�6c   $��5 5 15 5q�6~   $	 Số điện thoại��6�   #��[ 6�   ��6   �� *so_dien_thoai+$
 PropertyChanged=�6�   �� 5+      $��Center=�6[   $��5 5 15 5q�6v   $	 Email��6�   #��[ 6�   ��6   �� *email+$
 PropertyChanged=�6�   �� 5,      $��Center=�6[   $��5 5 15 5q�6v   $	 Thông tin thêm��6�   #��[ 6�   ��6   �� *mo_ta+$
 PropertyChanged=�6�   (  "System.Windows.Controls.DatePicker  5-      $	��100��6!   $��Center=�6O   $��5 5 15 5q�6j    � Language$ vi-VN�6|   $	 Ngày hợp tác��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 6�      SelectedDate 6-   �� *
ngay_tao+6�   5.      5/        53      $��1��6"   $��5q�6/   .��54      � .+�55      �� $��Thêm��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 56      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 57      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   58      59        5;      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5<      �� $
��Auto=�6   .��5=      � .+�5>      �� ��6#   �� *List+ �� SelectedItem 6@   �� *SelectedItem+ �� Mode$ TwoWay=� �� View 5?      �� 5@   "   .P�5A   &   �� $��Tên nhà cung cấp��65    �� DisplayMemberBinding 6O   �� *	ten_nhacc+�� 5B   &   $��Địa chỉ��65    6F   �� *	dia_chi+6q   �� 5C   &   $��Số điện thoại��65    6L   �� *so_dien_thoai+6}   �� 5D   &   $��Email��65    6D   �� *email+6m   �� 5E   &   $��Thông tin thêm��65    6M   �� *mo_ta+6v   �� 5F   &   $��Ngày hợp tác��65    6K   �� *
ngay_tao+ �� StringFormat$ 
dd/MM/yyyy��5G   #   5I      5J      5K      5L      5N      5O      5P      