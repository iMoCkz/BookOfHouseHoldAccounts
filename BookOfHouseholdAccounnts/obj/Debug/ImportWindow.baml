   M S B A M L   `   `   `  ����S  OBookOfHouseholdAccounnts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:BookOfHouseholdAccounntsBookOfHouseholdAccounnts  5   	   +    %BookOfHouseholdAccounnts.ImportWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:BookOfHouseholdAccounnts     5   	     � Title$  ImportWindow��5   	   $��95��6   $	��450��6*   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��10* �6   6-   �� 5      $	��75* �6   6-   �� 5      $	��15* �6   6-    � RowDefinitions 5   
   �� 5      $	��50* �6   6+   �� 5      $	��50* �6   6+   .+�5   
   �� $��0��6   $��0��6   .��6-   File:64   �� 5   
   -   5        $��txtbx_filePath��5   /   $��0��6   $��1��6   $��Center=�6E   $��10(�6`    �� 
IsReadOnly . $��0,0,5,0q�6�   6�   �� 5   
   -   5        $��btn_selectPath��5   .   $��0��6   $��2��6   $��Center=�6D   $
��Left=�6_   $��30��6z   $��17��6�   .��6�   ...6�   �� 5   
   $��1��6   $��0��6   $	��Top=�6-   .��6E   Bank:6L   �� 5   
   -   5        $"��combobox_bankInstituteImport��5   0   $��1��6   $��1��6     �� DisplayMemberPath$
 Name��6�    �� SelectedIndex$ 0��6�   $	��Top=�6�   $
��Left=�6�   $	��120��6�   $��22��6�   $��0,0,5,0q�6�   ��6T   �� *BankInstituteOptions+6  �� 5   
   -   5        $��
btn_import��5   .   $��1��6   $��2��6   $��0,0,5,5q�6Y   .��6j   Import6r   5      5      