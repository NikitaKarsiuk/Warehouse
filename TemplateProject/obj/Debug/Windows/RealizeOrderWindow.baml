   M S B A M L   `   `   `  ����J  FTemplateProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TemplateProject.ConvertersTemplateProject.Converters  5   	   A%clr-namespace:TemplateProject.WindowsTemplateProject.Windows  5   	   0    *TemplateProject.Windows.RealizeOrderWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?
converters(clr-namespace:TemplateProject.Converters     5   	   7local%clr-namespace:TemplateProject.Windows     5   	   -   5        $	��380��5	   	   $	��700��6     � 
ResizeMode$  NoResize=�5   	    � 	Resources 5      .  (TemplateProject.Converters.CostConverter   CostConverter.  (TemplateProject.Converters.UnitConverter  UnitConverter4  .TemplateProject.Converters.PackedTypeConverter  PackedTypeConverter��%W   &	        &	      &	 &       5   
   6#     5   
   6#     5   
   6)   5      .��5      � .+�5   
   �� $��0,0,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,20,0,0q�63   .+�5      �� -   5         ��Name$ 
SaveButton��5      $��Сохранить��6-    
 Button#��[ 6B   $��10,0,0,0q�6b   6t   �� 5      -   5        $ DeleteButton��5      $��Удалить��6/   #��[ 6B   $��10,0,0,0q�6b   6t   �� 5      -   5        $ PrintButton��5      $��Чек��6.   $y�Hidden=�6<   #��[ 6P   $��
10, 0, 0,0q�6p   �� 5      -   5        $��PrintPriceTagButton��5      $��Ценник��64   $y�Hidden=�6E   #��[ 6Y   $
��Left=�6y   6�   5      �� 5      $ 
Horizontal=�6   $��0,20,0,0q�63   $	��262��6E   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ProductDataGrid��5         AutoGenerateColumns .  $	��662��6�   $��	10,0,15,0q�6�      Columns 5      4  .System.Windows.Controls.DataGridComboBoxColumn  5      -   5        
nameColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Название��5   E      DisplayMemberPath$
 Name��6W   	   SelectedItemBinding	 6p   �� *	Product+0  *System.Windows.Controls.DataGridTextColumn  5       $ Тип продукта��6-   
   
IsReadOnly
 . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
TypeName+6`     5!      $ Ед. измерения��6-   
 .  6D   �� *
UnitName+6a     5"      $2 ,Тип упакованного товара��6-   
 .  6N   �� *
PackedName+6m     5#      $ Количество��6-    6A   �� *
OrderCount+  5$      $ Цена, руб.��6-   
 .  6A   �� *	Product+ �� 	Converter# [  6�     5%      $ Сумма, руб.��6-   
 .  6B   �� *Sum+6Z   5'      5(      5)      5*      5+      