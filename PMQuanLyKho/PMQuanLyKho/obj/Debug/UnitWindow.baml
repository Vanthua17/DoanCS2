   M S B A M L   `   `   `  ����F  BPMQuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PMQuanLyKhoPMQuanLyKho  5   	   C&clr-namespace:PMQuanLyKho.UserControlTPMQuanLyKho.UserControlT  5   	       PMQuanLyKho.UnitWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:PMQuanLyKho     5   	   5uc&clr-namespace:PMQuanLyKho.UserControlT     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6;   -   5          �Name$  window��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$
 None=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    
  UnitVM � DataContext# [  5   	    � Title$ Đơn vị đo��5   	   $	��500��6   $	��500��6(   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      +   %PMQuanLyKho.UserControlT.ControlBarUC  
 � Tag 6   �� *Title+ �� ElementName$ window��6J   5      � 5   
   $��1��6    5      �� 5       $
��auto �6    60   �� 5!      $
��auto �6    60   �� 5"      6"   .+�5&      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5'      � .+�5(      � .+�5)      �� $��Center=�5*      $��5q�60   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Tên đơn vị đo��5+       % !MaterialDesignFloatingHintTextBox#��[ 5,      ��5)      �� *
ten_don_vi+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Mode$ TwoWay=�5,      (  "System.Windows.Controls.DatePicker  5.      $	��100��6!   $��Center=�6O   $��5 5 15 5q�6j    � Language$ vi-VN�6|   $	 Ngày tạo��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .  ( $MaterialDesignFloatingHintDatePicker#��[ 6�      SelectedDate 6-   �� *
ngay_tao+6�   �� 5/      $��Center=�50      $��5q�60   $	 Mô tả��51      #��[ 52      ��5/      �� *mo_ta+$
 PropertyChanged=�$ TwoWay=�52      53      54      55        59      $��1��6"   $��5q�6/   .��5:      � .+�5;      �� $��Thêm��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 5<      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 5=      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   5>      5?        5B      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5C      �� $
��Auto=�6   .��5D      � .+�5E      �� ��6#   �� *List+$ TwoWay=�$
 PropertyChanged=� �� SelectedItem 6r   �� *SelectedItem+$ TwoWay=� �� View 5F      �� 5G   "   .P�5I   &   �� $��Tên đơn vị đo��65    �� DisplayMemberBinding 6L   �� *
ten_don_vi+6z   �� 5J   &   $��Mô tả��65    6D   �� *mo_ta+6m   �� 5K   &   $��Ngày tạo��65    6G   �� *
ngay_tao+ �� StringFormat$ 
dd/MM/yyyy��5M   #   5O      5P      5Q      5R      5T      5U      5V      