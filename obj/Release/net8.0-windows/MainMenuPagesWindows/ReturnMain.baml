   M S B A M L   `   `   `  ���� D  @EkatBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EkatBooks	EkatBooks  ;"clr-namespace:EkatBooks.ConvertersEkatBooks.Converters  %clr-namespace:EkatBooks	EkatBooks      EkatBooks.ReturnMain   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:EkatBooks     3conv"clr-namespace:EkatBooks.Converters     *local6clr-namespace:EkatBooks     -     N�Name$  ReturnMainBackround�� N� Title$ 
ReturnMain�� N� 
Background
 ����� � DataContext '  !EkatBooks.PageReturnMainViewModel  -   	ViewModel�� � 	Resources -   'EkatBooks.Converters.ImagePathConverter   ImagePathConverter��%   &	          .��� .+�� .+��� 
����iA�$��27(�$
��Bold"�
�������.��
Книги�� $	��700��$��0,-15,302,360q�$
��0.15q��� $��20(�$	��800��$��	5 60 0 10q�$
��Wrap=�$
��Left=���Все книги — это книги, которые уже ждут вас, чтобы стать частью вашего мира знаний и вдохновения. Наши доступные издания охватывают самые актуальные темы в науке, искусстве, бизнесе и личностном росте, предлагая вам проверенные идеи и захватывающие истории. Погрузитесь в инновационные подходы, найдите ответы на свои вопросы и откройте новые перспективы с книгами, которые уже на полках и готовы отправиться к вам домой!�� -    ��Name$ BooksListView4��$��10 240 10 10q�$	��Top=�$��0q����� *Books+���� .R�� $��5q�$	��200�� � ColumnDefinitions �� $
��Auto ��� $	��500���� $
��Auto �.+��� $	��200��$	��200��$��0,0,20,0q�$��0������ *
CoverImage+ �� 	Converter# [  �� $��
10 0 10 10q�$��1�� J� IsHitTestVisible$ False��.+��� $��20(�
�������$��0,5,0,5q����� *IdAuthorNavigation.Name+�� $
��Bold"�$��20(�$��
0,20,-60,5q����� *Title+�� $��20(�$��	-6,20,0,0q����� *Price+	 �� StringFormat$	  {0} ₽���� $��20(�$��0,30,0,0q����� *Description+�� -   $��Купить��
����iA�
�������$	��100��$��40��$��20,0,-1500,0q�!  System.Windows.SystemColors 
   ActiveCaptionTextBrushKey J� OpacityMask# � 
  � Cursor$
 Hand��$|�False������  �� 
TargetType" ��.���  �� Property � �� �� Value$ 0q���  �@�Cursor$
 Hand�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect   � -   $��	MainFrame��  � NavigationUIVisibility$ Hidden=�$��1��$��1��