   M S B A M L   `   `   `  ����H  DProjectClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ProjectClientProjectClient  5   	   %    ProjectClient.View.DownloadFile   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ProjectClient     5   	     � Title$  Zapisz plik��5   	   $	��400��6   $	��400��6*   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��Wybierz plik:��6   $��0��6(   $��0��65   �� 5   
   -   5        $��AvailableFileCombobox��5      $��0��60   $��1��6=      SaveComboBoxCenterLeft#��[  6M   �� 5   
   $��
Zapisz do:��6   $��1��6%   $��0��62   �� 5   
   $��1��6   $��1��6"   $��Center=�62    �� Orientation$ 
Horizontal=�6M   .+�5      �� -   5         ��Name$ 
PathTxtbox��5      $	��130��6*   {�.  69   �� 5      -   5         ��Name$ SelectPathBut��5      $	��...��6,   $��20��6:   6E   5      �� 5   
   $��Format zapisu:��6   $��2��6)   $��0��66   �� 5   
   -   5        $��TypeFileCombobox��5      $��2��6+   $��1��68   #��[  6H   �� 5   
   -   5        $ 
ConfirmBut��5      $��Pobierz��6%     ButtonMediumSize#��[ 67   $��3��6a   $��0��6n   $��2��6~   6�   5      5      