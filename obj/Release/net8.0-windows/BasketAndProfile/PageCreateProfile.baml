   M S B A M L   `   `   `  ���� D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EkatBooks	EkatBooks  !    EkatBooks.PageCreateProfile   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:EkatBooks       N� Title$  PageCreateProfile�� N� 
Background
 �����.��� .+��� $��Center=�$	��200��.+��� $#��Создать аккаунт��$��Times New Roman)�$��25(�$��	65 10 0 0q��� -    ��Name$ inputTextBoxEmail��$	��200��$��25��$��0 20 0 0q�$��Email��
��������� -   $ inputTextBoxLogin_2��$	��200��$��25��$��0 10 0 0q�$��
Логин��
�������G� -    G�Name$ inputTextBoxPassword_2��$	��200��$��25��$��0 10 0 0q�
���   �$	��Top=� G� 	MaxLength$ 37���� -   $! inputTextBoxPasswordVisible��$	��200��$��25��$��0 10 0 0q�
���   �$	��Top=�$y�	Collapsed=� �� 	MaxLength$ 37���� -   
�������$��280 -25 1 0q�$��20��$��30������ -    ��Name$	 eye��$��30��$��30���� 
�������$	��300��$	��110��$��10q�.+��� $+��%Требования к паролю:��$��20(�$��
20,10,0,10q���  �� Orientation$ 
Horizontal=�$��20,0,0,5q�.+��� $)��#Не менее 6 символов ��$��Center=�$��0,0,92,0q�$��16(��� -    ��Name$ 
Checkbox_1��$��Center=�${�False��
��������� $ 
Horizontal=�$��20,0,0,5q�.+��� $4��.Как минимум 1 спец.символ ��$��Center=�$��0,0,43,0q�$��16(��� -   $ 
Checkbox_2��$��Center=�${�False��
��������� $ 
Horizontal=�$��20,0,0,5q�.+��� $)��#Как минимум 2 цифры ��$��Center=�$��0,0,85,0q�$��16(��� -	   $ 
Checkbox_3��$��Center=�${�False��
��������� $#��Создать аккаунт��
����iA�
�������$	��200��$��25��$��0 10 0 0q��� -
   $0��*У меня уже есть аккаунт��
�������
���   �$	��0.4q�$	��200��$��25��$��0 10 0 0q