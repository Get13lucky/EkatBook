using Npgsql;
using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using System.Data.Entity;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace EkatBooks.BasketAndProfile
{
    /// <summary>
    /// Логика взаимодействия для WindowManager.xaml
    /// </summary>
    public partial class WindowManager : Window
    {
        private string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";
        private BooksContext _context = new BooksContext();
        private Orderbook _selectedOrder;
        public int SelectedCategoryId { get; set; }
        public int SelectedTrendId { get; set; }




        public WindowManager()
        {
            InitializeComponent();
            LoadAuthors();
            LoadBooks();
            LoadUsers();
            LoadCategories(); // Добавьте эту строку
            LoadTrends(); // Добавляем загрузку трендов



        }

        private void LoadTrends()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_trend, name FROM trend ORDER BY name";

                    var trends = new List<Trend>();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trends.Add(new Trend
                            {
                                id_trend = reader.GetInt32(0),
                                name = reader.GetString(1)
                            });
                        }
                    }

                    BookTrendComboBox.ItemsSource = trends;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке трендов: {ex.Message}");
            }
        }

        public class Trend
        {
            public int id_trend { get; set; }
            public string name { get; set; }
        }

        public class Category
        {
            public int id_category { get; set; }
            public string name { get; set; }
        }
        private void LoadCategories()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_category, name FROM category ORDER BY name";

                    var categories = new List<Category>();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                id_category = reader.GetInt32(0),
                                name = reader.GetString(1)
                            });
                        }
                    }

                    BookCategoryComboBox.ItemsSource = categories;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}");
            }
        }

        // Обработчик кнопки обновления списка категорий
        private void RefreshCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }

        public class PaymentInfo
        {
            public int IdPayment { get; set; }
            public string PaymentMethod { get; set; }  // Было ChoicePayment в базе
            public DateTime? PaymentDate { get; set; }
            public int? IdOrder { get; set; }
            public string OrderStatus { get; set; }
            public decimal? OrderTotalPrice { get; set; }
        }

        private void PaymentsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPayments();
        }

        private void RefreshPaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPayments();
        }

        private void LoadPayments()
        {
            try
            {
                using (var context = new BooksContext())
                {
                    // Загружаем платежи с связанными данными о заказах
                    var payments = context.Payments
                        .Select(p => new PaymentInfo
                        {
                            IdPayment = p.IdPayment,
                            PaymentMethod = p.ChoicePayment, // Используем существующее поле ChoicePayment
                            PaymentDate = p.PaymentDate,
                            // Для Amount и Status используем данные из связанного заказа
                            IdOrder = context.Orderbooks
                                .Where(o => o.IdPayment == p.IdPayment)
                                .Select(o => o.IdOrder)
                                .FirstOrDefault(),
                            OrderStatus = context.Orderbooks
                                .Where(o => o.IdPayment == p.IdPayment)
                                .Join(context.OrderStatuses,
                                    o => o.IdStatus,
                                    s => s.IdStatus,
                                    (o, s) => s.Status)
                                .FirstOrDefault(),
                            OrderTotalPrice = context.Orderbooks
                                .Where(o => o.IdPayment == p.IdPayment)
                                .Select(o => o.TotalPrice)
                                .FirstOrDefault()
                        })
                        .OrderByDescending(p => p.PaymentDate)
                        .ToList();

                    PaymentsDataGrid.ItemsSource = payments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке платежей: {ex.Message}");
            }
        }




        private void OrdersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }
        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedOrder = OrdersDataGrid.SelectedItem as Orderbook;

            if (_selectedOrder != null)
            {
                LoadStatuses((int)_selectedOrder.IdStatus);
            }
        }
        private void LoadStatuses(int currentStatusId)
        {
            try
            {
                using (var context = new BooksContext())
                {
                    var statuses = context.OrderStatuses.ToList();
                    StatusComboBox.ItemsSource = statuses;
                    StatusComboBox.DisplayMemberPath = "Status";
                    StatusComboBox.SelectedValuePath = "IdStatus";

                    // Устанавливаем текущий статус заказа
                    StatusComboBox.SelectedValue = currentStatusId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке статусов: {ex.Message}");
            }
        }

        
        private void LoadOrders()
        {
            try
            {
                using (var context = new BooksContext())
                {
                    // Загружаем заказы с связанными данными, включая данные пользователя
                    var orders = context.Orderbooks
                        .Include(o => o.IdStatusNavigation)
                        .Include(o => o.IdUserNavigation) // Это загрузит связанного пользователя
                        .Include(o => o.IdPaymentNavigation)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();

                    OrdersDataGrid.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}");
            }
        }




        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ для изменения статуса");
                return;
            }

            if (StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите новый статус");
                return;
            }

            try
            {
                using (var context = new BooksContext())
                {
                    var order = context.Orderbooks.Find(_selectedOrder.IdOrder);
                    if (order != null)
                    {
                        order.IdStatus = (int)StatusComboBox.SelectedValue;
                        context.SaveChanges();

                        MessageBox.Show("Статус заказа успешно обновлен");
                        LoadOrders(); // Обновляем список заказов
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }




        // Добавление нового автора
        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из полей ввода
                string authorName = AuthorNameTextBox.Text;
                string authorBio = AuthorBioTextBox.Text;
                string authorPhoto = AuthorPhotoTextBox.Text;
                int authorId = Convert.ToInt32(AuthorIdTextBox.Text);
                DateTime authorBirthDate = AuthorBirthDatePicker.SelectedDate ?? DateTime.Now;

                // Валидация обязательных полей
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    MessageBox.Show("Имя автора не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(authorPhoto))
                {
                    MessageBox.Show("Фотография автора не может быть пустой!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(authorBio))
                {
                    MessageBox.Show("Биография автора не может быть пустой!");
                    return;
                }
                

                // Валидация URL фотографии


                // Подключение к базе данных и выполнение запроса
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO author (id_author, name, bio, photo, birth_date) VALUES (@id_author, @name, @bio, @photo, @birth_date)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id_author", authorId);
                        cmd.Parameters.AddWithValue("name", authorName);
                        cmd.Parameters.AddWithValue("bio", string.IsNullOrWhiteSpace(authorBio) ? (object)DBNull.Value : authorBio);
                        cmd.Parameters.AddWithValue("photo", authorPhoto); // Фото уже проверено на пустоту и валидность
                        cmd.Parameters.AddWithValue("birth_date", authorBirthDate);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Автор успешно добавлен!");
                    LoadAuthors();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID автора (число).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении автора: {ex.Message}");
            }
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    LoadAuthors();
        //}

        private void LoadAuthors()
        {
            using (var context = new BooksContext())
            {
                AuthorsDataGrid.ItemsSource = context.Authors.OrderBy(user => user.IdAuthor).ToList();

               
            }
        }
        private void BooksDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (var context = new BooksContext())
            {
                BooksDataGrid.ItemsSource = context.Books
                    .OrderBy(b => b.IdBook)
                    .ToList();
            }
        }

        private void UsersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new BooksContext())
            {
                UsersDataGrid.ItemsSource = context.Userwpfs
                    .OrderBy(user => user.IdUser)  // Сортировка по IdUser
                    .ToList();
            }
        }


        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем значения
                int bookId = Convert.ToInt32(BookIdTextBox.Text);
                string bookTitle = BookTitleTextBox.Text;
                decimal bookPrice = Convert.ToDecimal(BookPriceTextBox.Text);
                DateTime bookPublicationDate = BookPublicationDatePicker.SelectedDate ?? DateTime.Now;
                string bookIsbn = BookIsbnTextBox.Text;
                string bookDescription = BookDescriptionTextBox.Text;
                string bookCoverImage = BookCoverImageTextBox.Text;
                int bookQuantity = Convert.ToInt32(BookQuantityTextBox.Text);
                int bookAuthorId = Convert.ToInt32(BookAuthorIdTextBox.Text);
                int bookCategoryId = (int)BookCategoryComboBox.SelectedValue;
                int bookTrendId = (int)BookTrendComboBox.SelectedValue;

                // Валидация
                if (bookId <= 0)
                {
                    MessageBox.Show("ID книги должен быть положительным числом!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(bookTitle))
                {
                    MessageBox.Show("Название книги не может быть пустым!");
                    return;
                }

                if (bookPrice <= 0)
                {
                    MessageBox.Show("Цена книги должна быть больше 0!");
                    return;
                }

                if (bookPublicationDate > DateTime.Now)
                {
                    MessageBox.Show("Дата публикации не может быть в будущем!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(bookIsbn))
                {
                    MessageBox.Show("ISBN  книги не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(bookDescription))
                {
                    MessageBox.Show("Описание книги не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(bookCoverImage))
                {
                    MessageBox.Show("URL не может быть пустым!");
                    return;
                }

                if (bookQuantity < 0)
                {
                    MessageBox.Show("Количество книг не может быть отрицательным!");
                    return;
                }

                if (bookAuthorId <= 0 || bookCategoryId <= 0 || bookTrendId <= 0)
                {
                    MessageBox.Show("Необходимо выбрать автора, категорию и тренд!");
                    return;
                }

                // Работа с БД
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Добавление книги
                            string bookQuery = @"INSERT INTO book (id_book, title, price, publication_date, isbn, 
                                        description, cover_image, quantity_books, id_author, id_category) 
                                        VALUES (@id_book, @title, @price, @publication_date, @isbn, 
                                        @description, @cover_image, @quantity_books, @id_author, @id_category)";

                            using (var cmd = new NpgsqlCommand(bookQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("id_book", bookId);
                                cmd.Parameters.AddWithValue("title", bookTitle);
                                cmd.Parameters.AddWithValue("price", bookPrice);
                                cmd.Parameters.AddWithValue("publication_date", bookPublicationDate);
                                cmd.Parameters.AddWithValue("isbn", bookIsbn);
                                cmd.Parameters.AddWithValue("description", bookDescription);
                                cmd.Parameters.AddWithValue("cover_image", bookCoverImage);
                                cmd.Parameters.AddWithValue("quantity_books", bookQuantity);
                                cmd.Parameters.AddWithValue("id_author", bookAuthorId);
                                cmd.Parameters.AddWithValue("id_category", bookCategoryId);

                                cmd.ExecuteNonQuery();
                            }

                            // Добавление тренда
                            string trendQuery = "INSERT INTO book_trend (id_book, id_trend) VALUES (@id_book, @id_trend)";
                            using (var cmd = new NpgsqlCommand(trendQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("id_book", bookId);
                                cmd.Parameters.AddWithValue("id_trend", bookTrendId);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Книга успешно добавлена!");
                            LoadBooks();
                        }
                        catch (NpgsqlException ex) when (ex.SqlState == "23505") // Ошибка уникальности
                        {
                            transaction.Rollback();
                            MessageBox.Show("Книга с таким ID или ISBN уже существует!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при добавлении книги: {ex.Message}");
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность ввода числовых значений!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}");
            }
        }


        // Добавление новой книги

        // Обработчик для выбора фото автора
        // Обработчик для выбора фото автора
        private void ChooseAuthorCoverButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект диалога выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif", // Ограничиваем выбор изображениями
                Title = "Выберите фото автора",
                InitialDirectory = @"C:\Users\user\Desktop\authors" // Указываем начальную папку
            };

            // Проверяем, был ли выбран файл
            if (openFileDialog.ShowDialog() == true)
            {
                // Устанавливаем путь к выбранному файлу в TextBox
                AuthorPhotoTextBox.Text = openFileDialog.FileName;
            }
        }

        // Обработчик для выбора обложки книги
        private void ChooseBookCoverButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект диалога выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif", // Ограничиваем выбор изображениями
                Title = "Выберите обложку книги",
                InitialDirectory = @"C:\Users\user\Desktop\books" // Указываем начальную папку
            };

            // Проверяем, был ли выбран файл
            if (openFileDialog.ShowDialog() == true)
            {
                // Устанавливаем путь к выбранному файлу в TextBox
                BookCoverImageTextBox.Text = openFileDialog.FileName;
            }
        }


        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the Book ID from the TextBox
            int bookId = Convert.ToInt32(BookIdTextBox.Text);

            // Check if the Book ID is valid
            if (bookId <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID книги для удаления.");
                return;
            }

            // Prompt the user for confirmation before deleting
            var result = MessageBox.Show("Вы уверены, что хотите удалить эту книгу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        // SQL query to delete the book by ID
                        string query = "DELETE FROM book WHERE id_book = @id_book";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            // Add the parameter for the book ID
                            cmd.Parameters.AddWithValue("id_book", bookId);

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the deletion was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Книга успешно удалена.");
                            }
                            else
                            {
                                MessageBox.Show("Книга с таким ID не найдена.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении книги: {ex.Message}");
                }
            }
        }
        private void LoadBookDataById(int bookId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Загрузка основных данных книги
                    string bookQuery = "SELECT title, price, publication_date, isbn, description, " +
                                     "cover_image, quantity_books, id_author, id_category FROM book WHERE id_book = @bookId";

                    using (var cmd = new NpgsqlCommand(bookQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("bookId", bookId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                BookTitleTextBox.Text = reader["title"].ToString();
                                BookPriceTextBox.Text = reader["price"].ToString();
                                BookPublicationDatePicker.SelectedDate = (DateTime)reader["publication_date"];
                                BookIsbnTextBox.Text = reader["isbn"].ToString();
                                BookDescriptionTextBox.Text = reader["description"].ToString();
                                BookCoverImageTextBox.Text = reader["cover_image"].ToString();
                                BookQuantityTextBox.Text = reader["quantity_books"].ToString();
                                BookAuthorIdTextBox.Text = reader["id_author"].ToString();

                                if (reader["id_category"] != DBNull.Value)
                                {
                                    BookCategoryComboBox.SelectedValue = Convert.ToInt32(reader["id_category"]);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Книга с таким ID не найдена.");
                                return;
                            }
                        }
                    }

                    // Загрузка тренда книги
                    string trendQuery = "SELECT id_trend FROM book_trend WHERE id_book = @bookId";
                    using (var cmd = new NpgsqlCommand(trendQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("bookId", bookId);
                        var trendId = cmd.ExecuteScalar();
                        if (trendId != null)
                        {
                            BookTrendComboBox.SelectedValue = Convert.ToInt32(trendId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных книги: {ex.Message}");
            }
        }

        private void EditAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из полей ввода
                int authorId = Convert.ToInt32(AuthorIdTextBox.Text);
                string authorName = AuthorNameTextBox.Text;
                string authorBio = AuthorBioTextBox.Text;
                string authorPhoto = AuthorPhotoTextBox.Text;
                DateTime? authorBirthDate = AuthorBirthDatePicker.SelectedDate;

                // Валидация
                if (authorId <= 0)
                {
                    MessageBox.Show("ID автора должен быть положительным числом!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(authorName))
                {
                    MessageBox.Show("Имя автора не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(authorBio))
                {
                    MessageBox.Show("Биография автора не может быть пустой!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(authorPhoto))
                {
                    MessageBox.Show("Фотография автора не может быть пустой!");
                    return;
                }

                if (authorBirthDate.HasValue && authorBirthDate > DateTime.Now)
                {
                    MessageBox.Show("Дата рождения не может быть в будущем!");
                    return;
                }

              

                // Запрос на обновление данных автора в базе
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = @"UPDATE author 
                                   SET name = @name, 
                                       bio = @bio, 
                                       photo = @photo, 
                                       birth_date = @birth_date 
                                   WHERE id_author = @id_author";

                            using (var cmd = new NpgsqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("id_author", authorId);
                                cmd.Parameters.AddWithValue("name", authorName);
                                cmd.Parameters.AddWithValue("bio", authorBio);
                                cmd.Parameters.AddWithValue("photo", authorPhoto);
                                cmd.Parameters.AddWithValue("birth_date", authorBirthDate ?? (object)DBNull.Value);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Автор с указанным ID не найден.");
                                    return;
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Данные автора успешно обновлены!");
                            LoadAuthors();
                        }
                        catch (NpgsqlException ex) when (ex.SqlState == "23505")
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ошибка уникальности данных!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при редактировании автора: {ex.Message}");
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID автора (число).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}");
            }
        }

        private void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            int authorId = Convert.ToInt32(AuthorIdTextBox.Text);

            // Check if the Book ID is valid
            if (authorId <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID автора для удаления.");
                return;
            }

            // Prompt the user for confirmation before deleting
            var result = MessageBox.Show("Вы уверены, что хотите удалить этого автора?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        // SQL query to delete the author by ID
                        string query = "DELETE FROM author WHERE id_author = @id_author";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            // Add the parameter for the author ID
                            cmd.Parameters.AddWithValue("id_author", authorId);

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the deletion was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Автор успешно удален.");
                                LoadAuthors(); // Обновляем список авторов после удаления
                            }
                            else
                            {
                                MessageBox.Show("Автор с таким ID не найден.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении автора: {ex.Message}");
                }
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            int bookId = Convert.ToInt32(BookIdTextBox.Text);

            if (bookId <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID книги.");
                return;
            }

            string bookTitle = BookTitleTextBox.Text;
            decimal bookPrice = Convert.ToDecimal(BookPriceTextBox.Text);
            DateTime bookPublicationDate = BookPublicationDatePicker.SelectedDate ?? DateTime.Now;
            string bookIsbn = BookIsbnTextBox.Text;
            string bookDescription = BookDescriptionTextBox.Text;
            string bookCoverImage = BookCoverImageTextBox.Text;
            int bookQuantity = Convert.ToInt32(BookQuantityTextBox.Text);
            int bookAuthorId = Convert.ToInt32(BookAuthorIdTextBox.Text);
            int bookCategoryId = (int)BookCategoryComboBox.SelectedValue;
            int bookTrendId = (int)BookTrendComboBox.SelectedValue;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Обновляем данные книги
                        string bookQuery = "UPDATE book SET title = @title, price = @price, publication_date = @publication_date, " +
                                         "isbn = @isbn, description = @description, cover_image = @cover_image, " +
                                         "quantity_books = @quantity_books, id_author = @id_author, id_category = @id_category " +
                                         "WHERE id_book = @id_book";

                        using (var cmd = new NpgsqlCommand(bookQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("id_book", bookId);
                            cmd.Parameters.AddWithValue("title", bookTitle);
                            cmd.Parameters.AddWithValue("price", bookPrice);
                            cmd.Parameters.AddWithValue("publication_date", bookPublicationDate);
                            cmd.Parameters.AddWithValue("isbn", bookIsbn);
                            cmd.Parameters.AddWithValue("description", bookDescription);
                            cmd.Parameters.AddWithValue("cover_image", bookCoverImage);
                            cmd.Parameters.AddWithValue("quantity_books", bookQuantity);
                            cmd.Parameters.AddWithValue("id_author", bookAuthorId);
                            cmd.Parameters.AddWithValue("id_category", bookCategoryId);

                            cmd.ExecuteNonQuery();
                        }

                        // 2. Обновляем связь с трендом
                        // Сначала удаляем старую связь
                        string deleteTrendQuery = "DELETE FROM book_trend WHERE id_book = @id_book";
                        using (var cmd = new NpgsqlCommand(deleteTrendQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("id_book", bookId);
                            cmd.ExecuteNonQuery();
                        }

                        // Затем добавляем новую
                        string insertTrendQuery = "INSERT INTO book_trend (id_book, id_trend) VALUES (@id_book, @id_trend)";
                        using (var cmd = new NpgsqlCommand(insertTrendQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("id_book", bookId);
                            cmd.Parameters.AddWithValue("id_trend", bookTrendId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Данные книги успешно обновлены, включая тренд!");
                        LoadBooks();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при редактировании книги: {ex.Message}");
                    }
                }
            }
        }

        private void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BookIdTextBox.Text, out int bookId))
            {
                LoadBookDataById(bookId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID книги.");
            }
        }


        // Метод для добавления пользователя
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = UserLoginTextBox.Text;
                string name = UserNameTextBox.Text;
                string email = UserEmailTextBox.Text;
                string phone = UserPhoneTextBox.Text;
                string password = UserPasswordBox.Password;
                int roleId = 1;

                // Валидация полей
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Логин не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Пароль не может быть пустым!");
                    return;
                }

                // Хешируем пароль
                string passwordHash = HashPassword(password);

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO UserWpf (login, name, email, number_phone, password_hash, created_at, id_role) " +
                                  "VALUES (@login, @name, @email, @phone, @passwordHash, @createdAt, @roleId)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("login", login);
                        cmd.Parameters.AddWithValue("name", name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("passwordHash", passwordHash);
                        cmd.Parameters.AddWithValue("createdAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("roleId", roleId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Пользователь успешно добавлен!");
                LoadUsers(); // Обновляем список пользователей
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}");
            }
        }

        // Метод для редактирования пользователя
        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(UserIdTextBox.Text);
                string login = UserLoginTextBox.Text;
                string name = UserNameTextBox.Text;
                string email = UserEmailTextBox.Text;
                string phone = UserPhoneTextBox.Text;
                string password = UserPasswordBox.Password;
                int roleId = 1;

                // Валидация полей
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Логин не может быть пустым!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email не может быть пустым!");
                    return;
                }

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE UserWpf SET login = @login, name = @name, email = @email, " +
                                  "number_phone = @phone, id_role = @roleId ";

                    // Добавляем обновление пароля только если он был введен
                    if (!string.IsNullOrEmpty(password))
                    {
                        string passwordHash = HashPassword(password);
                        query += ", password_hash = @passwordHash ";
                    }

                    query += "WHERE id_user = @userId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("userId", userId);
                        cmd.Parameters.AddWithValue("login", login);
                        cmd.Parameters.AddWithValue("name", name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("roleId", roleId);

                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("passwordHash", HashPassword(password));
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные пользователя успешно обновлены!");
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким ID не найден.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании пользователя: {ex.Message}");
            }
        }

        // Метод для удаления пользователя
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = Convert.ToInt32(UserIdTextBox.Text);

            if (userId <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный ID пользователя.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?",
                                       "Подтверждение",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "DELETE FROM UserWpf WHERE id_user = @userId";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("userId", userId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Пользователь успешно удален.");
                                LoadUsers();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь с таким ID не найден.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}");
                }
            }
        }

        // Метод для хеширования пароля (используйте тот же, что и в вашем проекте)
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Метод для загрузки данных пользователя по ID
        private void LoadUserDataById(int userId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT login, name, email, number_phone, id_role FROM UserWpf WHERE id_user = @userId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("userId", userId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserLoginTextBox.Text = reader["login"].ToString();
                                UserNameTextBox.Text = reader["name"].ToString();
                                UserEmailTextBox.Text = reader["email"].ToString();
                                UserPhoneTextBox.Text = reader["number_phone"].ToString();
                                UserPasswordBox.Password = ""; // Пароль не показываем
                            }
                            else
                            {
                                MessageBox.Show("Пользователь с таким ID не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных пользователя: {ex.Message}");
            }
        }
        private void SearchUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserIdTextBox.Text, out int userId))
            {
                LoadUserDataById(userId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID пользователя.");
            }
        }



        private void LoadAuthorDataById(int authorId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT name, bio, photo, birth_date FROM author WHERE id_author = @authorId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("authorId", authorId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AuthorNameTextBox.Text = reader["name"].ToString();
                                AuthorBioTextBox.Text = reader["bio"].ToString();
                                AuthorPhotoTextBox.Text = reader["photo"].ToString();

                                if (reader["birth_date"] != DBNull.Value)
                                {
                                    AuthorBirthDatePicker.SelectedDate = (DateTime)reader["birth_date"];
                                }
                                else
                                {
                                    AuthorBirthDatePicker.SelectedDate = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Автор с таким ID не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных автора: {ex.Message}");
            }
        }

     private void SearchAuthorButton_Click(object sender, RoutedEventArgs e)
{
    if (int.TryParse(AuthorIdTextBox.Text, out int authorId))
    {
        LoadAuthorDataById(authorId);
    }
    else
    {
        MessageBox.Show("Пожалуйста, введите корректный ID автора.");
    }
}



        private void Button_ClickСlose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
