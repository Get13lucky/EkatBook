using EkatBooks.BasketAndProfile;
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

namespace EkatBooks
{
    /// <summary>
    /// Логика взаимодействия для PageRecoveryPassword.xaml
    /// </summary>
    public partial class PageRecoveryPassword : Page
    {
        private Frame _windowProfileFrame;
        public static int  count = 0;
        private int userId;
        public PageRecoveryPassword(Frame windowProfileFrame)
        {
            InitializeComponent();
            _windowProfileFrame = windowProfileFrame;
        }
        private void TextBox_GotFocusLoginNumber(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxLoginNumber.Text == "Email")
            {
                inputTextBoxLoginNumber.Text = "";
                inputTextBoxLoginNumber.Foreground = new SolidColorBrush(Colors.Black);
            }

        }


        private void TextBox_LostFocusLoginNumber(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxLoginNumber.Text))
            {
                inputTextBoxLoginNumber.Text = "Email";
                inputTextBoxLoginNumber.Foreground = new SolidColorBrush(Colors.Gray);
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
        private void TextBox_GotFocusNewPassword(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxLoginNewPassword.Text == "Новый пароль")
            {
                inputTextBoxLoginNewPassword.Text = "";
                inputTextBoxLoginNewPassword.Foreground = new SolidColorBrush(Colors.Black);
            }

        }


        private void TextBox_LostFocusLoginNewPassword(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxLoginNewPassword.Text))
            {
                inputTextBoxLoginNewPassword.Text = "Новый пароль";
                inputTextBoxLoginNewPassword.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }


        private void ReturnProfile(object sender, RoutedEventArgs e)
        {
            _windowProfileFrame.Navigate(new PageProfile_1(_windowProfileFrame));
        }

        public bool UpdateUserPassword(string login, string email, string newPassword)
        {
            try
            {
                // Валидация входных параметров
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Логин не может быть пустым!");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email не может быть пустым!");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Новый пароль не может быть пустым!");
                    return false;
                }

                string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";
                string passwordHash = HashPassword(newPassword);

                string query = @"
UPDATE public.userwpf
SET password_hash = @PasswordHash
WHERE login = @Login AND email = @Email";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Пароль успешно обновлен!");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить пароль. Проверьте правильность введенных данных.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пароля: {ex.Message}");
                return false;
            }
        }

        // Добавьте этот метод, если у вас его еще нет
        private int GetUserId(string login, string connectionString)
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


        public static bool IsEmailInDatabase(string email, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM public.userwpf WHERE email = @Email";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;  
                }
            }
        }
        public static bool IsValidPassword(string password)
        {
            var passwordPattern = @"^(?=.*\d.*\d)(?=.*[!@#$%^&*()_+=[\]{};:'"",<>\./?\\|`~\-]).{6,}$";

            return Regex.IsMatch(password, passwordPattern);
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


        public static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        private static string HashPassword(string NewPassword)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(NewPassword);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }

        private void Button_ClickPagePassword(object sender, RoutedEventArgs e)
        {
            UpdateUserPassword(inputTextBoxLogin.Text, inputTextBoxLoginNumber.Text, inputTextBoxLoginNewPassword.Text);
        }
    }
}
