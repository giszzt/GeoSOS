   M S B A M L   `   `   `  ����  	FrameWork)    #FrameWork.Gui.TreeViewOptionsDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6K   g cICSharpCode.Core.Presentation, Version=4.4.2.9749, Culture=neutral, PublicKeyToken=f829da5c02be14ee3core(http://icsharpcode.net/sharpdevelop/core  6�   j fICSharpCode.SharpDevelop.Widgets, Version=4.4.2.9749, Culture=neutral, PublicKeyToken=f829da5c02be14ee9widgets+http://icsharpcode.net/sharpdevelop/widgets  6�   $	��414��5   	   $	��552��5   	   $	��260��5   	   $	��480��5   	     � WindowStartupLocation$  CenterOwner=�5   	   .��5	      �  � RowDefinitions 5
   
   �� 5      $��1* �5      �� 5      $
��Auto �5      .+�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �5      �� 5      $��1* �5      .+�5      R� -   5        $��treeView��5      $��0 0 2 0q�6'   ��5      �� 5       �� 
TargetType" Q�6   .�5      ��  �� Property �@Q�
IsExpanded �� Value 67   �� *
IsExpanded+ �� Mode$ TwoWay=��� 5       �@Q�
IsSelected 67   �� *
IsSelected+$ TwoWay=�5      ��5      �� 5        �� ItemsSource 6/   �� *
Children+.R�5!      ��  �� Orientation$ 
Horizontal=�6%   $��	-16 0 0 0q�6>   .+�5"      �� $��16��6=   $��16��6H   ��6$   �� *Image+6H   �� 5#      ��6(   �� *Title+5$      5%      5'      �� 5(      $��2��6   �� 5)      -   5        $��optionPanelScrollViewer��5)      $��1��5*      $��Disabled=�5+      $
��Auto=�5,      .��5-      ]� .+�5.      �� -   5        $��optionPanelTitle��50      $	��Top=�5/      $��Tahoma)�51      $
��14pt(�6   $
��Bold"�6+   	 �� Padding$	 8q�52      ��54      �� 55   	   
 �� 
StartPoint$

 0, 0&�6    �� EndPoint$
 0, 1&�6/   ��57      �  � Offset$	 0.0Q�6    � Color# � ��58      � 59      $	 1.0Q�6   # � ��5:      5<   
   5>      �� 5?      -   5        $��optionPanelContent��5?   '   5@      5A      5B      =  7ICSharpCode.SharpDevelop.Widgets.UniformGridWithSpacing  5C   
    A� Columns$ 2��6)   $��0��65   $��1��6E   $��Right=�6R   $��	0,4,12,12q�6n   .+�5D      �� -   5        $��okButton��5G       �� 	IsDefault . 0  *ICSharpCode.Core.Presentation.GlobalStyles   ButtonStyle#��Z 5H      ��5E      5  /ICSharpCode.Core.Presentation.LocalizeExtension  *Global.OKButtonText+5I      �� 5J      -   5        $��cancelButton��5M       �� IsCancel . #��Z 5N      ��5K        *Global.CancelButtonText+5O      5P      5Q      5R      