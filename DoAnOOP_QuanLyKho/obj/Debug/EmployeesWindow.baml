   M S B A M L   `   `   `  ����L  HDoAnOOP_QuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:DoAnOOP_QuanLyKho.UserControlOOP DoAnOOP_QuanLyKho.UserControlOOP  5   	   '    !DoAnOOP_QuanLyKho.EmployeesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   7uc.clr-namespace:DoAnOOP_QuanLyKho.UserControlOOP  5   	   -   5          �Name$  window��5   	      
EmployeeVM � DataContext# [  5   	    � Title$! Phần mềm quản lý kho��5   	   $	��600��5	   	   $	��800��5
   	     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   
�������  MaterialDesignFont#���  5   	    � 
ResizeMode$ 	CanResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      3   -DoAnOOP_QuanLyKho.UserControlOOP.ControlBarUC  
	 � Tag	 6   �� *Title+
 �� ElementName$
 window��6J   5       � 5!   
   $��1��6    5"      �� 5#      $
��auto �6    60   �� 5$      $
��auto �6    60   �� 5%      6"   .+�5(      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5)      � .+�5*      �� $��	10 5 10 5q�6m   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên nhân viên��6�    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *DisplayName+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5+      -  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��6    �� DisplayMemberPath$ TenGT��5,      $ Giới tính��5-      $��0 5 10 5q�5.       �� 
IsEditable . $��35��51      $��Bottom=�52       �� SelectedItem 5/      �� *SelectedGender+$ TwoWay=�$ PropertyChanged=���53      �� *GendersList+54      �� 55      $��5 5 10 5q�6e   $	��150��6w   $ Số điện thoại��6�   #��[ 6�   ��6   �� *SDT+$ PropertyChanged=�$ TwoWay=�6�   �� 56      $��5 5 10 5q�6f   $
 CMND��6x   #��[ 6�   ��6   �� *CMND+$ PropertyChanged=�$ TwoWay=�6�   (  "System.Windows.Controls.DatePicker  57      $	��100��6!   $ 
Ngày sinh��58   !      SelectedDateFormat$ Short=�5:   !   $��5 5 10 4q�5;   !   $��35��5<   !    ( $MaterialDesignFloatingHintDatePicker#��[ 5=   !   $��Bottom=�5>   !      SelectedDate 59   !   �� *
NgaySinh+$ TwoWay=�$ PropertyChanged=� �� StringFormat$ 
dd/MM/yyyy��5>   !   �� 5@      $ 2��6   $ TenCV��5A      $ Chức vụ��5B      $��0 5 10 5q�5C      $��35��5E       . $��Bottom=�5G       5D      �� *SelectedPosition+$ TwoWay=�$ PropertyChanged=���5H      �� *PositionsList+5I      5J      5K        5M      $��1��6"   $��5 0 5 5q�6/   .��5N      � $��Stretch=�6    � ColumnDefinitions 5O      �� 5P      6-   �� 5Q      6-   �� 5R      6-   .+�5T      �� $��0��6   $��Thêm��6-   $��5q�6[   $	��120��6f   $��Center=�6r   ��6<   �� *
AddCommand+6�   �� 5U      $��1��6   $��Sửa��6-   $��5q�6[   $	��120��6f   $��Center=�6r   ��6;   �� *EditCommand+6�   �� 5V      $��2��6   $
��Xóa��6-   $��5q�6]   $	��120��6h   $��Center=�6t   ��6;   �� *DeleteCommand+6�   5W      5X        5Z      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5[      �� $
��Auto=�6   .��5\      �� ��6   �� *List+ 6<   �� *SelectedItem+$ OneWayToSource=� �� View 5]      �� 5^      .P�5_   "   �� $��Tên nhân viên��61    �� DisplayMemberBinding 6H   �� *TenNV+6q   �� 5`   "   $��Giới tính��61    6D   �� *GioiTinh1.TenGT+6w   �� 5a   "   $��
Ngày sinh��61    6D   �� *
NgaySinh+$ 
dd/MM/yyyy��6�   �� 5b   "   $
��CMND��61    6?   �� *CMND+6g   �� 5c   "   $��Chức vụ��61    6B   �� *ChucVu.TenCV+6r   �� 5d   "   $��Số điện thoại��61    6H   �� *SDT+6o   5e      5g      5h      5i      5j      5k      5l      