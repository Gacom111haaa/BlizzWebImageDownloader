   M S B A M L   `   `   `  ����   WebImageDownloader    WebImageDownloader.WID   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$	  WID��$��428.063��$��761.265�� � Icon$ images/main_icon.ico��.���  � RowDefinitions �� $	��23* ��� $	��70* ��� $
��285* �.+�o�  o� 
IsMainMenu . 
�������
�������$��1q�.@�m� $
��Task��.@�m� -   $��Add Link��m� -   $��Download���� m� -   $
��Exit��m� $
��File��.@�m� $��Pause��m� -   $��Delete��m� -   $��
Delete All��m� $��Resume��m� -   $��Config��m� $��About����  �� Orientation$ 
Horizontal=�$��1�����	#FFF9F9F9.+��� -   $��	ButtonAdd�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-Add.ico��$��40��$��40���� $��Center=�Add�� -   $��
ButtonDown�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $ ��Images/Button-Download.ico��$��40��$��40���� $��Center=�Start�� -	   $��ButtonPause�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-Pause.ico��$��40��$��40���� $��Center=�Pause�� -
   $��ButtonResume�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-Resume.ico��$��40��$��40���� $��Center=�Resume�� -   $��ButtonDelete�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� -   $��image_Delete_Button��$��Images/Button-Delete.ico��$��40��$��40���� -   $��textbox_Delete_Button��$��Center=�Delete�� -   $��ButtonClear�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� -   $��image_Clear_Button��$"��Images/Button-DeleteAll2.ico��$��40��$��40���� -   $��textbox_Clear_Button��$��Center=�
Clear List�� -   $��ButtonOptions�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-Options.ico��$��40��$��40���� $��Center=�	Options�� -   $��
ButtonHtml�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-html.ico��$��40��$��40���� $��Center=�Html�� -   $��	ButtonPDF�����	#FFF9F9F9$��60�����	#FFF9F9F9.���� .+��� $��Images/Button-PDF.ico��$��40��$��40���� $��Center=�
Make pdf� .+��� ���	#FFF9F9F9r� -    r�Name$ myGif�� r� LoadedBehavior$ Manual=� r� Source$' !Images/LoadingCircle_finalani.gif?�$��40��$��40��&   System.Windows.Controls.DataGrid  -     Name$ DataGridListDownload��$��2��	   AutoGenerateColumns	 .  
�������$��1q����� 
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+ �� Mode$ OneTime=�  $ SavePath��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 200  �� *
savepath+  $ linkdown��$	 300  �� *
linkdown+4  .System.Windows.Controls.DataGridTemplateColumn  $ 
Percentage��$	 120    
IsReadOnly .    CellTemplate �� .R�� $��21��$��Center=� � Value �� *
percentage+  $ Status��$	 120  �� *status+$ OneTime=� � ContextMenu �� .@�m� $	��Add��m� -   $��Delete��m� $��
Redownload��