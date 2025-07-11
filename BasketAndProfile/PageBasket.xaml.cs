using EkatBooks.BasketAndProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;

namespace EkatBooks
{
    public partial class PageBasket : Page
    {
        private readonly BooksContext _context;

        public PageBasket()
        {
            InitializeComponent();
            _context = new BooksContext();
        }

        private void StorePage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            List<CartItemViewModel> cartItems;

            if (UserSession.IsLoggedIn)
            {
                // Загружаем товары из БД для авторизованного пользователя
                cartItems = CartManager.GetCartItems(UserSession.CurrentUserId);

                // Если у пользователя нет товаров в корзине, но есть во временной корзине,
                // перемещаем товары из временной корзины
                if (cartItems.Count == 0 && TempCartManager.TempCartItems.Count > 0)
                {
                    TempCartManager.MoveToUserCart(UserSession.CurrentUserId);
                    cartItems = CartManager.GetCartItems(UserSession.CurrentUserId);
                }
            }
            else
            {
                // Используем временную корзину для неавторизованного пользователя
                cartItems = TempCartManager.TempCartItems;
            }

            // Устанавливаем источник данных для ListView
            CartItemsListView.ItemsSource = cartItems;

            // Обновляем общую стоимость
            UpdateTotalPrice(cartItems);
        }

        private void UpdateTotalPrice(List<CartItemViewModel> cartItems)
        {
            // Вычисляем общую стоимость
            decimal totalPrice = cartItems.Sum(item => item.TotalPrice);

            // Обновляем отображение общей стоимости
            TotalPriceText.Text = $"{totalPrice} ₽";
        }

        private void DecreaseQuantity(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (CartItemViewModel)button.Tag;

            if (item.Quantity > 1)
            {
                item.Quantity--;

                // Обновляем количество в БД, если пользователь авторизован
                if (UserSession.IsLoggedIn && item.CartItemId > 0)
                {
                    CartManager.UpdateCartItemQuantity(item.CartItemId, item.Quantity);
                }

                // Обновляем отображение
                RefreshCartDisplay();
            }
        }

        private void IncreaseQuantity(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (CartItemViewModel)button.Tag;

            item.Quantity++;

            // Обновляем количество в БД, если пользователь авторизован
            if (UserSession.IsLoggedIn && item.CartItemId > 0)
            {
                CartManager.UpdateCartItemQuantity(item.CartItemId, item.Quantity);
            }

            // Обновляем отображение
            RefreshCartDisplay();
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (CartItemViewModel)button.Tag;

            if (UserSession.IsLoggedIn && item.CartItemId > 0)
            {
                // Удаляем из БД, если пользователь авторизован
                CartManager.RemoveFromCart(item.CartItemId);
            }
            else
            {
                // Удаляем из временной корзины
                TempCartManager.TempCartItems.Remove(item);
            }

            // Обновляем отображение корзины без полной перезагрузки
            var cartItems = CartItemsListView.ItemsSource as List<CartItemViewModel>;
            if (cartItems != null)
            {
                cartItems.Remove(item);
            }

            // Обновляем отображение
            CartItemsListView.Items.Refresh();

            // Обновляем общую стоимость
            UpdateTotalPrice(cartItems);
        }

        private void RefreshCartDisplay()
        {
            // Перезагружаем товары
            LoadCartItems();
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Для оформления заказа необходимо войти в систему.");
                return;
            }

            // Получаем текущего пользователя из базы данных
            var currentUser = _context.Userwpfs
                .Include(u => u.Carts)
                .FirstOrDefault(u => u.IdUser == UserSession.CurrentUserId);

            if (currentUser == null)
            {
                MessageBox.Show("Ошибка: пользователь не найден.");
                return;
            }

            // Проверяем, есть ли товары в корзине
            var cartItems = _context.Carts
                .Where(c => c.IdUser == currentUser.IdUser)
                .SelectMany(c => c.CartItems)
                .Include(ci => ci.IdBookNavigation)
                .ToList();

            if (cartItems.Count == 0)
            {
                MessageBox.Show("Ваша корзина пуста.");
                return;
            }

            // Открываем окно оформления заказа с передачей текущего пользователя
            WindowOrder windowOrder = new WindowOrder(currentUser);
            windowOrder.ShowDialog();

            // Обновляем корзину после закрытия окна заказа
            LoadCartItems();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Loaded += StorePage_Loaded;
        }
    }
}