   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   NormalStateBtnBackground0  NormalStateBtnBackground1  NormalStateBtnBackground2  PressedStateBtnBorder  PressedStateBtnBackground0  PressedStateBtnBackground1  PressedStateBtnBackground2  MouseOverStateBtnBorder   MouseOverStateBtnBackground0  	 MouseOverStateBtnBackground1  
 MouseOverStateBtnBackground2  commonButtonTemplate �� 
TargetType ��Name �� CornerRadius �� EndPoint �� 
StartPoint � Color � Offset	 �� TextAlignment'   !System.Windows.VisualStateManager
    VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To   From   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty	 �� To g� Duration	 W� To �� 
TargetType �� Property �� Value#   WpfApplication1.MessageBoxWin � ColumnDefinitions � RowDefinitions �Name �� Stretch J� RenderTransformOrigin �� ScaleX �� Orientation  ��Name! �� RelativeSource" �� Path# �� Mode$ �� UpdateSourceTrigger% ��Name& � Cursor' �� Triggers( P� Property) P� Value* �� 
TargetName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      'localclr-namespace:WpfApplication1  5        5      �� 5   
   $) #../Common/CustomControlsStyles.xaml?�6   %�.  &	        &	      &	 >     &	 [     &	 |     &	 �     &	 �     &	 �     &	 �     &		     &	
 6    &	 S    7   7  7  7  7  7 	 7 
 7  7  7  7  '�� �	    7  '  2
    7  7  7  7  ��5      	White��66   ��5	      	#FFEAF1FF��6:   ��5
      	White��66   ��5      	#FF173D81��66   ��5      	White��67   ��5      	#FFD8E2F5��6;   ��5      	#FFE2EAF9��6;   ��5      	#FF3B70CE��68   ��5      	White��69   ��5      	White��69   ��5      	White��69   �� 5      " ��63   .R�5   
   � .+�5      �� $ 
MainBorder��6   ���	#FF5E7FBA$��1q�6A   $ 4��6U   ��5      �� 5      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5       � 8   $ 0Q�6Z   � 5!      8  $ 0.854Q�6Z   � 5"      8  $ 1Q�6Z   5#      5%      �� 5'      #��z��6   $��14(�69   $��Microsoft Sans Serif)�6G   $	 Center=�6i   ���	#FF182B4B$��Center=�6�   $��Center=�6�   $	��5,0q�6�   
 5)        5*      CommonStates�� 5+        5,      $ 0:0:0.0D�6+     5-      $ 0:0:0.0D�6+   $ Pressed��6G     5.      $ 0:0:0.0D�6+   $ Pressed��6G   . 50        
Normal��6"     51      	MouseOver��. 52      �� .��53      �� $ 
MainBorder��6-   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6P   8  $ 0:0:0.1D�6�   �� 54      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6P   8  $ 0:0:0.1D�6�   �� 55      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[1].(GradientStop.Color)�6P   8  $ 0:0:0.1D�6�   �� 56      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[2].(GradientStop.Color)�6P   8  $ 0:0:0.1D�6�   58      59        5:      Pressed��. 5;      �� .��5<      �� $ 
MainBorder��6-   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6P   8  $ 0:0:0.1D�6�   �� 5=      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[0].(GradientStop.Color)�6P   8  $ 0:0:0.1D�6�   W� 5>      $ 
MainBorder��6.   $V P(Border.Background).(LinearGradientBrush.GradientStops)[1].(GradientStop.Offset)�6Q   $ 0.142X�6�   $ 0:0:0.1D�6�   �� 5?      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[1].(GradientStop.Color)�6P   8 	 $ 0:0:0.1D�6�   �� 5@      $ 
MainBorder��6-   $U O(Border.Background).(LinearGradientBrush.GradientStops)[2].(GradientStop.Color)�6P   8 
 $ 0:0:0.1D�6�   5B      5C      5J      5L      5M      �� 5O      " ��6   .�5P   
   ��  �@J�SnapsToDevicePixels . 60   �� 5Q   
    � ��8   6%   5R      �� 5T      "  6   .�5U   
   ��  �@J�SnapsToDevicePixels . 60   �� 5V   
    � �� 5W      �� 5X      "  6"   .R�5Y      � 
