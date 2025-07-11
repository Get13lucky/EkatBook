using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EkatBooks.BasketAndProfile
{
    public partial class WindowOrder : Window
    {
        private readonly BooksContext _context;
        private readonly Userwpf _currentUser;
        private readonly Cart _userCart;

        public WindowOrder(Userwpf currentUser)
        {
            InitializeComponent();
            _context = new BooksContext();
            _currentUser = currentUser;

            // Получаем корзину пользователя
            _userCart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.IdBookNavigation)
                .FirstOrDefault(c => c.IdUser == _currentUser.IdUser);

            // Заполняем данные пользователя по умолчанию
            NameTextBox.Text = _currentUser.Name;
            PhoneTextBox.Text = _currentUser.NumberPhone;
        }

        private void Button_ClickDoOrder(object sender, RoutedEventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Заполните имя");
                return;
            }
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Заполните номер телефона");
                return;
            }
            if (string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                MessageBox.Show("Заполните адрес доставки");
                return;
            }
            if (PaymentMethodComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите способ оплаты");
                return;
            }
            if (DeliveryMethodComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите способ доставки");
                return;
            }

            // Создаем окно подтверждения
            var confirmWindow = new Window
            {
                Title = "Подтверждение заказа",
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this,
                ResizeMode = ResizeMode.NoResize
            };

            var stackPanel = new StackPanel { Margin = new Thickness(20) };

            // Добавляем информацию для подтверждения
            stackPanel.Children.Add(new TextBlock
            {
                Text = "Пожалуйста, проверьте данные заказа:",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10)
            });

            stackPanel.Children.Add(new TextBlock { Text = $"Имя: {NameTextBox.Text}" });
            stackPanel.Children.Add(new TextBlock { Text = $"Телефон: {PhoneTextBox.Text}" });
            stackPanel.Children.Add(new TextBlock { Text = $"Адрес: {AddressTextBox.Text}" });
            stackPanel.Children.Add(new TextBlock
            {
                Text = $"Способ оплаты: {(PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content}"
            });
            stackPanel.Children.Add(new TextBlock
            {
                Text = $"Способ доставки: {(DeliveryMethodComboBox.SelectedItem as ComboBoxItem)?.Content}"
            });

            // Добавляем кнопки
            // Добавляем кнопки
            var buttonPanel = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 10, 0, 0)
            };
            // Создаем две колонки - одна занимает все доступное пространство, вторая - по содержимому
            buttonPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            buttonPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            var cancelButton = new Button
            {
                Content = "Вернуться к редактированию",
                Padding = new Thickness(15, 5, 15, 5),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Grid.SetColumn(cancelButton, 0);

            var confirmButton = new Button
            {
                Content = "Подтвердить заказ",
                Background = System.Windows.Media.Brushes.RoyalBlue,
                Foreground = System.Windows.Media.Brushes.White,
                Padding = new Thickness(15, 5, 15, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(10, 0, 0, 0) // Добавляем отступ слева
            };
            Grid.SetColumn(confirmButton, 1);

            // Обработчики событий
            cancelButton.Click += (s, args) =>
            {
                confirmWindow.DialogResult = false;
                confirmWindow.Close();
            };

            confirmButton.Click += async (s, args) =>
            {
                confirmWindow.DialogResult = true;
                confirmWindow.Close();
                await ProcessOrderAsync();
            };

            // Добавляем кнопки в панель
            buttonPanel.Children.Add(cancelButton);
            buttonPanel.Children.Add(confirmButton);

            stackPanel.Children.Add(buttonPanel);

            confirmWindow.Content = stackPanel;
            confirmWindow.ShowDialog();
        }

        private async Task ProcessOrderAsync()
        {
            try
            {
                // Проверка корзины
                if (_userCart == null || _userCart.CartItems == null || !_userCart.CartItems.Any())
                {
                    MessageBox.Show("Ваша корзина пуста");
                    return;
                }

                // Создаем новый контекст для операции
                using var context = new BooksContext();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    // 1. Обновляем пользователя
                    var user = await context.Userwpfs.FindAsync(_currentUser.IdUser);
                    if (user != null)
                    {
                        user.Name = NameTextBox.Text;
                        user.NumberPhone = PhoneTextBox.Text;
                    }

                    // 2. Создаем платеж (без указания ID)
                    var payment = new Payment
                    {
                        PaymentDate = DateTime.Now,
                        ChoicePayment = (PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };
                    context.Payments.Add(payment);
                    await context.SaveChangesAsync(); // Здесь будет сгенерирован новый ID

                    // 3. Создаем заказ
                    var order = new Orderbook
                    {
                        DeliveryAddress = AddressTextBox.Text,
                        DeliveryMethod = (DeliveryMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        IdPayment = payment.IdPayment,
                        IdStatus = 1,
                        IdUser = _currentUser.IdUser,
                        OrderDate = DateOnly.FromDateTime(DateTime.Now),
                        TotalPrice = _userCart.CartItems.Sum(ci => ci.IdBookNavigation.Price * ci.QuantityGoods)
                    };
                    context.Orderbooks.Add(order);
                    await context.SaveChangesAsync();

                    // 4. Добавляем элементы заказа и обновляем количество книг
                    foreach (var cartItem in _userCart.CartItems)
                    {
                        var book = await context.Books.FindAsync(cartItem.IdBook);

                        if (book == null)
                        {
                            throw new Exception($"Книга с ID {cartItem.IdBook} не найдена");
                        }

                        if (book.QuantityBooks < cartItem.QuantityGoods)
                        {
                            throw new Exception($"Недостаточно товара '{book.Title}' на складе. Доступно: {book.QuantityBooks}");
                        }

                        book.QuantityBooks -= cartItem.QuantityGoods;

                        // Создаем элемент заказа (без указания ID)
                        var orderItem = new OrderItem
                        {
                            IdBook = book.IdBook,
                            IdOrder = order.IdOrder,
                            QuantityGoodsUnique = cartItem.QuantityGoods,
                            TotalPrice = book.Price * cartItem.QuantityGoods
                        };
                        context.OrderItems.Add(orderItem);
                    }

                    // 5. Очищаем корзину
                    var cart = await context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefaultAsync(c => c.IdCart == _userCart.IdCart);

                    if (cart != null)
                    {
                        context.CartItems.RemoveRange(cart.CartItems);
                    }

                    // Фиксируем транзакцию
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Обновляем данные в основном контексте
                    _context.Entry(_currentUser).Reload();
                    if (_userCart != null)
                    {
                        _context.Entry(_userCart).Reload();
                        _context.Entry(_userCart).Collection(c => c.CartItems).Load();
                    }

                    MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_ClickReturnBasket(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _context?.Dispose();
        }
    }
}