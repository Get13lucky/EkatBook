   M S B A M L   `   `   `  ���� D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EkatBooks	EkatBooks      EkatBooks.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:EkatBooks       � Title$  EkatBook��$	��594��$	��878�� � Icon$A ;C:\Users\user\source\repos\EkatBooks\images\bookFeather.PNG�� � WindowState$ 	Maximized=�.���� -    ��Name$ PageScrollViewer��$
��Auto=�.��� -    �Name$ MainGrid�� � ColumnDefinitions �� $	��89* ��� $	��300��$
��311* � � RowDefinitions �� $	��100��$
��100* ��� $
��284* ��� $	��100 �.+��� -    ��Name$ 	MainStack��$��2��$��0��
����iA�.+��� -   
����iA�$
��Left=�$��0 -1 700 10q�
����iA�����  �� 
TargetType" ��.��� 	 �� Property	 � ��
 �� Value$
 0q��� 	 �@�Cursor$

 Hand������ $A��;C:\Users\user\source\repos\EkatBooks\images\bookFeather.PNG��$	��130��$��80�� � Cursor$
 Hand��o� -    o�Name$ MainMenu��
����iA�
�������$��130 -90 130 90q�$
��1200��$	��300��$
��Left=�$	��670��.@�m� -   $��Магазин��$��17(��� m� -   $��Издательство��$��17(��� m� -   $��Новости��$��17(��� m� -	   $��Авторы��$��17(��� �� -
    ��Name$ inputTextBox��$��135 -90 235 30q�$��26��$	��292��$
��Left=�$	��286��$��23��$��Найти книгу...��
��������� -   $��435 -90 235 30q�$��24��$��24��$��28��$��23��
�������$
��Left=�.���� $;��5C:\Users\user\source\repos\EkatBooks\images\loupe.png��$��24��$��30��$��28��$��23��$��-1.4 -2 -2 -2q�$
��Left=��� $=��7Время работы: Пн-Пт: с 10:00 до 18:00��
�������$��10,-75,220,40q�$��40��$	��250��$��20(�$��Right=��� $(��"                 8 (912) 500 33 77��
�������$��10,-80,10,40q�$��40��$	��250��$��20(�$��Right=��� -   $��50��$��50��$��-40 -200 360 15q�$��Right=�!  System.Windows.SystemColors    ActiveCaptionTextBrushKey J� OpacityMask# �  $
 Hand��$|�False�� J� RenderTransformOrigin$	 1,1&���Z�  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  ���� $<��6C:\Users\user\source\repos\EkatBooks\images\basket.png��$��Right=�$
 Hand�� � ToolTip _� $)��#Ваша корзина пуста!��
�������
���   �$��15(�$��50��$	��100���� -   
����iA�
����iA�$��Right=�$��0 -220 50 0q�$��50��$��50��   ���� $9��3C:\Users\user\source\repos\EkatBooks\images\per.png��$��Right=�$
 Hand���� -   $ MainStack_2��$��1��.+��� -    ��Name$ MainTabControl��$��Stretch=�$��0q�$��3q�$��16(�
�������.@��� $��
Книги��$��0q�$��Stretch=�$	��133��.���� -   $	 Mas��.+��� -   $��0q�
�������$��0 0 0 5q�.����  �� Orientation$ 
Horizontal=�.+��� $;��5C:\Users\user\source\repos\EkatBooks\images\money.png��$��20��$��20��$��5q��� $(��" Бизнес-литература��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�.+��� $?��9C:\Users\user\source\repos\EkatBooks\images\computer4.png��$��20��$��18��$��	-10 0 0 0q��� $6��0   Компьютерная литература��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $:��4C:\Users\user\source\repos\EkatBooks\images\atom.png��$��20��$��18��$��
-10 0  0 0q��� $-��'   Наука и образование��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $<��6C:\Users\user\source\repos\EkatBooks\images\museum.png��$��20��$��18��$��
-10 0  0 0q��� $3��-   Публицистика и история��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $>��8C:\Users\user\source\repos\EkatBooks\images\medicine.png��$��20��$��18��$��
-10 0  0 0q��� $4��.   Медицинская литература��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $<��6C:\Users\user\source\repos\EkatBooks\images\psycho.png��$��20��$��18��$��
-10 0  0 0q��� $<��6   Психологическая литература��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $9��3C:\Users\user\source\repos\EkatBooks\images\law.png��$��20��$��18��$��
-10 0  0 0q��� $4��.   Юридическая литература��$	��226��$��20���� -   $��0q�
�������$��0 0 0 5q�.���� $ 
Horizontal=�$��Right=�$��0 5 1 1q�.+��� $=��7C:\Users\user\source\repos\EkatBooks\images\history.png��$��20��$��18��$��
-10 0  0 0q��� $:��4   Художественная литература��$	��226��$��20���� $��Тренды��$��0q�$��Stretch=�$	��133��
�������.��� -    �Name$ MainWrapPanel��$��5 2 0 0q�.+��� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $<��6C:\Users\user\source\repos\Ekaterinburg\Images\new.png��$��20��$��20��$��0 0 7 0q��� $�� Новинки��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $C��=C:\Users\user\source\repos\Ekaterinburg\Images\bestseller.png��$��20��$��20��$��0 0 7 0q��� $�� Бестселлеры��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $=��7C:\Users\user\source\repos\Ekaterinburg\Images\soon.png��$��20��$��20��$��0 0 7 0q��� $�� Скоро��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $=��7C:\Users\user\source\repos\Ekaterinburg\Images\like.png��$��20��$��20��$��0 0 7 0q��� $�� Рекомендуем��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $?��9C:\Users\user\source\repos\Ekaterinburg\Images\camera.png��$��20��$��20��$��0 0 7 0q��� $�� Экранизация��$��Center=�$��5q�$	��228��$��20���� -   $��0q�
�������.���� $ 
Horizontal=�.+��� $<��6C:\Users\user\source\repos\Ekaterinburg\Images\cup.png��$��20��$��20��$��0 0 7 0q��� $��
 Топ 10��$��Center=�$��5q�$	��228��$��20���� -    $��0q�
�������.���� $ 
Horizontal=�.+��� $?��9C:\Users\user\source\repos\Ekaterinburg\Images\random.png��$��20��$��20��$��0 0 7 0q��� $#�� Cлучайная книга��$��Center=�$��5q�$	��228��$��20���� -!   $��0q�
�������.���� $ 
Horizontal=�.+��� $>��8C:\Users\user\source\repos\Ekaterinburg\Images\medal.png��$��20��$��20��$��0 0 7 0q��� $*��$ Нобелевская премия��$��Center=�$��5q�$	��228��$��20��]� $��0��$��2��
����iA�$��2��.+��� .+��� $ 
Horizontal=�$��	20 10 0 0q�.+��� $3��-г. Екатеринбург, Крауля 74��
�������$��17(��� $��EkatBook��
�������$��Center=�$��30(�$��Modern No. 20)�$��
360 -6 0 0q��� $ 
Horizontal=�$��
60 -10 0 0q�.+��� -"   
����iA�$��50��$��50������ $>��8C:\Users\user\source\repos\EkatBooks\images\telegram.png��$��50��$��50��$��69��$��46���� -#   
����iA�$��50��$��64������ $?��9C:\Users\user\source\repos\Ekaterinburg\Images\whats3.png��$��50��$��60��$��73��$��46��$��10 0 0 1q��� $.��(мир книг в одном клике��
�������$��25(�$��330 10 450 40q�$��Georgia)�$��42���� $��© 1980 - 2025 EkatBook��
�������$��15(�$��-900 50 140 0q�$��42��$	��301���� $��8 (912) 500 33 77��
�������$��-170 10 450 40q�$��40��$	��250��$��25(�$��Right=� � -$   $��	MainFrame��  � NavigationUIVisibility$ Hidden=�$��1��$��1��