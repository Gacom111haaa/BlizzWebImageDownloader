   M S B A M L   `   `   `  ����  WebImageDownloader"    WebImageDownloader.ChapToVol   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	ChapToVol��5   	   $	��266��6   $	��380��6(    � Icon$ Images/Button-pdf.ico��64   .��5      �  � RowDefinitions 5   
   �� 5      $��8* �6   �� 5      $	��26* �6   �� 5	      $��8* �6   �� 5
      $	��26* �6   �� 5      $��8* �6   �� 5      $	��26* �6   �� 5      $��8* �6   �� 5      $	��26* �6   �� 5      $��8* �6   �� 5      $	��26* �6   �� 5      $��8* �6   �� 5      $	��11* �6   �� 5      $��8* �6   �� 5      $	��33* �6   �� 5      $��2* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5      �� $��Đường dẫn vào��6   $	��100��6,   �� 5      -   5        $��textBoxInput��5   O   $
��Left=�6    �� TextWrapping$
 Wrap=�62   $�� ��6F   $	��200��6c   � 5      $��10��6   �� 5      -   5        $��button_Input��5   .   $	��...��6   $��40��6#   6B   5      �� 5   
   $��3��6   $ 
Horizontal=�6"   .+�5      �� $��Số chap một vol��6   $
��Left=�6.   $	��100��6J   �� 5       -   5        $��textBoxNumberChap��5    E   $
��Left=�6   $�� ��61   $	��250��69   {�.  6^   5!      �� 5#   
   $��5��6   $ 
Horizontal=�6"   .+�5$      �� $��Chap bắt đầu��6   $
��Left=�6+   $	��100��6G   $��6��6S   �� 5%      -   5        $��textBoxChapStart��5%   =   $
��Left=�6   $	��250��61   {�.  6U   5&      �� 5(   
   $��7��6   $ 
Horizontal=�6"   .+�5)      �� $��Tên truyện��6   $
��Left=�6)   $	��100��6E   $��6��6Q   �� 5*      -   5        $��textBoxName��5*   P   $
��Left=�6   $	��250��61   {�.  6P   5+      �� 5-   
   $
��Left=�6   $��9��60   $ 
Horizontal=�6=   .+�5.      �� $��Đường dẫn ra��6   $
��Left=�6+   $	��100��6F   �� 5/      -   5        $��textBoxOutput��5/   2   $
 Wrap=�6   $�� ��6*   $	��200��6G   � 50      $��10��6   �� 51      -   5        $��button_Output��51   .   $	��...��6   $��40��6#   6C   52      � 54   
   -   5        $��progressBar1��54   =    � Maximum$	 100Q�6   $��10��6$   $	��289��61    � Value$ 0Q�6Q   $��11��6[   $��Center=�6i   �� 55   
   -	   5        $��Chạy��55      $��Right=�6    $��Center=�6=   $��75��6X   $��13��6z   57      58      