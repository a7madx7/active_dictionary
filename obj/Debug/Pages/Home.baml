   M S B A M L   `   `   `  ����  Active Dictionary"    Active_Dictionary.Pages.Home   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      Z VFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  5	      -   5          >�Name$  userControl��5   J   $	��Top=�5   ,   $��643.143��6D    � 	Resources 5         ItemsPanelTemplate1 K� IsItemsHost $� CanHorizontallyScroll $� CanVerticallyScroll J� RenderTransformOrigin � LayoutTransform �� ScaleY �� ScaleX	 �� AngleY
 �� AngleX �� Angle��%�  &	        �� 5   
   .R�5      $�  . $��Stretch=�68   $��Stretch=�6V    .  . $ 0.5,0.5&�5   %    5      V� 5      .~�5      �� $ 1Q�6)   $ 1Q�64   �� 5      $	 0Q�6(   $
 0Q�63   �� 5      $ 0Q�6*   T� 5      5      z�5      V� 5      .~�5      �� $ 1Q�6)   $ 1Q�64   �� 5      $	 0Q�6(   $
 0Q�63   �� 5       $ 0Q�6*   T� 5!      5"      5$      5%      5&      .��5'      �� $��Center=�6   $��Stretch=�67   .��5(   
   ]� -   5         ]�Name$ ParentPanel��5(      $	��200��6)   ��. .+�5)      � -   5         �Name$ 	SearchBox��5)      $	��Top=�6&   $��31��6;    � ColumnDefinitions 5*      �� 5+      �� 5,      $��35 �6'   .+�5.      �� -   5         ��Name$ txtSearchTerm��51       �� TextWrapping$
 Wrap=�5/      $�� ��50      $��18.667(�53   /    � ToolTip$X Rابدأ الكتابه ليقوم القاموس بالترجمه في الحال��6A    � Cursor$ Arrow��54      $��2��6$    �� 	IsEnabled . $��31��6T   �� 55      -   5         ��Name$ clearButton��55      $��1.213,-3.571,-0.71,-3.571q�6D    J� Opacity$	 0.3Q�6g   $��1��56   /   $��0q�6?   }�.  J� SnapsToDevicePixels .  �� IsCancel . $
 Hand��57   D   $ Clear search text��6R   ��55   .   Z� ��56      Z� .  (System.Windows.Interactivity.Interaction   Triggers 58      /  )System.Windows.Interactivity.EventTrigger  59         	EventName$ 
MouseEnter��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5:      B  <Microsoft.Expression.Interactivity.Core.ChangePropertyAction     PropertyName$ Opacity��66      Value$ 1��6M   5;        5<      $ 
MouseLeave��6)   . 5=        $ Opacity��66   $	 0.3��6M   5>        5?      $ Click��6)   . 5@      >  8Microsoft.Expression.Interactivity.Media.PlaySoundAction     Source$ Assets/Sound/clear.wav?�61      Volume$ 1Q�6Q     5A      8  2System.Windows.Interactivity.TargetedTriggerAction   
TargetName$ txtSearchTerm��66   $
 Text��5C   6       TargetObject  5B   6   �� ! �� ElementName$! txtSearchTerm��5C   6   5D      .��5F      �� .+�5G      F� $��30.647��6   $��25.857��6.   " �� Left$" -15.773��6>   
# �� Top$# -11.701��6T   $ �� Stretch$
$ Fill=�6i   ���	#FF000000�����9�D\��౉��CQ9�-!�l�@@���x��� Zb�}���x��Q�z�-!�l�@@� ;��C��z�D\�������� Zb`-k�������9�D\�����y�xr��X��I��/@�� u�� u� u uӸ�C u@�exL�@�e�)��@�e@���@�e�^���C 6n u 6n��� 6n�@�� 6n�I��|txr��S����������0@L�+���0@��[���y���@��Ӏ��@�����p��0ѻD��Ѥ@3�iY�xU'�~MxU't,:�@3HA.�0ѻ豥�����ǝ��������$��� u$����O*$����7̦���7���р�7$*�Ӏ�7���O*��� u������@��5I      5J      5K      5L      � 5M      -   5        $ 
grdResults��5M      $��Bottom=�6'   $��611.428��6?   $
��Left=�6P   ��5N      �� *ActualWidth+$! txtSearchTerm�� 5P        HomeIconData7  1FirstFloor.ModernUI.Windows.Controls.ModernButton% �� 
TargetType& �� Property' �� Value��%�  &	       '  /    7  B�5Q      ��F1 M 37.75,19L 38.25,19C 38.25,19 57,19 57,39C 57,48 55,51 54,52C 54,52 51,54 51.9999,51.25C 51.9999,48.9362 53,44 53,44C 53,44 54,44 54,39C 54,33 50,22.5 39,22.5L 37,22.5C 26,22.5 22,33 22,39C 22,44 23,44 23,44C 23,44 24.0001,48.9362 24.0001,51.25C 25,54 22,52 22,52C 21,51 19,48 19,39C 19,19 37.75,19 37.75,19 Z M 26.5533,39.1655C 28.194,38.9349 29.711,40.0781 29.9416,41.7188L 31.4725,52.6117C 31.7031,54.2524 30.56,55.7694 28.9192,56C 27.2785,56.2306 25.2615,55.0875 25.0309,53.4467L 23.5,42.5538C 23.2694,40.9131 24.9126,39.3961 26.5533,39.1655 Z M 49.4467,39.1655C 51.0874,39.3961 52.7306,40.9131 52.5,42.5538L 50.9691,53.4467C 50.7385,55.0875 48.7215,56.2306 47.0808,56C 45.44,55.7694 44.2969,54.2524 44.5275,52.6117L 46.0584,41.7188C 46.289,40.0781 47.806,38.9349 49.4467,39.1655 Z�6O  �� 5S      "%  6   .�5T      �� & �@ IconData8'   65   �� 5U      & � ��$' 0,0,8,0q�63   �� 5V      & � ��$' 2q�6<   5W      5Y      ( � RowDefinitions( 5\      �� 5]      �� 5^      $��97.428 �6$   .+�5`      �� -   5        ) ��Name$) 
lstResults��5`      }�.  .   StarredListBox#���  5e      #���   6C   $��18.667(�5f      $��0,0,0,4.951q�6,   5g      �� 5h      $��97.022��6   $	��Top=�6)   $��0,0.049,0,0q�6A   $��1��6V   ._�5i      � .+�5j      �� -   5        * ��Name$* lblTranslation��5l   $   $��WrapWithOverflow=�5j   $   $��29.333(�5m   $   $
��Bold"�5n   $   $
��Auto=�5p   $   ��. + � UseLayoutRounding+ . }�.  . $��RightToLeft=�5u   :   , �� TextAlignment$, Center=�6V   $	 0.5Q�5v   $   $��0,19,0,0.022q�62   ��5k   $   �� *SelectedValue+$! 
lstResults����5o   $   Z� ��5u   $   Z� 5v   2     5w      -	   5        -  Name$- btnSpeak��5w   +   .   EllipseDiameter$. 82Q�6V   /   EllipseStrokeThickness$/ 3Q�5x   +   0   	IconWidth$0 60Q�6F   1   
IconHeight$1 60Q�6U   $	 0.3Q�6e   2 �� 	IsDefault2 . $
 Hand��5y   <   $- 'أضغط لسماع نطق الكلمه��6J   $#��-0.373,28.397,214.302,-20.816q�5z   +   5{      5|      5}      5      5�      5�      