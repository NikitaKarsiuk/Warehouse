   M S B A M L   `   `   `  ����J  FTemplateProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TemplateProject.WindowsTemplateProject.Windows  5   	   ,    &TemplateProject.Windows.MismatchWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TemplateProject.Windows     5   	   -   5        $	��430��5   	   $	��600��6     � 
ResizeMode$  NoResize=�5
   	   .��5      � .+�5   
   �� $��0,0,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,20,0,0q�63   .+�5      �� -   5         ��Name$ 
SaveButton��5      $��Сохранить��6-    
  Button#��[  6B   $��10,0,0,0q�6b   6t   �� 5      -   5        $ PrintButton��5      $��Печать��6.   #��[  6?   $��
10, 0, 0,0q�6_   6s   5      �� 5      $ 
Horizontal=�6   $��0,20,0,0q�63   .+�5      �� $��10,0,0,0q�6   .��6*   ТТН6/   �� 5      -   5         ��Name$ TtnComboBox��5       �� DisplayMemberPath$ ID��60   $	��210��6G   $��41,0,0,0q�6S   6e   5      �� 5      $ 
Horizontal=�6   $��0,20,0,0q�63   $	��262��6E   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ProductDataGrid��5         AutoGenerateColumns .  $	��562��6�   $��	10,0,15,0q�6�      Columns 5      4  .System.Windows.Controls.DataGridComboBoxColumn  5      -   5        
nameColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Название��5   E   	   DisplayMemberPath$
	 Name��6W   
   SelectedItemBinding
 6p   �� *	Product+0  *System.Windows.Controls.DataGridTextColumn  5      $ Тип продукта��6-      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
TypeName+6`     5      $ Ед. измерения��6-    .  6D   �� *
UnitName+6a     5      $ Количество��6-    6A   �� *
OrderCount+  5      $ Цена, руб.��6-    .  6A   �� *Product.Cost+6b     5      $ Сумма, руб.��6-    .  6B   �� *Sum+6Z   5!      5"      5#      5$      5%      