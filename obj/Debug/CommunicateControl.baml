   M S B A M L   `   `   `  ����  Active Dictionary*    $Active_Dictionary.CommunicateControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      -   5          >�Name$  UserControl��5
      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   � -   5        $ grdCommunicate��5      .+�5      �� -   5         ��Name$ btnFB��5      $
��Left=�6$   $��81.998��6?   $��444.468,0,0,0q�6O   $	��Top=�5      $��104.679��6-    � Cursor$
 Hand��6=   $��32(�6K   $
��Bold"�6Y      ahViolet#���   5       � ToolTip$A ;زيارة الصفحة الشخصيه على فيس بوك��6=   ��5      Z� .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Click��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      >  8Microsoft.Expression.Interactivity.Media.PlaySoundAction     Source$ Assets/Sound/click.wav?�6-   5      .��5      �� -   5        	 ��Name$	 image��5      $��Assets/fb.png��6'   $��UniformToFill=�6>   $
 Hand��6V   5      �� 5      -   5        $ 
btnTwitter��5      $
��Left=�6)   $��112.224,0.851,0,0q�6D   $	��Top=�6_   $
 Hand��5   @   $��32(�6N   $
��Bold"�6\   #���   5      $$ تابعني على تويتر��6=   ��5      �� *Width+
 �� ElementName$
 btnFB����5   X   Z� ��5      �� *Height+$
 btnFB�� 5        5      $ Click��6%   . 5         $ Assets/Sound/click.wav?�6-      Volume$ 1Q�6M   5!      .��5#      �� $��Assets/tw.png��6   $��UniformToFill=�6/   $
 Hand��6G   5$      �� 5%      -   5        $ 
btnWebsite��5%      $
��Left=�6)   $��0,0.851,0,0q�5&      $	��Top=�6*   $
 Hand��5'      $��32(�6#   $
��Bold"�61   #���   6C   $ موقعي الشخصي��5(      ��5%   D   �� *Height+$
 btnFB����5&   B   �� *Width+$
 btnFB����5(   ,   Z�  5)        5*      $ Click��6%   . 5+        $ Assets/Sound/click.wav?�6-   5,      .��5.      �� $��Assets/web.png��6   $��UniformToFill=�60   $
 Hand��6H   5/      �� 50      -   5        $ 
btnYoutube��50      $
��Left=�6)   $��332.789,0.851,0,0q�51      $	��Top=�60   $
 Hand��52      $��32(�6#   $
��Bold"�61   #���   6C   $4 .اشترك في قناتي على يوتيوب��53      ��50   D   �� *Height+$
 btnFB����51   H   �� *Width+$
 btnFB����53   9   Z�  54        55      $ Click��6%   . 56        $ Assets/Sound/click.wav?�6-   57      .��59      �� $��Assets/yt.png��6   $��UniformToFill=�6/   $
 Hand��6G   5:      �� 5;      -	   5        $ btnGP��5;      $
��Left=�6$   $��222.014,0.851,0,0q�6?   $	��Top=�6Z   $
 Hand��5<   @   $��32(�6N   $
��Bold"�6\   #���   5=      $# +تابعني على جوجل��6=   ��5<      �� *Width+$
 btnFB����5=   X   Z� ��5>      �� *Height+$
 btnFB�� 5?        5@      $ Click��6%   . 5A        $ Assets/Sound/click.wav?�6-   5B      .��5D      �� $��Assets/gp.png��6   $��UniformToFill=�6/   $
 Hand��6G   5E      5F      5G      5H      