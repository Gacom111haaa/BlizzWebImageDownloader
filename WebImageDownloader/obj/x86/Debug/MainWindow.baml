   M S B A M L   `   `   `  ����M  IWebImageDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WebImageDownloaderWebImageDownloader  5   	       WebImageDownloader.WID   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2local clr-namespace:WebImageDownloader     5   	     � Title$	  WID��5   	   $	��500��6   $	��800��6"    � Icon$ images/main_icon.ico��6.    � 	Resources 5      ,  &WebImageDownloader.IntToColorConverter   IntToColorConverter��%   &	          5   
   6$   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $	��29* �6   �� 5      $	��87* �6   �� 5      $	��25* �6   �� 5      $
��329* �6   .+�5   
   o�  o� 
IsMainMenu . 
�������
�������$��1q�6G   .@�5      m� $
��Task��6   .@�5      m� -   5        $��Add Link��5      6-   m� 5      -   5        $��Download��5      6-   �� 5      6   m� 5      -   5        $
��Exit��5      6)   5      m� 5      $
��File��6   .@�5      m� $��Pause��6   m� 5      -   5        $��Delete��5      6+   m� 5      -   5        $��
Delete All��5      6/   m� 5      $��Resume��6   5      m� 5      -   5        $��Config��5      6'   m� 5      $��About��6   5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��1��6.   ���	#FFF9F9F9.+�5!      �� -   5        $��	ButtonAdd��5!   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5"      �� .+�5#      �� $��Images/Button-Add.ico��6   $��40��6;   $��40��6F   �� 5$      $��Center=�6    Add6B   5%      5&      �� 5'      -   5        $��
ButtonDown��5'   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5(      �� .+�5)      �� $ ��Images/Button-Download.ico��6   $��40��6@   $��40��6K   �� 5*      $��Center=�6    Start6D   5+      5,      �� 5-      -	   5        $��ButtonPause��5-   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5.      �� .+�5/      �� $��Images/Button-Pause.ico��6   $��40��6=   $��40��6H   �� 50      $��Center=�6    Pause6D   51      52      �� 53      -
   5        $��ButtonResume��53   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��54      �� .+�55      �� $��Images/Button-Resume.ico��6   $��40��6>   $��40��6I   �� 56      $��Center=�6    Resume6E   57      58      �� 59      -   5        $��ButtonDelete��59   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5:      �� .+�5;      �� -   5        $��image_Delete_Button��5;      $��Images/Button-Delete.ico��67   $��40��6Y   $��40��6d   �� 5<      -   5        $��textbox_Delete_Button��5<       $��Center=�6=   Delete6b   5=      5>      �� 5?      -   5        $��ButtonClear��5?   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5@      �� .+�5A      �� -   5        $��image_Clear_Button��5A      $"��Images/Button-DeleteAll2.ico��66   $��40��6\   $��40��6g   �� 5B      -   5        $��textbox_Clear_Button��5B       $��Center=�6<   
Clear List6e   5C      5D      �� 5J      -   5        $��ButtonOptions��5J   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5K      �� .+�5L      �� $��Images/Button-Options.ico��6   $��40��6?   $��40��6J   �� 5M      $��Center=�6    	Options6F   5N      5O      �� 5P      -   5        $��
ButtonHtml��5P   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5Q      �� .+�5R      �� $��Images/Button-html.ico��6   $��40��6<   $��40��6G   �� 5S      $��Center=�6    Html6C   5T      5U      �� 5V      -   5        $��	ButtonPDF��5V   O   ���	#FFF9F9F9$��60��6-   ���	#FFF9F9F9.��5W      �� .+�5X      �� $��Images/Button-PDF.ico��6   $��40��6<   $��40��6G   �� 5Y      $��Center=�6!   
Make pdf6H   5Z      5[      � 5\      .+�5]      �� ���	#FFF9F9F96   r� 5^      -   5         r�Name$ myGif��5^       r� LoadedBehavior$ Manual=�6.    r� Source$' !Images/LoadingCircle_finalani.gif?�6G   $��40��6r   $��40��6}   6�   5_      5a      �� 5b   
   $ 
Horizontal=�6   $��Right=�6.   $��2��5c      ���	#FFF9F9F9.+�5d      �� $��Search��6   6+   �� 5e      -   5        $��	tbxSearch��5e      $	��150��6+   6]   � 5f      $��10��6   6$   5g      &   System.Windows.Controls.DataGrid  5h   
   -   5        	  Name$	 DataGridListDownload��5h      
   AutoGenerateColumns
 .  
�������   GridLinesVisibility$
 None=�5i      $��1q�6.   $��4��6B   ��5h   1   ��    Columns 5j      0  *System.Windows.Controls.DataGridTextColumn  5k      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *ID+ �� Mode$ OneTime=�  5l      $ SavePath��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6T    67   �� *
savepath+6T     5m      $ linkdown��6%   $	 300 67    6C   �� *
linkdown+4  .System.Windows.Controls.DataGridTemplateColumn  5n      $ 
Percentage��6)   $	 120 6=      
IsReadOnly .    CellTemplate 5o      �� 5p      .R�5r      �� $��18��6(   $��20��64   ��6?   ��  �� Path$ 
percentage� �� 	Converter# [  6�   5t      5v        5w      $ Status��6%   $	 120 65    6A   �� *status+$ OneTime=� � ContextMenu 5z      �� 5{      .@�5|      m� -   5        $
��Open��5|      6-   m� 5}      -   5        $��Delete��5}      6/   m� 5~      $��
Redownload��6   5      5�      5�      5�      