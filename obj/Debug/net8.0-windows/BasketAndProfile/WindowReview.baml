   M S B A M L   `   `   `  ����  	EkatBooks-    'EkatBooks.BasketAndProfile.WindowReview   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$   Отзывы о книге��5   	   $	��600��6    $	��800��6-    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       '�� L     �� 5	   
   " ��6   .�5
      ��  � ��$ Center=�6;   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�6;   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $ ��Отзывы о книге��6!   $��16(�67   $
��Bold"�6E   $��0,0,0,10q�6W   $��Center=�6i   �� 5   
   -   5         ��Name$ 	lvReviews��5       $��1��6   $��0,0,0,20q�63    �� View 5      �� 5      .P�5      �� $��
Логин��6%    �� Width$	 120��6l   	 �� DisplayMemberBinding	 64   �� *IdUserNavigation.Login+6l   �� 5       $��Оценка��6%   $ 60��6]   	 65   �� *Rating+6]   �� 5!      $��Дата��6%   $	 150��6�   	 63   �� *	CreatedAt+
 �� StringFormat$
 {0:dd.MM.yyyy HH:mm}��6�   �� 5"      $��
Отзыв��6%   $	 400��6`   	 64   �� *
ReviewText+6`   5#      5%      �� 5)   
   -   5         ��Name$ addReviewPanel��5)      $��2��6   $y�	Collapsed=�66   ._�5*      �� .+�5+      �� $+��%Оставьте свой отзыв:��6   $
��Bold"�68   $��0,10,0,5q�6J   �� 5-       �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�5.      �� $��Оценка:��6    $��Center=�6/   $��0,0,10,0q�6J   	� 5/      -   5         	�Name$	 rb1��5/   "   $��1��6/    	� 	GroupName$ Rating��6;   $��0,0,5,0q�6N   	� 50      -   5        $	 rb2��50   "   $��2��6/   $ Rating��6;   $��0,0,5,0q�6N   	� 51      -   5        $	 rb3��51   "   $��3��6/   $ Rating��6;   $��0,0,5,0q�6N   	� 52      -   5        $	 rb4��52   "   $��4��6/   $ Rating��6;   $��0,0,5,0q�6N   	� 53      -   5        $	 rb5��53   "   $��5��6/   $ Rating��6;    d� 	IsChecked$
 TrueY�6N   54      �� 56      -   5         ��Name$ 	txtReview��56       �� AcceptsReturn$
 True��6-    �� TextWrapping$
 Wrap=�57      " �� VerticalScrollBarVisibility$
 Auto=�6.   $	��100��58      $��0,0,0,10q�6'   �� 5:      $ 
Horizontal=�6   $��Right=�66   .+�5;      �� -	   5         ��Name$ 	btnSubmit��5;      $#��Отправить отзыв��60   $	��120��6J   $��0,0,10,0q�6V   6h   �� 5<      -
   5        $ 	btnCancel��5<      $��Закрыть��60   $	��100��6B   6N   5=      5>      5?      �� 5B   
   -   5        $ loginRequiredPanel��5B      $��2��6   $y�Visible=�6:   ._�5C      �� .+�5D      �� $^��XЧтобы оставить отзыв, пожалуйста, авторизуйтесь��6   $
��0,10q�5E      $��Center=�6*   �� 5F      -   5        $��
Войти��5F      $	��100��6)   $��Center=�65   5G      5H      5I      5J      5K      