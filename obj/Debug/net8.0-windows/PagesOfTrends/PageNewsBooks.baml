   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:EkatBooks.PagesOfTrendsEkatBooks.PagesOfTrends  5      ;"clr-namespace:EkatBooks.ConvertersEkatBooks.Converters  5      %clr-namespace:EkatBooks	EkatBooks  5      +    %EkatBooks.PagesOfTrends.PageNewsBooks   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:EkatBooks.PagesOfTrends     5      3conv"clr-namespace:EkatBooks.Converters     5      *local3clr-namespace:EkatBooks     5        N� Title$  PageNewsBooks��5       N� 
Background
 ����� � DataContext 5      &    EkatBooks.PageNewsBooksViewModel  5   
   -   5        	ViewModel��5   (    � 	Resources 5      -   'EkatBooks.Converters.ImagePathConverter   ImagePathConverter��%   &	          5   
   6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5!      $
��Auto �6   �� 5#      $��1* �6   .+�5(   
   �� 
����iA�$��27(�6'   $
��Bold"�65   
�������$��0��6_   .��6l   Новинки5*      �� 5-   
   $��1��6   
���   �$��2��5/   	   $��	0,0,30,12q�50   	   �� 52   
   $��20(�6   $	��800��6"   $��5,0,0,10q�61   $
��Wrap=�6C   $
��Left=�6W   $��2��6r   ��Новинки литературы — это ваш ключ к открытию свежих идей и вдохновению на новые свершения. Наши новые книги помогут вам оставаться в курсе последних тенденций, будь то в науке, искусстве, бизнесе или личностном росте. Погрузитесь в мир актуальных знаний, откройте для себя современные подходы и расширьте свои горизонты с нашими новинками!54      �� 57   
   
����iA�$��3��6(   $��7,5,20,0q�66    �� CornerRadius$ 15��6H   ._�58      �  59      �� 5:      $
��Auto �6$   �� 5;      $
��Auto �6$   �� 5<      $
��Auto �6$   �� 5=      $
��Auto �6$   .+�5A      �� $��По цене:��6   $��20(�6,   $	��Top=�6:   $��0��6R   $��15 5 5 5q�6_   $
��Left=�6q   
�������6�   �� 5B      -   5        $��Возрастание��5B      $��	125 5 5 5q�6N   $��30��6a   $	��100��6p   $��Center=�6   
�������$	��0.5q�6�   $��0��6�   �� 5C      -   5        $��Убывание��5C      $��5 5 5 5q�6L   $��30��6]   $	��100��6l   $��Right=�6{   
�������$	��0.5q�6�   $��0��6�   �� 5F      $��По названию:��6   $��20(�60   $	��Top=�6>   $��15 5 5 5q�6V   $��1��6h   $
��Left=�6u   
�������6�   �� 5G      -   5        $��А-Я��5G      $��	125 5 5 5q�6H   $��30��6[   $	��100��6j   $��Center=�6y   
�������$	��0.5q�6�   $��1��6�   �� 5H      -   5        $��Я-А��5H      $��5 8 5 5q�6J   $��30��6[   $	��100��6j   $��Right=�6y   
�������$	��0.5q�6�   $��1��6�   �� 5K      $)��#По дате добавления:��6   $��20(�67   $	��Top=�6E   $��15 5 5 5q�6]   $��2��6o   $
��Left=�6|   
�������6�   �� 5L      -   5        $��
Новые��5L      $��	125 5 5 5q�6F   $��30��6Y   $	��100��6h   $��Center=�6w   
�������$	��0.5q�6�   $��2��6�   �� 5M      -   5        $��Старые��5M      $��5 8 5 5q�6I   $��30��6Z   $	��100��6i   $��Right=�6x   
�������$	��0.5q�6�   $��2��6�   5N      5O      �� 5R   
   -   5         ��Name$ BooksListView4��5R      $��10 10 10 10q�6+   $	��Top=�6@   $��0q�6v   $��4��6�   ��6X   �� *Books+��5S      �� 5T      .R�5U      � $��5q�6   $	��200��6&    � ColumnDefinitions 5V      �� 5W      $
��Auto �6/   �� 5X      $	��500��6/   �� 5Y      $
��Auto �6/   .+�5]      �� $	��200��6m   $	��200��6y   $��0,0,20,0q�6�   $��0��6�   ��6    �� *
CoverImage+ �� 	Converter# [  6�   �� 5`      $��
10 0 10 10q�6%   $��1��69   	 J� IsHitTestVisible$	 False��6I   .+�5a      �� $��20(�6Q   
�������$��0,5,0,5q�6q   ��6(   �� *IdAuthorNavigation.Name+6q   �� 5b      $
��Bold"�6?   $��20(�6Q   $��
0,20,-60,5q�6_   ��6(   �� *Title+6_   �� 5c      $��20(�6V   $��	-6,20,0,0q�6d   ��6(   �� *Price+
 �� StringFormat$
  {0} ₽��6d   �� 5d      $��20(�6E   $��0,30,0,0q�6S   ��6(   �� *Description+6S   5e      �� 5g      -	   5        
�������$��40��5i      $��40��5j      $��20,0,-1000,0q�5k       � Cursor$
 Hand��5l      $|�False��5m       � ToolTip$) #Просмотреть отзывы��5n      .��5o      �� $��/images/star.png��6$   $��24��5p      $��24��5q      $��Uniform=�5r      ��5t      �� 5u   "    �� 
TargetType" ��6(   .�5v   &   ��  �� Property � �� �� Value$ 0q�6H   �� 5w   &    �@�Cursor$
 Hand��6?   5y   #   5|      �� 5~      -
   5        $��Купить��5~   !   
����iA�
�������$	��100��6o   $��40��6{   $��20,0,-1500,0q�6�   !  System.Windows.SystemColors    ActiveCaptionTextBrushKey J� OpacityMask# �  6�   $
 Hand��6�   $|�False��6�   ��5      �� 5�   "   " ��6(   .�5�   &   ��  � ��$ 0q�6H   �� 5�   &    �@�Cursor$
 Hand��6?   5�   #    J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�   "   5�      5�      5�      5�      5�      5�      