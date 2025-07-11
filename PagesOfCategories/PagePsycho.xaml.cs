using EkatBooks.ViewModel;
using EkatBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EkatBooks.BasketAndProfile;

namespace Ekaterinburg.PagesOfCategories
{
    /// <summary>
    /// Логика взаимодействия для PagePsycho.xaml
    /// </summary>
    public partial class PagePsycho : Page
    {
        public PagePsycho()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        // Метод для начальной загрузки данных
        private async Task LoadDataAsync()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsync();
        }

        // Обработчик для перехода на страницу корзины
        private void PageBasket(object sender, RoutedEventArgs e)
        {
            // Получаем книгу, на которую нажали
            Button button = (Button)sender;
            var book = (Book)button.DataContext;

            if (UserSession.IsLoggedIn)
            {
                // Пользователь авторизован, добавляем в его корзину в БД
                if (CartManager.AddToCart(UserSession.CurrentUserId, book.IdBook))
                {
                    MessageBox.Show($"Книга \"{book.Title}\" добавлена в корзину!");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить книгу в корзину. Попробуйте позже.");
                }
            }
            else
            {
                // Пользователь не авторизован, добавляем во временную корзину
                if (TempCartManager.AddToTempCart(book))
                {
                    MessageBox.Show($"Книга \"{book.Title}\" добавлена в корзину!");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить книгу в корзину. Попробуйте позже.");
                }
            }

            // Опционально: Переход на страницу корзины
            // NavigationService.Navigate(new PageBasket());
        }

        private void StoreOpen(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var book = (Book)button.DataContext;

            // Открываем окно отзывов, передавая userId только если пользователь авторизован
            int? userId = UserSession.IsLoggedIn ? UserSession.CurrentUserId : null;
            WindowReview windowReview = new WindowReview(book.IdBook, userId);
            windowReview.ShowDialog();
        }

        // Обработчик для сортировки по возрастанию цены
        private async Task LoadBooksAsyncUptoPrice()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncUptoPrice();
        }

        // Обработчик для сортировки по убыванию цены
        private async Task LoadBooksAsyncDowntoPrice()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncDowntoPrice();
        }

        // Обработчик для сортировки по названию (А-Я)
        private async Task LoadBooksAsyncUptoNameBook()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncUptoNameBook();
        }

        // Обработчик для сортировки по названию (Я-А)
        private async Task LoadBooksAsyncDowntoNameBook()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncDowntoNameBook();
        }

        // Обработчик для сортировки по дате (новые книги первыми)
        private async Task LoadBooksAsyncUptoDate()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncUptoDate();
        }

        // Обработчик для сортировки по дате (старые книги первыми)
        private async Task LoadBooksAsyncDowntoDate()
        {
            var viewModel = (PagePsychoViewModel)DataContext;
            await viewModel.LoadBooksAsyncDowntoDate();
        }

        // Обработчики кнопок для сортировки по цене, названию и дате
        private void Button_ClickUptoPrice(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncUptoPrice();
        }

        private void Button_ClickDowntoPrice(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncDowntoPrice();
        }

        private void Button_ClickUptoNameBook(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncUptoNameBook();
        }

        private void Button_ClickDowntoNameBook(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncDowntoNameBook();
        }

        private void Button_ClickUptoDate(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncUptoDate();
        }

        private void Button_ClickDowntoDate(object sender, RoutedEventArgs e)
        {
            LoadBooksAsyncDowntoDate();
        }
    }
}
