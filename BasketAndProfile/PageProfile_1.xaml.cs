using EkatBooks.BasketAndProfile;
using EkatBooks.PagesOfTrends;
using Ekaterinburg.PagesOfCategories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;


namespace EkatBooks
{
    /// <summary>
    /// Логика взаимодействия для PageProfile_1.xaml
    /// </summary>
    public partial class PageProfile_1 : Page
    {

        public Frame MainFrame;

        private Frame _frame;

        // Конструктор с передачей Frame
        // Конструктор PageProfile_1, который принимает Frame
        public PageProfile_1(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame ?? throw new ArgumentNullException(nameof(mainFrame));  // Проверка на null
            //eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eyeClose.png"));

        }

        int count = 0;

        private WindowProfile _windowProfile;

        public bool InputUserProfile(string login, string newPassword)
        {
            try
            {
             

                string passwordHash = HashPassword(newPassword);
                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

                // Проверка существования логина в базе данных
                if (!IsLoginInDatabase(login, connectionString))
                {
                    MessageBox.Show("Логин не найден в базе данных.");
                    return false; // Логин не найден — возвращаем false
                }

                // Проверка пароля
                if (!IsPasswordInDatabase(passwordHash, connectionString, login))
                {
                    MessageBox.Show("Пароль не найден в базе данных.");
                    return false; // Пароль не найден — возвращаем false
                }

                // Проверка на пустой пароль
                if (inputTextBoxPassword_3.Text.Length == 0)
                {
                    MessageBox.Show("Пароль пустой.");
                    return false; // Если пароль пустой — возвращаем false
                }

                // Получение ID пользователя
                int userId = GetUserId(login, connectionString);

                //// Переносим товары из временной корзины, если они есть
                //if (TempCartManager.TempCartItems != null && TempCartManager.TempCartItems.Count > 0)
                //{
                //    TempCartManager.MoveToUserCart(userId);
                //    MessageBox.Show("Товары из вашей временной корзины были добавлены в вашу корзину.");
                //}

                // Если логин и пароль прошли проверку
                if (userId != 0)
                {
                    // Сохранение сессии пользователя
                    UserSession.CurrentUserId = userId;
                    UserSession.SaveSession();

                    // Перемещение товаов в корзину пользователя
                    TempCartManager.MoveToUserCart(userId);

                    // Логика навигации
                    if (UserSession.IsLoggedIn)
                    {
                        if (MainFrame != null)
                        {
                            // Используем MainFrame для навигации
                            this.MainFrame.Navigate(new PageUnSuccessCreation(this.MainFrame, UserSession.CurrentUserId));
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: MainFrame не инициализирован.");
                        }
                    }
                    else
                    {
                        if (MainFrame != null)
                        {
                            // Передаем MainFrame в конструктор PageProfile_1
                            this.MainFrame.Navigate(new PageProfile_1(this.MainFrame));
                        }
                    }
                    int roleId = GetRoleId(login, connectionString);

                    if (roleId == 2)
                    {
                        WindowManager windowManager = new WindowManager();
                        windowManager.Show();
                    }
                    else
                    {
                        MainFrame.Navigate(new PageUnSuccessCreation(MainFrame, userId));
                    }

                    return true; // Успешная авторизация
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при получении данных пользователя.");
                    return false; // Ошибка при получении ID
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                return false;
            }
        }






        public static bool IsLoginInDatabase(string login, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM public.userwpf WHERE login = @Login";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public static int GetUserId(string login, string connectionString)
        {
            string query = "SELECT id_user FROM public.userwpf WHERE login = @Login";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1; 
                    }
                }
            }
        }


        public static int GetRoleId(string login, string connectionString)
        {
            string query = "SELECT id_role FROM public.userwpf WHERE login = @Login";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }


        public static bool IsPasswordInDatabase(string passwordHash, string connectionString, string login)
        {
            string query = "SELECT COUNT(*) FROM public.userwpf WHERE password_hash = @PasswordHash AND login = @Login";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        private void PageCreateProfile(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new PageCreateProfile(MainFrame));
        }
        //private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        //{



        //    if (inputTextBoxPassword_3.Visibility == Visibility.Visible)
        //    {
        //        eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eye.png"));
        //        inputTextBoxPasswordVisible_2.Text = inputTextBoxPassword_3.Password;
        //        inputTextBoxPasswordVisible_2.Visibility = Visibility.Visible;
        //        inputTextBoxPassword_3.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {








        //        eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eyeClose.png"));
        //        inputTextBoxPassword_3.Password = inputTextBoxPasswordVisible_2.Text;
        //        inputTextBoxPassword_3.Visibility = Visibility.Visible;
        //        inputTextBoxPasswordVisible_2.Visibility = Visibility.Collapsed;
        //    }
        //}
        private static string HashPassword(string NewPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(NewPassword);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void TextBox_GotFocusLogin(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxLogin.Text == "Логин")
            {
                inputTextBoxLogin.Text = "";
                inputTextBoxLogin.Foreground = new SolidColorBrush(Colors.Black);
            }

        }

        private void TextBox_LostFocusLogin(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxLogin.Text))
            {
                inputTextBoxLogin.Text = "Логин";
                inputTextBoxLogin.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }


        private void TextBox_GotFocusPassword(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxPassword_3.Text == "Пароль")
            {
                inputTextBoxPassword_3.Text = "";
                inputTextBoxPassword_3.Foreground = new SolidColorBrush(Colors.Black);
            }

        }

        private void TextBox_LostFocusPassword(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxPassword_3.Text))
            {
                inputTextBoxPassword_3.Text = "Пароль";
                inputTextBoxPassword_3.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void PageRecoveryPasswordReturn(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageRecoveryPassword(MainFrame));
        }
        //private void TextBox_GotFocusPassword(object sender, RoutedEventArgs e)
        //{

        //    if (inputTextBoxPassword_3.Text == "Пароль")
        //    {
        //        inputTextBoxPassword_2.Text = "";
        //        inputTextBoxPassword_2.Foreground = new SolidColorBrush(Colors.Black);
        //    }

        //}


        //private void TextBox_LostFocusPassword(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(inputTextBoxPassword.Text))
        //    {
        //        inputTextBoxPassword.Text = "Пароль";
        //        inputTextBoxPassword.Foreground = new SolidColorBrush(Colors.Gray);
        //    }
        //}

        private void Button_ClickInputUserProfile(object sender, RoutedEventArgs e)
        {
            InputUserProfile(inputTextBoxLogin.Text, inputTextBoxPassword_3.Text);
        }
    }
}
