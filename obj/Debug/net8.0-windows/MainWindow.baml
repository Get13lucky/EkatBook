   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EkatBooks	EkatBooks  5   	       EkatBooks.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:EkatBooks     5   	   -   5          � Title$  EkatBook��5
   	   $	��594��6   $	��878��6'    � Icon$ /images/bookFeather.PNG��63    � WindowState$ 	Maximized=�6T   .��5      �� -   5         ��Name$ PageScrollViewer��5      $
��Auto=�6-   .��5   
   � -   5         �Name$ MainGrid��5       � ColumnDefinitions 5      �� 5      $	��89* �6#   61   �� 5      $	��300��6#   $
��311* �62   6A    � RowDefinitions 5      �� 5      $	��100��6    $
��100* �60   5      �� 5      $
��284* �6    5      �� 5      $	��100 �6    .+�5%      �� -   5         ��Name$ 	MainStack��5%      $��2��6-   $��0��6A   
����iA�.+�5&      �� -   5        
����iA�$
��Left=�5&   E   $��0 -1 700 10q�6`   
����iA���5'      �� 5(       �� 
TargetType" ��6    .�5+      �� 	 �� Property	 � ��
 �� Value$
 0q�6@   �� 5,      	 �@�Cursor$

 Hand��67   5/      ��52      �� 53      $��\images\bookFeather.PNG��6    $	��130��6A   $��80��6Q    � Cursor$
 Hand��6_   55      57      o� 5;      -   5         o�Name$ MainMenu��5;      
����iA�
�������$��130 -90 130 90q�6T   $
��1200��6l   $	��300��6|   $
��Left=�6�   $	��670��6�   .@�5<      m� -   5        $��Магазин��5<      $��17(�60   6S   �� 5=      m� 5>      -   5        $��Издательство��5>      $��17(�65   6^   �� 5?      m� 5@      -	   5        $��Новости��5@      $��17(�60   6Q   �� 5A      m� 5B      -
   5        $��Авторы��5B      $��17(�6/   6R   �� 5C      5G      �� 5H      -   5         ��Name$ inputTextBox��5H      $��135 -90 235 30q�60   $��26��6H   $	��292��6W   $
��Left=�6f   $	��286��6�   $��23��6�   $��Найти книгу...��6�   
�������6�   �� 5I      -   5         ��Name$ ResearchButton��5I      $��435 -90 235 30q�6M   $��24��6e   $��24��6t   $��28��6�   $��23��6�   
�������$
��Left=�6�   .��5J      �� $��\images\loupe.png��6   $��24��67   $��30��6F   $��28��6T   $��23��6_   $��-1.4 -2 -2 -2q�6k   $
��Left=�6�   5L      �� 5M      $=��7Время работы: Пн-Пт: с 10:00 до 18:00��6   
�������$��10,-75,220,40q�5O      $��40��5P      $	��250��5Q      $��20(�5R      $��Right=�5S      �� 5T      $(��"                 8 (912) 500 33 77��6   
�������$��10,-80,10,40q�5W      $��40��5X      $	��250��5Y      $��20(�5Z      $��Right=�5[      �� 5`      -   5        $��50��5`   -   $��50��6<   $��-40 -200 360 15q�6K   $��Right=�6e   !  System.Windows.SystemColors    ActiveCaptionTextBrushKey J� OpacityMask# �  6�   $
 Hand��6�   $|�False��6�    J� RenderTransformOrigin$	 1,1&�6	  ��6�   Z�  J� Effect 5d      3  -System.Windows.Media.Effects.DropShadowEffect  5e      ��5i      �� 5j      $��\images\basket.png��6    $��Right=�6?   $
 Hand��6[   5l       � ToolTip 5o      _� 5p      $)��#Ваша корзина пуста!��6"   
�������
���   �$��15(�6f   $��50��6t   $	��100��6�   5s      �� 5u      -   5        
����iA�
����iA�$��Right=�5u   c   $��0 -220 50 0q�6   $��50��6�   $��50��6�    5x        5y      ��5|      �� 5}      $��\images\per.png��6    $��Right=�6=   $
 Hand��6Y   5      5�      5�      �� 5�      -   5        $ MainStack_2��5�      $��1��6/   .+�5�      �� -   5         ��Name$ MainTabControl��5�      $��Stretch=�66   $��0q�6T   $��3q�6h   $��16(�6s   
�������.@�5�      �� $��
Книги��6    $��0q�60   $��Stretch=�6D   $	��133��6b   .��5�      �� -   5        $	 Mas��5�   %   .+�5�      �� -   5        $��0q�5�   %   
�������$��0 0 0 5q�6`   .��5�   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5�   &   �� $��\images\money.png��6,   $��20��6G   $��20��6R   $��5q�6^   �� 5�   &   $(��" Бизнес-литература��60   $��Center=�6J   $��5q�6e   $	��228��6q   $��20��6}   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   .+�5�   &   �� $��\images\computer4.png��6,   $��20��6K   $��18��6V   $��	-10 0 0 0q�6b   �� 5�   &   $6��0   Компьютерная литература��60   $	��226��6R   $��20��6^   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\atom.png��6,   $��20��6F   $��18��6Q   $��
-10 0  0 0q�6]   �� 5�   &   $-��'   Наука и образование��60   $	��226��6N   $��20��6Z   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\museum.png��6,   $��20��6H   $��18��6S   $��
-10 0  0 0q�6_   �� 5�   &   $3��-   Публицистика и история��60   $	��226��6Q   $��20��6]   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\medicine.png��6,   $��20��6J   $��18��6U   $��
-10 0  0 0q�6a   �� 5�   &   $4��.   Медицинская литература��60   $	��226��6Q   $��20��6]   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\psycho.png��6,   $��20��6H   $��18��6S   $��
-10 0  0 0q�6_   �� 5�   &   $<��6   Психологическая литература��60   $	��226��6U   $��20��6a   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\law.png��6,   $��20��6E   $��18��6P   $��
-10 0  0 0q�6\   �� 5�   &   $4��.   Юридическая литература��60   $	��226��6Q   $��20��6]   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������$��0 0 0 5q�6L   .��5�   "   �� $ 
Horizontal=�6-   $��Right=�6F   $��0 5 1 1q�6b   .+�5�   &   �� $��\images\history.png��6,   $��20��6I   $��18��6T   $��
-10 0  0 0q�6`   �� 5�   &   $:��4   Художественная литература��60   $	��226��6T   $��20��6`   5�   #   5�      5�      5�      �� 5�      $��Тренды��6   $��0q�6/   $��Stretch=�6C   $	��133��6a   
�������.��5�      � -   5         �Name$ MainWrapPanel��5�   $   $��5 2 0 0q�6;   .+�5�      �� -   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\new.png��6,   $��20��6E   $��20��6P   $��0 0 7 0q�6\   �� 5�   &   $�� Новинки��60   $��Center=�6@   $��5q�6[   $	��228��6g   $��20��6s   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\bestseller.png��6,   $��20��6L   $��20��6W   $��0 0 7 0q�6c   �� 5�   &   $�� Бестселлеры��60   $��Center=�6D   $��5q�6_   $	��228��6k   $��20��6w   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\soon.png��6,   $��20��6F   $��20��6Q   $��0 0 7 0q�6]   �� 5�   &   $�� Скоро��60   $��Center=�6>   $��5q�6Y   $	��228��6e   $��20��6q   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\like.png��6,   $��20��6F   $��20��6Q   $��0 0 7 0q�6]   �� 5�   &   $�� Рекомендуем��60   $��Center=�6D   $��5q�6_   $	��228��6k   $��20��6w   5�   #   5�      �� 5�      -   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\camera.png��6,   $��20��6H   $��20��6S   $��0 0 7 0q�6_   �� 5�   &   $�� Экранизация��60   $��Center=�6D   $��5q�6_   $	��228��6k   $��20��6w   5�   #   5�      �� 5�      -    5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\cup.png��6,   $��20��6E   $��20��6P   $��0 0 7 0q�6\   �� 5�   &   $��
 Топ 10��60   $��Center=�6?   $��5q�6Z   $	��228��6f   $��20��6r   5�   #   5�      �� 5�      -!   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\random.png��6,   $��20��6H   $��20��6S   $��0 0 7 0q�6_   �� 5�   &   $#�� Cлучайная книга��60   $��Center=�6H   $��5q�6c   $	��228��6o   $��20��6{   5�   #   5�      �� 5�      -"   5        $��0q�5�   %   
