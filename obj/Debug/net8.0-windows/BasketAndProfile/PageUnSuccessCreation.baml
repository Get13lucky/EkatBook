   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:EkatBooks.ConvertersEkatBooks.Converters  5      %clr-namespace:EkatBooks	EkatBooks  5      6    0EkatBooks.BasketAndProfile.PageUnSuccessCreation   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3conv"clr-namespace:EkatBooks.Converters     5      )localclr-namespace:EkatBooks     5        N� Title$  UserProfile��5       N� 
Background
 ����� � 	Resources 5      -   'EkatBooks.Converters.ImagePathConverter   ImagePathConverter4   .EkatBooks.Converters.RoleToVisibilityConverter  RoleToVisibilityConverter7   1EkatBooks.Converters.BooleanToVisibilityConverter   BooleanToVisibilityConverter��%W   &	        &	      &	 &       5   
   6"     5   
   6)     5   
   6,   5       � DataContext 5      .   (EkatBooks.PageUnSuccessCreationViewModel  5   
   -   5        	ViewModel��5   /   .��5      � .+�5   
   �� .��5      �� -   5         ��Name$ MainStackPanel��5      $��Center=�61   $	��600��6N   .+�5      �� $��Профиль��6   $��Times New Roman)�6+   $��22(�6H   $��Center=�6V   $��0 10 0 0q�6s   �� 5      -   5         ��Name$ ButtonAdminPanel��5!      $/��)Панель администратора��5      
����iA�
�������$	��180��5$      $��25��5%      $��0,12,0,5q�5'      $y�	Collapsed=�5(       � Cursor$
 Hand��5)      $|�False��5*      $��Center=�5+      �� 51      -   5        $&�� Выход из аккаунта��51      
����iA�
�������$	��110��55      $��20��56      $��	0,12,0,20q�57      !  System.Windows.SystemColors    ActiveCaptionTextBrushKey J� OpacityMask# �  58      $
 Hand��59      $|�False��5:      $��Center=�5;      �� 5?      
�������$��0,0,0,1q�61   	 �� Padding$	 0,0,0,20q�6K   $��0,0,0,20q�6^   ._�5@      �� $��Личные данные��6    $��18(�65   $��0,0,0,10q�6D   5A      �� 5C      -   5        
 ��Name$
 UserDataListView��5C      $��0,0,0,20q�65   $��0q�6g   ��6G   �� *	userWpf+��5D      �� 5E      .R�5F      �� $��10q�6)   .+�5G   "   �� $��Логин:��6,   $��16(�6:   
�������6H   �� 5H   "   $��16(�6A   $��0,5,0,10q�6O   ��6*   �� *Login+6O   �� 5J   "   $��Email:��6,   $��16(�6:   
�������6H   �� 5K   "   $��16(�6A   $��0,5,0,10q�6O   ��6*   �� *Email+6O   �� 5M   "   $��Имя:��6,   $��16(�68   
�������6F   �� 5N   "   $��16(�6@   $��0,5,0,10q�6N   ��6*   �� *Name+6N   �� 5P   "   $��Телефон:��6,   $��16(�6<   
�������6J   �� 5Q   "   $��16(�6G   $��0,5,0,10q�6U   ��6*   �� *NumberPhone+6U   �� 5S   "   -   5        $+��%Сохранить изменения��5S   )   
����iA�
�������$	��150��5W   )   $��30��5X   )   $
��0,10q�5Y   )   $
��Left=�5Z   )   5[      5\      5^      �� 5a      -   5         ��Name$ OrdersSection��5a      
�������$��0,1,0,0q�6H   $
	 0,20q�6b   ._�5b      �� -   5        $ OrdersStackPanel��5b   !   .+�5c      �� $#��История заказов��6$   $��18(�6;   $��0,0,0,10q�6J   5e      5f      5g      5h       � 5i   
   -	   5          �Name$ WindowProfileFrame��5i        � NavigationUIVisibility$ Hidden=�6,   5j      5k      