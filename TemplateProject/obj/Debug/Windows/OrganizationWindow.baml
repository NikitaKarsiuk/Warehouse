   M S B A M L   `   `   `  ����J  FTemplateProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TemplateProject.WindowsTemplateProject.Windows  5   	   G(clr-namespace:TemplateProject.ConvertersTemplateProject.Converters  6=   0    *TemplateProject.Windows.OrganizationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TemplateProject.Windows     5   	   ?
converters(clr-namespace:TemplateProject.Converters     6=   -   5        $	��300��5   	   $	��345��6     � 
ResizeMode$  NoResize=�5
   	    � 	Resources 5      1  +TemplateProject.Converters.AddressConverter   AddressConverter��%   &	          5   
   6&   5      .��5      � .+�5   
   �� $��0,0,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� .��6   Наименование6&   �� 5      -   5         ��Name$ NameTextBox��5        TextBox#��[ 6/   $	��225��6P   $��5,0,0,0q�6\   5      �� 5      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� .��6   Р/С6   �� 5      -   5        $ BankDetailsTextBox��5      #��[ 66   $	��268��6W   $��26,0,0,0q�6c   5      �� 5      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5      �� .��6   УНП6   �� 5      -   5        $ 
UNPTextBox��5      #��[ 6.   $	��268��6O   $��20,0,0,0q�6[   5      �� 5       $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5!      �� .��6   
ОКПО6   �� 5"      -   5        $ OKPOTextBox��5"      #��[ 6/   $	��268��6P   $��10,0,0,0q�6\   5#      �� 5%      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5&      �� .��6   Контактый номер6)   �� 5'      -   5        $ ContactNumberTextBox��5'      #��[ 68   $	��205��6Y   $��10,0,0,0q�6e   5(      �� 5*      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5+      �� .��6   
Адрес6   �� 5,      -   5         ��Name$ AddressCombobox��5,      $	��268��65   $��11,0,0,0q�6A   ��5-      �� 5.      .R�5/      �� ��6(   �� *ID+ �� 	Converter# [   �� Mode$ TwoWay=�50      52      53      �� 55      -   5         ��Name$ 
SaveButton��55      $��Сохранить��6)    
 Button#��[ 6>   $��0,10,0,5q�6^   6p   56      57      58      