   M S B A M L   `   `   `  ����L  HDoAnOOP_QuanLyKho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:DoAnOOP_QuanLyKho.UserControlOOP DoAnOOP_QuanLyKho.UserControlOOP  5
   	   #    DoAnOOP_QuanLyKho.UnitsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   7uc.clr-namespace:DoAnOOP_QuanLyKho.UserControlOOP  5
   	   -   5          �Name$  window��5   	    
  UnitVM � DataContext# [  5   	    � Title$! Phần mềm quản lý kho��5   	   $	��600��6&   $	��800��63     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   
�������  MaterialDesignFont#���  5   	    � 
ResizeMode$ 	CanResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      3   -DoAnOOP_QuanLyKho.UserControlOOP.ControlBarUC  
	 � Tag	 6   �� *Title+
 �� ElementName$
 window��6J   5      � 5   
   $��1��6    5       �� 5!      $
��auto �6    60   �� 5"      $
��auto �6    60   �� 5#      6"   .+�5&      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5q�6/   .��5'      � .+�5(      �� $��5q�6m   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên đơn vị đo��6x    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *DisplayName+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   5)      5*        5,      $��1��6"   $��5 0 5 5q�6/   .��5-      � $��Stretch=�6    � ColumnDefinitions 5.      �� 5/      6-   �� 50      6-   �� 51      6-   .+�53      �� $��0��6   $��Thêm��6-   $��5q�6[   $	��120��6f   $��Center=�6r   ��6<   �� *
AddCommand+6�   �� 54      $��1��6   $��Sửa��6-   $��5q�6[   $	��120��6f   $��Center=�6r   ��6;   �� *EditCommand+6�   �� 55      $��2��6   $
��Xóa��6-   $��5q�6]   $	��120��6h   $��Center=�6t   ��6;   �� *DeleteCommand+6�   56      57        59      $��2��6"   $��5q�6/   $��Stretch=�6:   .��5:      �� $
��Auto=�6   .��5;      �� $��5 0 5 0q�6<   ��6   �� *List+ �� SelectedItem 6M   �� *SelectedItem+$ OneWayToSource=� �� View 5<      �� 5=      .P�5>   "   �� $��Tên đơn vị��61    �� DisplayMemberBinding 6E   �� *TenDV+6n   5?      5A      5B      5C      5D      5E      5F      