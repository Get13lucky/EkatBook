using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace EkatBooks.BasketAndProfile
{
    /// <summary>
    /// Логика взаимодействия для PageUnSuccessCreation.xaml
    /// </summary>
    /// 


    public partial class PageUnSuccessCreation : Page
    {
        private Frame MainFrame;
        private int userId;
        private List<OrderInfo> userOrders = new List<OrderInfo>();

        public PageUnSuccessCreation(Frame mainframe, int userId)
        {
            InitializeComponent();
            MainFrame = mainframe;
            this.userId = userId;
            LoadDataAsync();
            ShowButtonById();

            LoadUserOrdersAsync();
            ShowButtonCancel();
        }


        private void ShowButtonById()
        {
            if (UserSession.CurrentUserId == 2)
            {
                if (this.FindName("ButtonAdminPanel") is System.Windows.Controls.Button button)
                {
                    button.Visibility = Visibility.Visible;
                }

            }
            if (UserSession.CurrentUserId != 2)
            {
                if (this.FindName("ButtonAdminPanel") is System.Windows.Controls.Button button)
                {
                    button.Visibility = Visibility.Collapsed;
                }

            }
        }

        private async void ShowButtonCancel()
        {
            try
            {
                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

                // Проверяем, есть ли у пользователя заказы со статусом "Отменен" (6)
                string query = "SELECT COUNT(*) FROM orderbook WHERE id_user = @userId AND id_status = 6";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        int canceledOrdersCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                        // Если есть отмененные заказы - скрываем их из списка
                        if (canceledOrdersCount > 0)
                        {
                            // Фильтруем заказы, удаляя отмененные из userOrders
                            userOrders.RemoveAll(order => order.Status == "Отменен");

                            // Обновляем отображение
                            UpdateOrdersDisplay();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка при проверке статусов заказов: {ex.Message}");
                OrdersSection.Visibility = Visibility.Visible; // По умолчанию показываем секцию
            }
        }




        private async Task LoadDataAsync()
        {
            var viewModel = (PageUnSuccessCreationViewModel)DataContext;
            await viewModel.LoadUserByIdAsync(userId);
        }

        private void Button_ClickAdminPanel(object sender, RoutedEventArgs e)
        {
            // Проверяем, что пользователь действительно администратор
            if (UserSession.CurrentUserId == 2)
            {
                var adminWindow = new WindowManager();
                adminWindow.Show(); // Открываем новое окно
                Window.GetWindow(this)?.Close(); // Закрываем текущее (если нужно)
            }
            else
            {
                MessageBox.Show("Доступ запрещен. Недостаточно прав.");
            }
        }

        private async Task UpdateOrderStatusesAutomatically()
        {
            try
            {
                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Получаем все активные заказы
                    string getOrdersQuery = @"
                SELECT o.id_order, o.order_date, o.id_status, os.status 
                FROM orderbook o
                JOIN order_status os ON o.id_status = os.id_status
                WHERE o.id_status IN (1, 2, 3, 4, 5)";

                    using (var cmd = new NpgsqlCommand(getOrdersQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int orderId = reader.GetInt32(0);
                            DateTime orderDate = reader.GetDateTime(1);
                            int currentStatusId = reader.GetInt32(2);
                            string currentStatusName = reader.GetString(3);

                            TimeSpan timePassed = DateTime.Now - orderDate;
                            int daysPassed = (int)timePassed.TotalDays;

                            // Новая логика с понятными статусами
                            int newStatusId = GetNewStatusId(currentStatusId, daysPassed);

                            if (newStatusId != currentStatusId)
                            {
                                await UpdateOrderStatus(conn, orderId, newStatusId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка при автоматическом обновлении статусов: {ex.Message}");
            }
        }
        private int GetNewStatusId(int currentStatusId, int daysPassed)
        {
            // Логика перехода между статусами:
            // 1 -> 2 через 3 дня (Новый -> В обработке)
            // 2 -> 3 через 3 дня (В обработке -> Собирается)
            // 3 -> 4 через 3 дня (Собирается -> Передан в доставку)
            // 4 -> 5 через 3 дня (Передан в доставку -> Доставлен)

            int daysForNextStatus = 3;
            int statusIncrement = daysPassed / daysForNextStatus;
            int newStatusId = currentStatusId + statusIncrement;

            // Максимальный статус - 5 (Доставлен)
            return Math.Min(newStatusId, 5);
        }

        private int CalculateNewStatus(int daysPassed, int currentStatus)
        {
            // Логика изменения статуса:
            // - Каждые 3 дня повышаем статус на 1
            // - Максимальный статус - 5 ("Доставлен")

            int statusIncrement = daysPassed / 3;  // Каждые 3 дня = +1 к статусу
            int newStatus = currentStatus + statusIncrement;

            return Math.Min(newStatus, 5);  // Не превышаем статус "Доставлен"
        }

        private async Task UpdateOrderStatus(NpgsqlConnection conn, int orderId, int newStatus)
        {
            string updateQuery = "UPDATE orderbook SET id_status = @newStatus WHERE id_order = @orderId";

            using (var cmd = new NpgsqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@newStatus", newStatus);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task LoadUserOrdersAsync()
        {
            try
            {
                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";
                await UpdateOrderStatusesAutomatically();


                string query = @"
SELECT 
      o.id_order,             -- 0 (int)

    o.order_date,           -- 1 (datetime)
    o.delivery_address,     -- 2 (string)
    o.total_price,          -- 3 (decimal)
    os.status,              -- 4 (string)
    p.choice_payment,       -- 5 (string)
    o.delivery_method,      -- 6 (string) <- Проблемное поле!
    oi.id_order_item,       -- 7 (int)
    oi.quantity_goods_unique, -- 8 (int)
    b.id_book,              -- 9 (int)
    b.title,                -- 10 (string)
    b.description,          -- 11 (string)
    b.cover_image,          -- 12 (string/null)
    b.price,                -- 13 (decimal)
    a.name as author_name   -- 14 (string)
FROM orderbook o
JOIN order_status os ON o.id_status = os.id_status
JOIN payment p ON o.id_payment = p.id_payment
JOIN order_item oi ON o.id_order = oi.id_order
JOIN book b ON oi.id_book = b.id_book
JOIN author a ON b.id_author = a.id_author
WHERE o.id_user = @userId
AND o.id_status != 6
ORDER BY o.order_date DESC
LIMIT 5;";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            userOrders.Clear();
                            var ordersDict = new Dictionary<int, OrderInfo>();

                            while (await reader.ReadAsync())
                            {
                                int orderId = reader.GetInt32(0);

                                if (!ordersDict.TryGetValue(orderId, out var order))
                                {
                                    order = new OrderInfo
                                    {
                                        OrderId = orderId,
                                        OrderDate = reader.GetDateTime(1),
                                        DeliveryAddress = reader.GetString(2),
                                        TotalPrice = reader.GetDecimal(3),
                                        Status = reader.GetString(4),
                                        PaymentMethod = reader.GetString(5),
                                        DeliveryMethod = reader.IsDBNull(6) ? "Не указан" : reader.GetString(6), // Чтение способа доставки
                                        Items = new List<OrderItemInfo>()
                                    };
                                    ordersDict.Add(orderId, order);
                                    userOrders.Add(order);
                                }

                                var orderItem = new OrderItemInfo
                                {
                                    OrderItemId = reader.GetInt32(7),    // было 6, стало 7
                                    Quantity = reader.GetInt32(8),       // было 7, стало 8
                                    BookId = reader.GetInt32(9),         // было 8, стало 9
                                    Title = reader.GetString(10),        // было 9, стало 10
                                    Description = reader.GetString(11),  // было 10, стало 11
                                    CoverImage = reader.IsDBNull(12) ? null : reader.GetString(12), // было 11, стало 12
                                    Price = reader.GetDecimal(13),       // было 12, стало 13
                                    AuthorName = reader.GetString(14)    // было 13, стало 14
                                };

                                order.Items.Add(orderItem);
                            }
                        }
                    }
                }

                UpdateOrdersDisplay();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}");
            }
        }


        private void UpdateOrdersDisplay()
        {
            OrdersStackPanel.Children.Clear();

            if (userOrders.Count == 0)
            {
                OrdersStackPanel.Children.Add(new TextBlock
                {
                    Text = "У вас пока нет заказов",
                    FontStyle = FontStyles.Italic,
                    Margin = new Thickness(0, 10, 0, 0)
                });
            }
            else
            {
                foreach (var order in userOrders)
                {
                    OrdersStackPanel.Children.Add(CreateOrderCard(order));
                }
            }
        }

        private Border CreateOrderCard(OrderInfo order)
        {
            var orderCard = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 0, 0, 15),
                Padding = new Thickness(15),
                Background = Brushes.WhiteSmoke
            };

            var orderStack = new StackPanel();

            // Номер заказа и дата
            var orderHeader = new StackPanel { Orientation = Orientation.Horizontal };
            orderHeader.Children.Add(new TextBlock
            {
                Text = $"Уникальный номер заказ № {order.OrderId}",
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 0, 10, 0)
            });
            orderHeader.Children.Add(new TextBlock
            {
                Text = order.OrderDate.ToString("dd.MM.yyyy"),
                Foreground = Brushes.Gray,
                FontSize = 14
            });
            orderStack.Children.Add(orderHeader);

            // Адрес доставки
            orderStack.Children.Add(new TextBlock
            {
                Text = $"Адрес доставки: {order.DeliveryAddress}",
                Margin = new Thickness(0, 8, 0, 0),
                FontSize = 14
            });

            // Способ доставки (новый блок)
            orderStack.Children.Add(new TextBlock
            {
                Text = $"Способ доставки: {order.DeliveryMethod}",
                Margin = new Thickness(0, 8, 0, 0),
                FontSize = 14
            });

            // Способ оплаты и статус
            var paymentStatus = new StackPanel
            {
                Margin = new Thickness(0, 8, 0, 0)
            };
            paymentStatus.Children.Add(new TextBlock
            {
                Text = $"Оплата: {order.PaymentMethod}",
                Margin = new Thickness(0, 0, 15, 0),
                FontSize = 14
            });
            paymentStatus.Children.Add(new TextBlock
            {
                Text = $"Статус: {order.Status}",
                Foreground = order.Status == "Доставлен" ? Brushes.Green : Brushes.Blue,
                Margin = new Thickness(0, 8, 0, 0),
                FontSize = 14
            });
            orderStack.Children.Add(paymentStatus);

            // Товары в заказе
            var itemsHeader = new TextBlock
            {
                Text = "Товары в заказе:",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 15, 0, 5),
                FontSize = 14
            };
            orderStack.Children.Add(itemsHeader);

            // Добавляем товары
            foreach (var item in order.Items)
            {
                orderStack.Children.Add(CreateOrderItemCard(item));
            }

            // Сумма заказа
            orderStack.Children.Add(new TextBlock
            {
                Text = $"Итого: {order.TotalPrice} ₽",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 15, 0, 0),
                FontSize = 15,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            // Кнопка отмены заказа
            if (order.Status != "Доставлен")
            {
                var cancelButton = new Button
                {
                    Content = "Отменить заказ",
                    Tag = order.OrderId,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = Brushes.RoyalBlue,
                    Foreground = Brushes.White,
                    MinWidth = 100
                };
                cancelButton.Click += CancelOrder_Click;
                orderStack.Children.Add(cancelButton);
            }

            orderCard.Child = orderStack;
            return orderCard;
        }

        private Border CreateOrderItemCard(OrderItemInfo item)
        {
            var itemCard = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0, 0, 0, 10),
                Padding = new Thickness(10),
                Background = Brushes.White
            };

            var itemGrid = new Grid();
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Изображение книги
            var image = new Image
            {
                Width = 80,
                Height = 100,
                Stretch = Stretch.Uniform,
                Margin = new Thickness(0, 0, 10, 0)
            };

            var binding = new Binding("CoverImage")
            {
                Converter = (IValueConverter)this.Resources["ImagePathConverter"],
                Source = item
            };
            image.SetBinding(Image.SourceProperty, binding);

            Grid.SetColumn(image, 0);
            itemGrid.Children.Add(image);

            // Информация о книге
            var bookInfoStack = new StackPanel();
            Grid.SetColumn(bookInfoStack, 1);
            itemGrid.Children.Add(bookInfoStack);

            bookInfoStack.Children.Add(new TextBlock
            {
                Text = item.Title,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap
            });

            bookInfoStack.Children.Add(new TextBlock
            {
                Text = $"Автор: {item.AuthorName}",
                FontSize = 13,
                Foreground = Brushes.Gray,
                Margin = new Thickness(0, 2, 0, 0)
            });

            bookInfoStack.Children.Add(new TextBlock
            {
                Text = $"Количество: {item.Quantity}",
                FontSize = 13,
                Margin = new Thickness(0, 5, 0, 0)
            });

            bookInfoStack.Children.Add(new TextBlock
            {
                Text = $"Цена: {item.Price} ₽",
                FontSize = 13,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 5, 0, 0)
            });

            itemCard.Child = itemGrid;
            return itemCard;
        }



        public class OrderInfo
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public string DeliveryAddress { get; set; }
            public decimal TotalPrice { get; set; }
            public string Status { get; set; }
            public string PaymentMethod { get; set; }
            public string DeliveryMethod { get; set; }  
            public List<OrderItemInfo> Items { get; set; }
        }

        public class OrderItemInfo
        {
            public int OrderItemId { get; set; }
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CoverImage { get; set; }
            public decimal Price { get; set; }
            public string AuthorName { get; set; }
            public int Quantity { get; set; }
        }


        private async void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отменить этот заказ?",
                    "Подтверждение отмены", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

                        string query = @"
                UPDATE orderbook 
                SET id_status = 6 
                WHERE id_order = @orderId 
                AND id_user = @userId
                AND id_status IN (1, 2, 3, 4, 5)";

                        using (var conn = new NpgsqlConnection(connectionString))
                        {
                            await conn.OpenAsync();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@orderId", orderId);
                                cmd.Parameters.AddWithValue("@userId", userId);

                                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Заказ успешно отменен!");

                                    // Обновляем отображение
                                    await LoadUserOrdersAsync();
                                    ShowButtonCancel(); // Проверяем статусы заказов после отмены
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось отменить заказ. Возможно, он уже был обработан или отменен.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"Ошибка при отмене заказа: {ex.Message}");
                    }
                }
            }
        }








        public bool UpdateUserInfo(string oldLogin, string login, string email, string name, string number)
        {
            try
            {
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Логин не должен быть пустым.");
                    return false;
                }

                if (login.Length < 6)
                {
                    MessageBox.Show("Логин должен быть минимум 6 символов.");
                    return false;
                }

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email не должен быть пустым.");
                    return false;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Email должен соответствовать формату!");
                    return false;
                }

                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

                string query = @"
                UPDATE public.userwpf
                SET 
                    login = @login,
                    email = @email,
                    name = @name,
                    number_phone = @number
                WHERE login = @oldLogin;
                ";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@name", name ?? string.Empty);
                        cmd.Parameters.AddWithValue("@number", number ?? string.Empty);
                        cmd.Parameters.AddWithValue("@oldLogin", oldLogin);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Профиль успешно обновлен!");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить профиль");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка при обновлении профиля: {ex.Message}");
                return false;
            }
        }



        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope((DependencyObject)sender), null);

            var user = (dynamic)((Button)sender).DataContext;

            string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

            string oldLogin = GetLoginFromDatabase(userId, connectionString);
            string login = user.Login;
            string email = user.Email;
            string name = user.Name;
            string number = user.NumberPhone;

            UpdateUserInfo(oldLogin, login, email, name, number);
        }
        public static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        public static string GetLoginFromDatabase(int userId, string connectionString)
        {
            string query = "SELECT login FROM public.userwpf WHERE id_user = @UserId";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private void Button_ClickLogOut(object sender, RoutedEventArgs e)
        {
            // Если в корзине пользователя есть товары, сохраняем их во временной корзине
            if (UserSession.IsLoggedIn)
            {
                var userCartItems = CartManager.GetCartItems(UserSession.CurrentUserId);
                if (userCartItems.Count > 0)
                {
                    // Очищаем временную корзину перед добавлением
                    TempCartManager.TempCartItems.Clear();

                    // Добавляем товары пользователя во временную корзину
                    foreach (var item in userCartItems)
                    {
                        TempCartManager.TempCartItems.Add(item);
                    }
                }
            }

            // Очищаем сессию пользователя
            UserSession.ClearSession();

            // Получаем родительский Frame
            var parentFrame = this.WindowProfileFrame as Frame;
            if (parentFrame == null)
            {
                MessageBox.Show("Не удалось получить родительский Frame.");
                return;
            }

            // Навигация на страницу с передачей Frame
            NavigationService.Navigate(new PageProfile_1(parentFrame));

        }



    }
}