   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:WpfApplication1.Common.ChartsWpfApplication1.Common.Charts  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ButtonBrush  �� Color  IconButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName	 �� TargetProperty
 g� Duration	 W� To ��Name J� IsHitTestVisible J� Opacity ��Name ��Name  ScrollBarBackgroundBrush  HoverBorderBrush  SelectedBackgroundBrush  SelectedForegroundBrush  DisabledForegroundBrush  NormalBrush  HorizontalNormalBrush 	 
GlyphBrush 
 
LightColor  PressedBrush  HorizontalNormalBorderBrush  NormalBorderBrush  
LightBrush �� 
StartPoint �� EndPoint � Color � Offset  HorizontalLightBrush  	DarkBrush  DisabledBackgroundBrush  WindowBackgroundBrush  DefaultedBorderBrush  PressedBorderBrush  DisabledBorderBrush  SolidBorderBrush  LightBorderBrush  LightColorBrush�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      5local+clr-namespace:WpfApplication1.Common.Charts  5      %3  &	        &	 #     &	 �    &	      &	     &	 <    &	 \    &	     &	 �    &		 �    &	 �    &	 �    &	     &	 8    &	 �    &	 �	    &	 6
    &	 T
    &	 r
    &	     &	 �    &	 �    &	     &	      �� 5      $  	#FFFFFFFF��6*   �� 5      " ��6$   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��
 ���� 6(   �� 5	   
    � ��$ 0q�6,   �� 5
   
    � ��$ 0q�6$   �� 5   
    � ��$ Center=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ Segoe UI)�6'   �� 5   
    � ��$ Light"�6'   �� 5   
    � ��$ 32(�6%   �� 5   
    � ��$ 25��6"   �� 5   
    � ��$ 25��6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �  5        5      CommonStates��. 5   "     
Normal��6.     5   "   PointerOver��. 5   &   �� .��5   *   W� $ BackgroundVisualWhite��6:   $	 Opacity�6h   $
 0D�6�   $ 1X�6�   5   '   5   #     5   "   	MouseOver��. 5   &   �� .��5    *   W� $ BackgroundVisualWhite��6:   $	 Opacity�6h   $
 0D�6�   $ 1X�6�   5!   '   5"   #     5#   "   Pressed��. 5$   &   �� .��5%   *   W� $ BackgroundVisualWhite��6:   $	 Opacity�6h   $
 0D�6�   $ 1X�6�   5&   '   5'   #     5(   "   Disabled��. 5)   &   �� .��5*   *   W� $ ContentPresenter��6:   $	 Opacity�6c   $
 0D�6�   $	 0.5X�6�   5+   '   5,   #   5-        5.      FocusStates��. 5/   "     Focused��6.     50   "   	Unfocused��6.     51   "   PointerFocused��6.   52      .+�54      �� $ BackgroundVisualWhite��6$   ���	#FFE0EBF1 .  $ 0Q�6m   �� 55      $ Border��6!   $
 0.75Q�61   #��z��6@   #��z��6l   #��z��6�   $��0q�6�   ._�56      �� $ ContentPresenter��6-   #��z��6G   #��z��6�   #��z��6�   #��z��6�   #��z��6  #��z��6K  57      58      59      5;      5<      �� 5?      $  	#FFB9D1E0��67   �� 5A      $  Black��6/   �� 5B      $	  Red��66   �� 5C      $  Yellow��66   �� 5D      $  	DarkGreen��66   �� 5E      $  Transparent��6*   �� 5F      $  	#FF71A4C1��64   �� 5H      #  � 
 6)   �� 5I      #  � 
 6+   �� 5J      $  	#FF5A95B8��6:   �� 5L      #  � 
 60   �� 5R      $	 0,0&�6-   $	 0,1&�6>   ��5T      � � 5U      $
 #FFF��6   $	 0.0Q�6,   � 5V      $
 #EEE��6   $	 1.0Q�6,   5W      5Y      �� 5[      $	 0,0&�67   $	 1,0&�6H   ��5]      � � 5^      $
 #FFF��6   $	 0.0Q�6,   � 5_      $
 #EEE��6   $	 1.0Q�6,   5`      5b      �� 5d      $	 0,0&�6,   $	 0,1&�6=   ��5f      � � 5g      $
 #FFF��6   $	 0.0Q�6,   � 5h      $
 #AAA��6   $	 1.0Q�6,   5i      5k      �� 5n      $
  #EEE��66   �� 5p      $
  #FFF��64   �� 5w      $	 0,0&�67   $	 0,1&�6H   ��5y      � � 5z      $
 #777��6   $	 0.0Q�6,   � 5{      $
 #000��6   $	 1.0Q�6,   5|      5~      �� 5�      $	 0,0&�65   $	 0,1&�6F   ��5�      � � 5�      $
 #444��6   $	 0.0Q�6,   � 5�      $
 #888��6   $	 1.0Q�6,   5�      5�      �� 5�      $
  #AAA��62   �� 5�      $
  #888��6/   �� 5�      $
  #AAA��6/   �� 5�      $  	#FF4B89AD��6.   5�      