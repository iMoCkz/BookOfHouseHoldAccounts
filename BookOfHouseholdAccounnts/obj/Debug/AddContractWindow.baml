   M S B A M L   `   `   `  ����S  OBookOfHouseholdAccounnts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:BookOfHouseholdAccounntsBookOfHouseholdAccounnts  5   	   0    *BookOfHouseholdAccounnts.AddContractWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:BookOfHouseholdAccounnts     5   	     � Title$  Contract��5   	   $	��270��6   $	��230��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��30* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��15* �6   6+   �� 5      $	��18* �6   6+    � ColumnDefinitions 5   
   �� 5      $	��40* �6   6-   �� 5      $	��60* �6   6-   .+�5   
   �� $��0��6   $��0��6   .��6-   Name63   �� 5   
   $��1��6   $��0��6   .��6-   Value64   �� 5   
   $��2��6   $��0��6   .��6-   
Start Date69   �� 5   
   $��3��6   $��0��6   .��6-   
End Date67   �� 5   
   $��4��6   $��0��6   .��6-   Cancel. Period6=   �� 5   
   $��5��6   $��0��6   .��6-   
Category67   �� 5   
   -   5        $��txtBox_name��5   /   $��0��6   $��1��6    �� TextWrapping$
 Wrap=�6B    �� 	MaxLength$ 22��6V   $��50��6e   $	��Top=�6q   $��0,5,10,0q�6�   6�   �� 5   
   -   5        $��txtBox_value��5   /   $��1��6   $��1��6   $	��Top=�6j   $��0,5,10,0q�6�   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��datepicker_startDate��5   2   $��2��6   $��1��6"   $��Center=�6{   $��0,0,10,0q�6�      SelectedDate 6N   �� *PrevCurrentDateTime+6�     5    
   -   5        $��datepicker_endDate��5    2   $��3��6   $��1��6"   $��Center=�6u   $��0,0,10,0q�6�    6L   �� *CurrentDateTime+6�   �� 5!   
   -   5        $��txtBox_canellationPeriod��5!   /   $��4��6   $��1��6   $��2��6�   $	��Top=�6�   $��0,5,60,0q�6�   6�   �� 5"   
   $��4��6   $��1��6   $��Right=�6-   $��0,0,10,0q�6I   .��6[   months6c   �� 5#   
   -   5        $��combobox_category��5#   0   $��5��6   $��1��6    $��22��6r   $��0,0,10,0q�6~   ��6I   �� *BudgetingOptions+6�   �� 5$   
   -   5        $��btn_cancelContract��5$   /   $��10��6   $��1��6   $
��Left=�6j   $��50��6�   $��25��6�   $��15,0,0,0q�6�   .��6�   Cancel6�   �� 5%   
   -   5        $��btn_addContract��5%   /   $��10��6   $��1��6   $��Right=�6d   $��50��6�   $��25��6�   $��0,0,10,0q�6�   .��6�   Add6�   5&      5'      