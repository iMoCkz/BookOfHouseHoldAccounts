   M S B A M L   `   `   `  ����S  OBookOfHouseholdAccounnts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:BookOfHouseholdAccounntsBookOfHouseholdAccounnts  5   	   7    1BookOfHouseholdAccounnts.FilterTransactionsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   2local&clr-namespace:BookOfHouseholdAccounnts  5   	   7avalon1)http://schemas.xceed.com/wpf/xaml/toolkit  6>     � Title$  FilterTransactionsWindow��5	   	   $	��200��6*   $	��600��67   .��5
      �  � RowDefinitions 5   
   �� 5      $	��20* �6   6+   �� 5      $	��20* �6   6+   �� 5      $	��20* �6   6+   �� 5      $	��20* �6   6+   �� 5      $	��20* �6   6+    � ColumnDefinitions 5   
   �� 5      $	��22* �6   6-   �� 5      $	��16* �6   6-   �� 5      $	��22* �6   6-   �� 5      $	��28* �6   6-   .+�5   
   �� -   5        $��chckbx_bankInstitute��5   0   $��0��6   $��0��6    $��Center=�6L   .��6g   Bank Institute6w   �� 5   
   -   5        $��listbox_filteredBanks��5   @   $��1��6   $��0��6   $��3��6/   $��5,0,5,5q�6�   {�6]   ��  �� ElementName$ chckbx_bankInstitute�� �� Path$ 	IsChecked���6�   �� *BankInstituteOptions+��5      �� 5      .R�5      �� ��6   �� *Name+ d� 	IsChecked 68   �� *
IsFiltered+5      5      �� 5    
   -   5        $��chckbx_valueRange��5    0   $��0��6   $��1��6    $��Center=�6I   .��6d   Value6k   �� 5!   
   $��1��6   $��1��6   $	��Top=�6-   $
��Left=�6E   .��6`   From6f   %  Xceed.Wpf.Toolkit.IntegerUpDown  5"   
   -   5        $��iud_min��5"   :   $��1��6   $��1��6*   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$
 -100X�6�      Minimum$ -10000X�6�      Maximum$ 10000X�6�   $��35,0,5,0q�6�   {�6I   �� $ chckbx_valueRange��$ 	IsChecked�6�   �� 5#   
   $��2��6   $��1��6   $	��Top=�6-   $
��Left=�6E   .��6`   To6d     5$   
   -   5        $��iud_max��5$   :   $��2��6   $��1��6*   $	 100X�6�   $ -10000X�6�   $ 10000X�6�   $��35,0,5,0q�6�   {�6I   �� $ chckbx_valueRange��$ 	IsChecked�6�   �� 5%   
   -   5        $��chckbx_dateRange��5%   0   $��0��6   $��2��6    $��Center=�6H   .��6c   Date6i   �� 5&   
   $��1��6   $��2��6   $	��Top=�6-   $
��Left=�6E   .��6`   From6f   (  "System.Windows.Controls.DatePicker  5'   
   -   5        $��datepicker_start��5'   2   $��1��6   $��2��6"   $��Center=�6�   $��35,0,5,0q�6�   {�6J   �� $ chckbx_dateRange��$ 	IsChecked�6�   �� 5(   
   $��2��6   $��2��6   $	��Top=�6-   $
��Left=�6E   .��6`   To6d     5)   
   -   5        $��datepicker_end��5)   2   $��2��6   $��2��6"   $��Center=�6�   $��35,0,5,0q�6�   {�6H   �� $ chckbx_dateRange��$ 	IsChecked�	   SelectedDate	 6�   �� *CurrentDateTime+6�   �� 5*   
   -	   5        $��chckbx_keyword��5*   0   $��0��6   $��3��6    $��Center=�6F   .��6a   	Keyword6j   �� 5+   
   -
   5        $��listbox_keywords��5+   @   $��1��6   $��3��6   $��3��6/   $��5,0,5,5q�6�   {�6X   �� $ chckbx_keyword��$ 	IsChecked�.@�5,      �� -   5        $��chckbx_partner��5,      .��6-   Transaction Partner6B   �� 5-      -   5        $��chckbx_description��5-      .��61   Description6>   �� 5.      6   �� 5/      -   5        $��txtbx_keyword��5/      $	��160��6+   .��6}   Enter your keyword6�   50      �� 51   
   -   5        $��	btn_apply��51   .   $��4��6   $��3��6   $��50��6?   $��25��6J   $��Right=�6V   $��0,0,5,0q�6r   .��6�   Apply6�   52      53      