   M S B A M L   `   `   `  ����D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:EkatBooks.MainMenuPagesWindowsEkatBooks.MainMenuPagesWindows  5      ;"clr-namespace:EkatBooks.ConvertersEkatBooks.Converters  5          EkatBooks.RightsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:EkatBooks.MainMenuPagesWindows     5      3conv"clr-namespace:EkatBooks.Converters     5        N� Title$  
RightsPage��5       � 	Resources 5      3   -EkatBooks.Converters.ImageAuthorPathConverter   ImageAuthorPathConverter��%   &	          5   
   6(   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5!   
   �� 
����iA�$��27(�6'   $
��Bold"�65   
�������$��0��6_   .��6l   Авторы5#      �� 5$   
   $��1��6   
���   �$��2��5&      $��	0,0,30,12q�5'      �� 5*   
   $��20(�6   $	��800��6"   $��5,0,0,10q�61   $
��Wrap=�6C   $
��Left=�6W   $��2��6r   ��Авторы — это настоящие мастера своего дела, чьи идеи и труды становятся источником вдохновения для множества читателей. Каждый автор, представленный в нашем каталоге, делится уникальными знаниями, опытом и философией, которые помогают изменить подход к жизни, бизнесу и творчеству. От известных экспертов до молодых и перспективных талантов — они все вносят свой вклад в развитие культуры и науки, создавая книги, которые могут изменить ваше восприятие мира. Погрузитесь в мир авторских мыслей и откройте для себя тех, кто стоит за словами, которые формируют наше будущее.5.   L   �� 5M   
   -   5         ��Name$ BooksListView��5M      $��10,10,10,10q�6*   $	��Top=�6?   $��0q�6w   $��4��6�   ��6W   �� *	Authors+��5N      �� 5O      .R�5P      � $��5q�6   $	��200��6&    � ColumnDefinitions 5Q      �� 5R      $
��Auto �6/   �� 5S      $	��500��6/   �� 5T      $
��Auto �6/   .+�5X      �� $	��200��6n   $	��200��6z   $��0,0,20,0q�6�   $��0��6�   ��6    �� *Photo+ �� 	Converter# [  6�   �� 5[      $��
10 0 10 10q�6%   $��1��69    J� IsHitTestVisible$ False��6I   .+�5\      �� $��20(�6>   
�������$��0,5,0,5q�6^   ��6(   �� *Name+6^   �� 5]      $	��650��6=   $��20(�6L   $��0,20,0,0q�6Z   $
��Wrap=�6l   ��6(   �� *Bio+6l   �� 5^      $
��Wrap=�6^   $��20(�6r   $��0,20,0,0q�6�   ��6(   �� *	BirthDate+ �� StringFormat$ 
dd.MM.yyyy��6�   5_      5`      5a      5c      5d      5e      