   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EkatBooks	EkatBooks  5      !    EkatBooks.PageCreateProfile   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:EkatBooks     5        N� Title$  PageCreateProfile��5	       N� 
Background
 �����.��5      � .+�5   
   �� $��Center=�6   $	��200��63   .+�5      �� $#��Создать аккаунт��6   $��Times New Roman)�6/   $��25(�6L   $��	65 10 0 0q�6[   �� 5      -   5         ��Name$ inputTextBoxEmail��5      $	��200��61   $��25��6A   $��0 20 0 0q�6P   $��Email��6b   
�������6�   �� 5      -   5        $ inputTextBoxLogin_2��5      $	��200��63   $��25��6C   $��0 10 0 0q�6R   $��
Логин��6d   
�������6�   �� 5      -   5        $ inputTextBoxPassword_2��5      $	��200��66   $��25��6E   $��0 10 0 0q�6T   $��Пароль��6f   
�������$	��Top=�6�    �� 	MaxLength$ 37��6�   6�   �� 5      
�������$��280 -25 1 0q�6(   $��20��6=   $��30��6L   ��5      �� 5      -   5         ��Name$	 eye��5      $��30��6)   $��30��68   5      5      �� 5       
�������$	��300��62   $	��110��6A   $��10q�6Q   .+�5"      �� $+��%Требования к паролю:��6   $��20(�68   $��
20,10,0,10q�6F   �� 5'       �� Orientation$ 
Horizontal=�6   $��20,0,0,5q�66   .+�5(      �� $)��#Не менее 6 символов ��6    $��Center=�6<   $��0,0,92,0q�6W   $��16(�6i   �� 5)      -   5         ��Name$ 
Checkbox_1��5)      $��Center=�63   ${�False��6N   
�������6`   5*      �� 5-      $ 
Horizontal=�6   $��20,0,0,5q�66   .+�5.      �� $4��.Как минимум 1 спец.символ ��6    $��Center=�6B   $��0,0,43,0q�6]   $��16(�6o   �� 5/      -   5        $ 
Checkbox_2��5/      $��Center=�63   ${�False��6N   
�������6`   50      �� 53      $ 
Horizontal=�6   $��20,0,0,5q�66   .+�54      �� $)��#Как минимум 2 цифры ��6    $��Center=�6<   $��0,0,85,0q�6W   $��16(�6i   �� 55      -   5        $ 
Checkbox_3��55      $��Center=�63   ${�False��6N   
�������6`   56      57      �� 59      -   5        $#��Создать аккаунт��59      
����iA�
�������$	��200��6|   $��25��6�   $��0 10 0 0q�6�   �� 5:      -	   5        $0��*У меня уже есть аккаунт��5:      
�������
���   �$	��0.4q�6]   $	��200��6t   $��25��6�   $��0 10 0 0q�6�   6�   5>      5?      5@      