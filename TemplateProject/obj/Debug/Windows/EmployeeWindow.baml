   M S B A M L   `   `   `  ����J  FTemplateProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TemplateProject.WindowsTemplateProject.Windows  5   	   ,    &TemplateProject.Windows.EmployeeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TemplateProject.Windows     5   	   -   5        $	��190��5   	   $	��300��6     � 
ResizeMode$  NoResize=�5
   	   .��5      � .+�5   
   �� $��0,0,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0, 10, 0, 0q�62   .+�5      �� .��6   ФИО6   �� 5      -   5         ��Name$ 
FioTextBox��5         TextBox#��[  6.   $	��190��6O   $��45,0,0,0q�6[   5      �� 5      $ 
Horizontal=�6   $��0, 10, 0, 0q�62   .+�5      �� .��6   Должность6#   �� 5      -   5         ��Name$ PositionComboBox��5       �� DisplayMemberPath$
 Name��65   $	��190��6N   $��11,0,0,0q�6Z   5      �� 5      $ 
Horizontal=�6   $��0, 10, 0, 0q�62   .+�5      �� .��6   Организация6%   �� 5      -   5        $ OrganizationComboBox��5      $
 Name��69   $	��190��6R   $��0,0,0,0q�6^   5      �� 5      -   5         ��Name$ 
SaveButton��5      $��Сохранить��6)    
 Button#��[ 6>   $��	0,10,0, 0q�6^   6q   5      5      5      