�������.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��\Images\medal.png��6,   $��20��6G   $��20��6R   $��0 0 7 0q�6^   �� 5�   &   $*��$ Нобелевская премия��60   $��Center=�6K   $��5q�6f   $	��228��6r   $��20��6~   5�   #   5�      5c     5g     5h     5k     ]� 5m     $��0��6   $��2��6(   
����iA�$��2��6L   .+�5n     �� .+�5p     �� $ 
Horizontal=�6!   $��	20 10 0 0q�6:   .+�5r     �� $3��-г. Екатеринбург, Крауля 74��6$   
�������$��17(�6Y   �� 5u     $��EkatBook��6$   
�������$��Center=�6G   $��30(�6d   $��Modern No. 20)�6r   $��
360 -6 0 0q�6�   5v     �� 5y     $ 
Horizontal=�6!   $��
60 -10 0 0q�6:   .+�5{     �� -#   5        
����iA�$��50��5{  ;   $��50��6G   ��5|     �� 5}  "   $��\images\telegram.png��6(   $��50��6F   $��50��6U   $��69��6c   $��46��6n   5     �� 5�     -$   5        
����iA�$��50��5�  ;   $��64��6G   ��5�     �� 5�  "   $��\Images\whats3.png��6(   $��50��6D   $��60��6S   $��73��6a   $��46��6l   $��10 0 0 1q�6x   5�     �� 5�     $.��(мир книг в одном клике��6%   
�������$��25(�6V   $��330 10 450 40q�6e   $��Georgia)�6|   $��42��6�   �� 5�     $��© 1980 - 2025 EkatBook��6%   
�������$��15(�6V   $��-900 50 140 0q�6e   $��42��6}   $	��301��6�   �� 5�     $��8 (912) 500 33 77��6$   
�������$��-170 10 450 40q�6U   $��40��6m   $	��250��6|   $��25(�6�   $��Right=�6�   5�     5�     5�      � 5�     -%   5        $��	MainFrame��5�       � NavigationUIVisibility$ Hidden=�6&   $��1��6F   $��1��6S   5�     5�     5�     