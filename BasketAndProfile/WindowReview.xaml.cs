using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace EkatBooks.BasketAndProfile
{
    public partial class WindowReview : Window
    {
        private readonly BooksContext _context;
        private readonly int _bookId;
        private readonly int? _userId;

        public WindowReview(int bookId, int? userId = null)
        {
            InitializeComponent();
            _context = new BooksContext();
            _bookId = bookId;
            _userId = userId;

            // Настраиваем видимость панелей
            if (_userId.HasValue)
            {
                addReviewPanel.Visibility = Visibility.Visible;
                loginRequiredPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                addReviewPanel.Visibility = Visibility.Collapsed;
                loginRequiredPanel.Visibility = Visibility.Visible;
            }

            LoadReviews();
        }

        private void LoadReviews()
        {
            try
            {
                var reviews = _context.Reviews
                    .Where(r => r.IdBook == _bookId)
                    .Include(r => r.IdUserNavigation)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();

                lvReviews.ItemsSource = reviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отзывов: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReview.Text))
                {
                    MessageBox.Show("Пожалуйста, введите текст отзыва",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int rating = 5;
                if (rb1.IsChecked == true) rating = 1;
                else if (rb2.IsChecked == true) rating = 2;
                else if (rb3.IsChecked == true) rating = 3;
                else if (rb4.IsChecked == true) rating = 4;

                var review = new Review
                {
                    IdReview = GetNextReviewId(),
                    IdBook = _bookId,
                    IdUser = _userId.Value,
                    Rating = rating,
                    ReviewText = txtReview.Text,
                    CreatedAt = DateTime.Now
                };

                _context.Reviews.Add(review);
                _context.SaveChanges();

                LoadReviews();
                txtReview.Clear();
                rb5.IsChecked = true;

                MessageBox.Show("Спасибо за ваш отзыв!", "Успешно",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении отзыва: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь должна быть логика открытия окна авторизации
            MessageBox.Show("Пожалуйста, авторизуйтесь в системе",
                          "Требуется авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private int GetNextReviewId()
        {
            return _context.Reviews.Any() ? _context.Reviews.Max(r => r.IdReview) + 1 : 1;
        }
    }
}