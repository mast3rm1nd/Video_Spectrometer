   M S B A M L   `   `   `  ����M  IVideo Spectrometer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:VideoFileFramesPictureVideoFileFramesPicture  5   	   '    !VideoFileFramesPicture.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5   	   4local$clr-namespace:VideoFileFramesPicture    5   	   X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Video Spectrometer��5	   	   $	��220��6$   $	��518��61    � 
ResizeMode$ CanMinimize=�6=    � WindowStartupLocation$ CenterScreen=�6V   .��5
      � .+�5   
   �� -   5         ��Name$ label��5      $��Видеофайл:��6   $
��Left=�64   $��	10,10,0,0q�6O   $	��Top=�6b   �� 5   
   -   5         ��Name$ ChooseFile_Button��5      $	��...��6,   $
��Left=�6:   $��88.96,14,0,0q�6U   $	��Top=�6k   $��91.817��6�   6�   �� 5   
   -   5        $ FileName_Label��5      $��не выбран��6(   $
��Left=�6<   $��185.777,10,0,0q�6W   $	��Top=�6o   �� 5   
   -   5        $ Process_Button��5      $'��!Сохранить "спектр"��6)   $
��Left=�6P   $��10,87.96,0,0q�6k   $	��Top=�6�   $	��476��6�   $��29.04��6�   6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  5   
   -   5          Name$" PixelsPerFrame_IntegerUpDown��5      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 50X�6C   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	Increment$ 1X�6N      Maximum$ 200000X�6\   $��125.777,49.22,331.223,0q�6m   	   Minimum$	 1X�6�   $��24.22��6�   $	��Top=�6�   �� 5   
   -   5        $ 
label_Copy��5      $%��Пикселей на кадр:��6$   $
��Left=�6@   $��10,47.48,0,0q�6[   $	��Top=�6q   � 5   
   -   5        
 �Name$
 Progress_ProgressBar��5      $
��Left=�64   $��19.04��6O   $��10,152.96,0,0q�6^   $	��Top=�6u   $	��476��6�   �� 5   
   -   5        $ Progress_Label��5      $�� ��6(   $
��Left=�63   $��
10,122,0,0q�6N   $	��Top=�6b   $	��476��6z   $��Center=�6�   $��Center=�6�   5      5      