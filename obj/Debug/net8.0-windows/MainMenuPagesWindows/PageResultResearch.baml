   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:EkatBooks.MainMenuPagesWindowsEkatBooks.MainMenuPagesWindows  5      ;"clr-namespace:EkatBooks.ConvertersEkatBooks.Converters  5      %clr-namespace:EkatBooks	EkatBooks  5      7    1EkatBooks.MainMenuPagesWindows.PageResultResearch   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:EkatBooks.MainMenuPagesWindows     5      3conv"clr-namespace:EkatBooks.Converters     5      *local7clr-namespace:EkatBooks     5        N� Title$  PageResultResearch��5       � DataContext 5      +   %EkatBooks.PageResultResearchViewModel  5   
   -   5        	ViewModel��5   -    � 	Resources 5      -   'EkatBooks.Converters.ImagePathConverter   ImagePathConverter��%   &	          5   
   6"   5      .��5      � -   5         �Name$ GridMain��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5#   
   �� -   5         ��Name$ ResultLabel��5#      
����iA�$��27(�6<   $
��Bold"�6J   
�������$��0��6t   .��6�   !Результат поиска5%      �� 5(   
   $	��700��6   $��-270,50,0,10q�6#   $
��0.15q�69   $��0��6P   �� 5+   
   -   5         ��Name$ BooksListView4��5+      $��10,20,10,10q�6+   $	��Top=�6@   $��0q�6v   $��1��6�   ��6X   �� *Books+��5,      �� 5-      .R�5.      � $��5q�6   $	��200��6&    5/      �� 50      $
��Auto �6/   �� 51      $	��500��6/   �� 52      $
��Auto �6/   .+�55      �� $	��200��6m   $	��200��6y   $��0,0,20,0q�6�   $��0��6�   ��6    �� *
CoverImage+ �� 	Converter# [  6�   �� 57      $��
10 0 10 10q�6%   $��1��69   	 J� IsHitTestVisible$	 False��6I   .+�58      �� $��20(�6Q   
�������$��0,5,0,5q�6q   ��6(   �� *IdAuthorNavigation.Name+6q   �� 59      $
��Bold"�6?   $��20(�6Q   $��
0,20,-60,5q�6`   ��6(   �� *Title+6`   �� 5:      $��20(�6V   $��	-6,20,0,0q�6d   ��6(   �� *Price+
 �� StringFormat$
  {0} ₽��6d   �� 5;      $��20(�6E   $��0,30,0,0q�6U   ��6(   �� *Description+6U   5<      �� 5>      -   5        $��Купить��5>   !   
����iA�
�������$	��100��6o   $��40��6{   $��20,0,-1500,0q�6�   !  System.Windows.SystemColors    ActiveCaptionTextBrushKey J� OpacityMask# �  6�    � Cursor$
 Hand��6�   $|�False��6�   ��5?      �� 5@   "    �� 
TargetType" ��6(   .�5A   &   ��  �� Property � �� �� Value$ 0q�6H   �� 5B   &    �@�Cursor$
 Hand��6?   5C   #    J� Effect 5E      3  -System.Windows.Media.Effects.DropShadowEffect  5F   "   5H      5I      5J      5L       � 5N   
   -   5        $��	MainFrame��5N        � NavigationUIVisibility$ Hidden=�6!   $��1��6A   $��0��6N   5O      5Q      