������� 5Z      �� 5[      $��60 �6/   �� 5\      $��* �6/   $	��350��69   $	��450��6H    5_      �� 5`      $��* �6,   $��70��67   �� 5a      $��50 �6,   .+�5d      �� $��0��6!   $��0��6.   $��2��6>   ���	#FFC6D7FA$��0,0,0,1q�6k   � 5f      $ IndormationIcon��6   $��40��68   $��40��6C   $	��Top=�6O   $��Center=�6g   $��10q�6�   $y�Hidden=�6�   .+�5g      F� ����𘎌�Qј�=�Qј�=Z�ј�=���ј�=��ј���ј��knј���knј����ј���ј����ј������T�B'�Sw��I+C`@@~��_�@~�Y
�@~���dw�n�qT�n�qӈQZn�q@RM��d@RMY
�@RM�g��QZ�I+C`@T�B'�� ��c�ӈ�	�c�pd�h�	pd� ��pd���w��	
`�% �
`�%�`�w
`�%
`�%��w
`�% ��
`�%h�	`�w�c� ��c�� �    ���X     �'(�� �' �� �'rMY��X �' � �'��� �'rMY ��    (���� �	���	#FF4C7AC0$
 Fill=�6Z  $��Stretch=�6i  $��Stretch=�6�  5h      � 5j      $ WarningIcon��6   $��40��64   $��40��6?   $	��Top=�6K   $��Center=�6c   $��10q�6�   $y�Hidden=�6�   .+�5k      F� $
 Fill=�6#   ���	#FFFFAF00�����HUD-�M�"Ѱ���M�"���w�M�"������ ���б}����8�����������r~��� �^��x4�	Ю�Ȃ8�_���8�_��0_8�_�j�t�p@Ю�- �^��H"�0��d�b\1�A���u1@2���u1б}���u1���  E�/�M�"HUD-�M�"$��0,0,-0.509,-0.064q�6�  $ 0.5,0.5&�6�  $
��Left=�6  $��33.064��6  $��Bottom=�6,  $��40.509��6G  z�5l   "   V� 5m   &   .~�5n   *   �� $ -1Q�69   �� 5o   *   �� 5p   *   T� 5q   *   5r   '   5t      F� 5u      $
 Fill=�6#   ���	#FF2B303B�������������r�
�r��'
�� �	�[���-	诏��-	��t���-	诃 �	�T �<
�����F�
�w �����Ƙ�����}���FN�w������� ���T+��p@诃+��p@��t�+��p@诏 ���[�������r9�r i����� ��������ƞ���� ��
��.���
�[����Fl	�ۥ���	�����	[�����	䅽���-	[I� �B	�0R�� �	�/
���'
�[r�rQ
�/��Ƥ
��'� �
������6�FZ�6� �*�6�r��[�����'� c�[l ���[r�r��%
��F����S �X�5�;�@�+��p@�
�+��p@���+��p@[[�d;�O�sp@��"����Qp@�����E`���F��C�������� ������$��18.381,0,0,5.359q�6  $ 0.5,0.5&�6*  $
��Left=�6J  $��18.604��6e  $��Bottom=�6u  $��3.747��6�  z�5v   "   V� 5w   &   .~�5x   *   �� $ -1Q�69   �� 5y   *   �� 5z   *   T� 5{   *   5|   '   5~      5      � 5�      $ 	ErrorIcon��6   $��40��62   $��40��6=   $	��Top=�6I   $��Center=�6a   $��10q�6~   $y�Hidden=�6�   .+�5�      F� $
 Fill=�6#   ���	#FFE22B02���������Ӱ7����
�
4̦�
�
䅇��
�
�?h�7��k���k�H�d�k ��?h �䅇� �4̦H�d��������:�"�x�JHx�Jl�.(�:8�>���2ܛF؃(�J��J���J�C	ܛF�P$�>��σ��ѰV�t>ӈc�ddFx���]Jh���]J�p'��]J`��ddF8��Ho>���$�.�� � ����� MJЀ��8��@�	�������A� ���_�ӈ���|�Ht��|��Z�d�рۃ4�M�0��_�Әq�|�@7+�|��;�d���JdI�(�J��ڊ��P@�;T�	�b������:�"$��Stretch=�6�  $��Stretch=�6�  5�      � 5�      $ QuestionIcon��6   $��42��65   $��36��6@   $	��Top=�6L   $��Center=�6d   $��	9,12,9,12q�6�   $y�Hidden=�6�   .+�5�      F� $
 Fill=�6#   ���	#FF5686C9�����^��"� �\=� Z��� Z����� Z��[�(L���L��h�L���j�[���j������j�h�|#~��|#~��|#~�����o �,�V�pDAn�5o�}m0�Txj�8����i@D�P�`x5��\�@�r5L0%fE6��^��"�;�����;����0�05�0�Ѹ�p0���i0��b��b�����b !���b4���i����p���05��������;�4��;� !��;������o���w�P���P���S�0�,ϖ�8h�k��ʧ���������P#��T� 1���;�����;� 8������h���Ѱ�l���0Cg���b�J�@�c����ptd���0CgP)���k���� �q2��|�����ܭ{����v����tp;��	h�(���`輨2W輨�M�輨�C(�4#;�T�@�4���L	.05�T;+H��T;+���T;+X�yL	.�Vt��2Ӹ�p|�5@�n�2:��l�>�P�jlE�^e�*I�^�*I��6�:H���1�:H�?-�C�1.�>�`04])X�7�ux�FH<��XW(#��m���H������pܦ���pR��'�ӈ��� �k��3�k�4�LӘy�L�\H����k�o���w$��Stretch=�6�  $��Stretch=�6  5�      �� 5�      $ Vertical=�6%   $��Stretch=�6<   $��Stretch=�6X   $��
0,10,10,10q�6v   $��0��6�   $��1��6�   .+�5�      �� $  MessageCaptionBlock��6:   $��0,0,0,10q�6(   $
��Wrap=�5�   (   $��Microsoft Sans Serif)�6<   $��14(�6^   
���   �$��Stretch=�5�   (   $��Stretch=�6D   $
��Bold"�6b   ��5�   W   �� ! �� *TemplatedParent+$" MessageText�$# TwoWay=�$$ PropertyChanged=�5�   b   �� 5�      $  MessageTextBlock��6(   $
��Wrap=�5�   (   $��Microsoft Sans Serif)�6<   $��14(�6^   
���   �$��Stretch=�5�   (   $��Stretch=�6D   ��5�   B   �� ! �� *TemplatedParent+$" MessageDescription�$# TwoWay=�$$ PropertyChanged=�5�   D   5�      �� 5�      $ 
Horizontal=�6%   $��1��6>   $��0��6K   $��2��6[   $��Center=�6o   $��Center=�6�   $��0,0,0,0q�6�   .+�5�      �� $	% yes��6%   $	��Yes��65   $��28��6C   $��85��6O   $��Right=�6]   $
& Hand��6y   $��0,0,0,0q�6�   8��  $$��local:MessageBoxWin.YesCommand��6�   �� 5�      $% no��6%   $��No��65   $��28��6C   $��85��6O   $��Right=�6]   $
& Hand��6y   $��5,0,0,0q�6�   8�� $#��local:MessageBoxWin.NoCommand��6�   �� 5�      $% ok��6%   $��OK��65   $��28��6C   $��85��6O   $��Right=�6]   $
& Hand��6y   $��5,0,0,0q�6�   8�� $#��local:MessageBoxWin.OKCommand��6�   �� 5�      $% cancel��6%   $��Cancel��65   $��28��6F   $��85��6R   $��Right=�6`   $
& Hand��6|   $��5,0,0,0q�6�   8�� $'��!local:MessageBoxWin.CancelCommand��6�   5�      5�      ' 5�      P� 5�      ( �@ MessageBoxButtons$) OK=�6?   . �5�      �� $* ok��6%    � y�$ Visible=�6K   �� 5�      $	* yes��6%    � y�$ 	Collapsed=�6L   �� 5�      $* no��6%    � y�$ 	Collapsed=�6K   �� 5�      $* cancel��6%    � y�$ 	Collapsed=�6O   5�      P� 5�      ( �@ MessageBoxButtons$) YesNoCancel=�6?   . �5�      �� $* ok��6%    � y�$ 	Collapsed=�6K   �� 5�      $	* yes��6%    � y�$ Visible=�6L   �� 5�      $* no��6%    � y�$ Visible=�6K   �� 5�      $* cancel��6%    � y�$ Visible=�6O   5�      P� 5�      ( �@ MessageBoxButtons$) YesNo=�6?   . �5�      �� $* ok��6%    � y�$ 	Collapsed=�6K   �� 5�      $	* yes��6%    � y�$ Visible=�6L   �� 5�      $* no��6%    � y�$ Visible=�6K   �� 5�      $* cancel��6%    � y�$ 	Collapsed=�6O   5�      P� 5�      ( �@ MessageBoxButtons$) 	OK_Cancel=�6?   . �5�      �� $* ok��6%    � y�$ Visible=�6K   �� 5�      $	* yes��6%    � y�$ 	Collapsed=�6L   �� 5�      $* no��6%    � y�$ 	Collapsed=�6K   �� 5�      $* cancel��6%    � y�$ Visible=�6O   5�      P� 5�      ( �@ MessageBoxImage$
) None=�6=   . �5�      �� $* IndormationIcon��6%    � y�$ 	Collapsed=�6X   �� 5�      $* WarningIcon��6%    � y�$ 	Collapsed=�6T   �� 5�      $* 	ErrorIcon��6%    � y�$ 	Collapsed=�6R   �� 5�      $* QuestionIcon��6%    � y�$ 	Collapsed=�6U   5�      P� 5�      ( �@ MessageBoxImage$) Warning=�6=   . �5�      �� $* IndormationIcon��6%    � y�$ 	Collapsed=�6X   �� 5�      $* WarningIcon��6%    � y�$ Visible=�6T   �� 5�      $* 	ErrorIcon��6%    � y�$ 	Collapsed=�6R   �� 5�      $* QuestionIcon��6%    � y�$ 	Collapsed=�6U   5�      P� 5�      ( �@ MessageBoxImage$) Error=�6=   . �5�      �� $* IndormationIcon��6%    � y�$ 	Collapsed=�6X   �� 5�      $* WarningIcon��6%    � y�$ 	Collapsed=�6T   �� 5�      $* 	ErrorIcon��6%    � y�$ Visible=�6R   �� 5�      $* QuestionIcon��6%    � y�$ 	Collapsed=�6U   5�      P� 5�      ( �@ MessageBoxImage$) Information=�6=   . �5�      �� $* IndormationIcon��6%    � y�$ Visible=�6X   �� 5�      $* WarningIcon��6%    � y�$ 	Collapsed=�6T   �� 5�      $* 	ErrorIcon��6%    � y�$ 	Collapsed=�6R   �� 5�      $* QuestionIcon��6%    � y�$ 	Collapsed=�6U   5�      P� 5�      ( �@ MessageBoxImage$) Question=�6=   . �5�      �� $* IndormationIcon��6%    � y�$ 	Collapsed=�6X   �� 5�      $* WarningIcon��6%    � y�$ 	Collapsed=�6T   �� 5�      $* 	ErrorIcon��6%    � y�$ 	Collapsed=�6R   �� 5�      $* QuestionIcon��6%    � y�$ Visible=�6U   5�      5�      5�      5�      5�      