   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5      ?$clr-namespace:WpfApplication1.CommonWpfApplication1.Common  5        System.Windows.InteractivityqPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      e aDe.TorstenMandelkow.MetroChart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d5b8d1bbc99c4a2awTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChartDe.TorstenMandelkow.MetroChart 5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source+    %WpfApplication1.RiskIDToNameConverter   RiskIDToNameConverter    WpfApplication1.ResultGrid �� 
TargetType �� Property �� Value �� 
TargetType J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth	 � RowDefinitions
 � ColumnDefinitions �� EndPoint �� 
StartPoint � Color � Offset �� Orientation ��Name  toolBarAddButtonStyle �� RelativeSource �� Path&   System.Windows.Controls.DataGrid  Name  GridCellStyle   	CellStyle �� AlternationCount   AutoGenerateColumns   MinRowHeight  DataGridStyle  ColumnHeaderStyle   ColumnHeaderStyle  GridRowStyle   RowStyle   SelectionMode   CanUserAddRows �� Mode �� UpdateSourceTrigger �� SelectedItem   RowHeaderTemplate  d�Name! � Cursor"   Columns0  *System.Windows.Controls.DataGridTextColumn,  &System.Windows.Controls.DataGridColumn#   Width5  /System.Windows.Controls.DataGridLengthConverter$   MinWidth%   
IsReadOnly&   Header1  +System.Windows.Controls.DataGridBoundColumn'   Binding( �� 	Converter9  3De.TorstenMandelkow.MetroChart.ClusteredColumnChart  ChartStyleVariableSize.	  (De.TorstenMandelkow.MetroChart.ChartBase) 	  
ChartTitle* 	  ChartSubTitle+ 	  SeriesSource, 	  SeriesTemplate0
  *De.TorstenMandelkow.MetroChart.ChartSeries- 
  DisplayMember. 
  ValueMember/ 
  SeriesTitle0 �� Triggers�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      'localclr-namespace:WpfApplication1  5      /common$clr-namespace:WpfApplication1.Common  5      ViPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5      ^chartTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChart  5        5      �� 5	   
   $! ../Common/CommonStyles.xaml?�6   �� 5
   
   $' !../Common/CustomDataGridView.xaml?�6   �� 5   
   $# ../Common/Charts/Generic.xaml?�6   %t
  &	        '       7  7  7  7  7  7   7     5      6"   �� 5      "  6.   .�5   
   ��  � �� 5      �� 5      "  6"   .R�5      �� ���	#FF0D3883$��0q�65   ���	#FF133E8B 5        5      $ 	#FF0D3883��6/   $ 0Q�6A   ._�5      �� $��1q�6!   ���	#FF1B3F95._�5      � 	 5   "   �� 5   &   $��50 �64   �� 5    &   $��* �64   
 5#   "   �� 5$   &   $
��Auto �67   �� 5%   &   $��* �67   �� 5&   &   $
��Auto �67   .+�5(   "   �� $��0��6)   $��3��66   ��5)   &   �� 5*   *   $ 0.5,1&�6>   $ 0.5,0&�6O   .��5+   .   � $ 	#FF6A8ED8��6;   $ 0Q�6M   � 5,   .   $ 	#FF3365C2��6;   $ 1Q�6M   5-   +   5/   #   �� 50   "   $��0��6-   $��0��6:   $ 
Horizontal=�6J   $��Center=�6c   $��Right=�6~   $��0,0,5,0q�6�   .+�51   &   �� $ AddTag��6-   $��5,0,0,0q�6=   $
��Test��6N   8��  ��6�   ��  �� *TemplatedParent+$ UpdateCommand�53   #   �� 54   "   $��1��6/   $��0��6<   $��3��6L   $
��Auto=�6a   $��Disabled=�6�   .��55   &   � 	 56   *   �� 57   .   $
��Auto �6<   �� 58   .   $��* �6<   .+�5:   *     $ mainGrid��6A   $��0��63   $
��Left=�6S   $	��Top=�6n   8  $ 2��5;   %    .  $ 26Q�6V   8�� 8  8  $ Single=�5<   H   .  ��6%   ��  �� *TemplatedParent+$ filteredCollection�$ TwoWay=�$ PropertyChanged=� 6�   ��  �� *TemplatedParent+$ SelectedResult�$ TwoWay=�$ PropertyChanged=� 5>   .   �� 5?   2   .R�5@   6   d� $  RowHeaderToggleButton��6C   $
! Hand��6c   5A   3   " 5C   .     5D   2   $	# 40* 6F   $$ 50Q�6R   % . $
& Risk��6�   ' 6`   �� *ID+8(  6�     5F   2   $
# 120* 6E   $$ 50Q�6R   % .  $& 
Importance��6�   ' 6a   �� *
Importance+6�     5G   2   $
# 120* 6E   $$ 50Q�6R   % .  $& InfluenceProb��6�   ' 6a   �� *InfluenceProb+6�     5H   2   $
# 120* 6E   $$ 50Q�6R   % .  $& ExternalRate��6�   ' 6a   �� *ExternalRate+6�     5I   2   $
# 120* 6E   $$ 50Q�6R   % .  $& ProtectionRate��6�   ' 6a   �� *ProtectionRate+6�   5L   +     5M   *   $��1��6E   8�� $&)  Column Chart with dynamic series��6�   $+* %Chart with a dynamic width and height��5N   E   + 6~   ��  �� *TemplatedParent+$ Series�$ TwoWay=�$ PropertyChanged=�, 5O   .   �� 5P   2   .R�5Q   6   
  $- Category��6z   $. Number��6�   / 6J   �� $ SeriesDisplayName���6�   �� $ Items�5R   3   5T   +   5U   '   5V   #   5W      5X      5Y      0 5Z      5^      5`      5a      5d      