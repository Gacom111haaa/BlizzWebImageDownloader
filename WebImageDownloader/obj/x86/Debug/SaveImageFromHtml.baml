   M S B A M L   `   `   `  ����  WebImageDownloader*    $WebImageDownloader.SaveImageFromHtml   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  SaveImageFromHtml��5   	   $	��400��6#   $	��900��60    � Icon$ Images/Button-Html.ico��6<   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��13* �6   �� 5      $	��72* �6   �� 5	      $
��275* �6   �� 5
      $	��12* �6   �� 5      $	��60* �6   �� 5      $	��13* �6    � RowDefinitions 5   
   �� 5      $��8* �6   �� 5      $	��26* �6   �� 5      $	��15* �6   �� 5      $	��26* �6   �� 5      $	��16* �6   �� 5      $	��10* �6   �� 5      $	��14* �6   �� 5      $
��134* �6   .+�5   
   �� -   5        $��label1��5   ?   $��Save To��6   $��3��6"   $��1��6/   $��Center=�6M   $��Right=�6o   �� 5   
   -   5        $��textBoxSaveTo��5      $��3��6   $��23��64   $��C:\Users\Admin\Desktop��6@   $��2��6^   �� 5   
   -   5        $��button1��5   >   $��Start��6   $��4��6!   $��3��61   $��Center=�6M   $��Center=�6�   $��Center=�6�   $��80��6�   $��23��6�   � 5   
   -   5        $��
pgbRunning��5   #   $��5��6   $��4��65   $��1��6I   �� 5   
   -   5        $��label4��5   ?   $��Address��6   $��1��6"   $��1��6/   $��Center=�6M   $��Right=�6o   �� 5   
   -   5        $��textBoxOpen��5   /   $��2��6   $��1��6"   $��23��6B   �� 5   
   -   5        $��buttonBrowser��5   @   $��Browser��6   $��4��6#   $��1��63   $��Center=�6q   $��Center=�6�   $��80��6�   $��23��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ grvHtmlFile��5   D   $��1��6   $��7��6#   $��4��60      AutoGenerateColumns .  $	��180��5    /   $	��Top=�6<   ��5   Y   ��    	CellStyle 5!      �� 5"       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5#      ��  �� Property � ��	 �� Value
	 �����6D   �� 5$       � ��$	 0q�68   6D   5%      
   Columns
 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$
 link��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 500 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *link+6Z     5)      $ status��6%   $	 100 6P    65   �� *status+6^   4  .System.Windows.Controls.DataGridTemplateColumn  5*      $ Action��6)      CellTemplate 5+      �� 5,      .R�5-      ��  �� Orientation$ 
Horizontal=�6)   .+�5.   "   �� -	   5        $	��Run��5.   )   $��50��6L   6Y   �� 5/   "   $��btnMove��68   -
   5        $
��Move��5/   )   $��50��6G   6j   �� 50   "   $��	btnDelete��6:   -   5        $��Delete��50   )   $��50��6c   6p   51      52      54      56      57      58      