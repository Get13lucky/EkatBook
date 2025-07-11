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
using static System.Net.Mime.MediaTypeNames;

namespace EkatBooks
{
    /// <summary>
    /// Логика взаимодействия для PageCreateProfile.xaml
    /// </summary>
    public partial class PageCreateProfile : Page
    {
        private Frame _windowProfileFrame;
        private int userId;

        public PageCreateProfile(Frame windowProfileFrame)
        {
            InitializeComponent();
            _windowProfileFrame = windowProfileFrame;

            
            //eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eyeClose.png"));



        }


        private string connectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123";

        public bool CreateAccount( string email, string login, string password)
        {
            try
            {
                string passwordHash = HashPassword(password);

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO UserWpf (login, email, password_hash, created_at, id_role) " +
                                   "VALUES (@Login, @Email, @PasswordHash, @CreatedAt, @IdRole)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                    {

                        if (IsValidEmail(email))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            
                        }
                        else
                        {
                            MessageBox.Show("неправильный формат Email");
                        }

                        if (login.Length >= 6)
                        {
                            cmd.Parameters.AddWithValue("@Login", login);

                        }
                        else
                        {
                            MessageBox.Show("длинна логина должна быть минимум 6 символов");

                        }
                        if (Checkbox_1.IsChecked == true && Checkbox_2.IsChecked == true && Checkbox_3.IsChecked == true)
                        {
                            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        }
                        else
                        {
                            MessageBox.Show("Пароль не соответсвует требованиям");

                        }





                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IdRole", 1);
                   

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            _windowProfileFrame.Navigate(new PageSuccessCreation(_windowProfileFrame));
                            return true;
                        }
                        else
                        {
                            _windowProfileFrame.Navigate(new PageUnSuccessCreation(_windowProfileFrame, userId));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }





        //private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        //{
           
        //    if (inputTextBoxPassword_2.Visibility == Visibility.Visible)
        //    {
        //        eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eye.png"));
        //        inputTextBoxPasswordVisible.Text = inputTextBoxPassword_2.Password;  
        //        inputTextBoxPasswordVisible.Visibility = Visibility.Visible;  
        //        inputTextBoxPassword_2.Visibility = Visibility.Collapsed;  
        //    }
        //    else
        //    {
        //        eye.Source = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\EkatBooks\\images\\eyeClose.png"));
        //        inputTextBoxPassword_2.Password = inputTextBoxPasswordVisible.Text;  
        //        inputTextBoxPassword_2.Visibility = Visibility.Visible;  
        //        inputTextBoxPasswordVisible.Visibility = Visibility.Collapsed;  
        //    }
        //}

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count_digit = 0;
            int count_letter = 0;
           

            string ValueInputTextBoxPassword_2 = inputTextBoxPassword_2.Text;
            int lenngtText = ValueInputTextBoxPassword_2.Length;




            if (inputTextBoxPassword_2.Text == "Пароль")
            {
                return;
            }




            foreach (var item in ValueInputTextBoxPassword_2)
            {
                if (Char.IsDigit(item))
                {
                    count_digit++;
                }
                if (!Char.IsLetterOrDigit(item)) 
                {
                    count_letter++;
                }
            }

            if (lenngtText >= 6)
            {
                Checkbox_1.IsChecked = true;

            }
            if (lenngtText < 6)
            {
                Checkbox_1.IsChecked = false;

            }
            if (count_letter >= 1)
            {
                Checkbox_2.IsChecked = true;

            }
            if (count_letter < 1)
            {
                Checkbox_2.IsChecked = false;

            }
            if (count_digit >= 2)
            {
                Checkbox_3.IsChecked = true;

            }
            if (count_digit < 2)
            {
                Checkbox_3.IsChecked = false;

            }



        }



        private void TextBox_GotFocusLogin(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxLogin_2.Text == "Логин")
            {
                inputTextBoxLogin_2.Text = "";
                inputTextBoxLogin_2.Foreground = new SolidColorBrush(Colors.Black);
            }

        }


        private void TextBox_LostFocusLogin(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxLogin_2.Text))
            {
                inputTextBoxLogin_2.Text = "Логин";
                inputTextBoxLogin_2.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }


        private void TextBox_GotFocusPassword(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxPassword_2.Text == "Пароль")
            {
                inputTextBoxPassword_2.Text = "";
                inputTextBoxPassword_2.Foreground = new SolidColorBrush(Colors.Black);
            }

        }


        private void TextBox_LostFocusPassword(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxPassword_2.Text))
            {
                inputTextBoxPassword_2.Text = "Пароль";
                inputTextBoxPassword_2.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void TextBox_GotFocusEmail(object sender, RoutedEventArgs e)
        {

            if (inputTextBoxEmail.Text == "Email")
            {
                inputTextBoxEmail.Text = "";
                inputTextBoxEmail.Foreground = new SolidColorBrush(Colors.Black);
            }

        }


        private void TextBox_LostFocusEmail(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(inputTextBoxEmail.Text))
            {
                inputTextBoxEmail.Text = "Email";
                inputTextBoxEmail.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _windowProfileFrame.Navigate(new PageProfile_1(_windowProfileFrame));
     
        }
        private void NavigateSuccesCreation(object sender, RoutedEventArgs e)
        {
            _windowProfileFrame.Navigate(new PageSuccessCreation(_windowProfileFrame));

        }
        private void NavigateUnSuccesCreation(object sender, RoutedEventArgs e)
        {
            _windowProfileFrame.Navigate(new PageUnSuccessCreation(_windowProfileFrame, userId));

        }

        private void Button_ClickCreateProfile(object sender, RoutedEventArgs e)
        {
            CreateAccount(inputTextBoxEmail.Text,inputTextBoxLogin_2.Text,inputTextBoxPassword_2.Text);
        }
    }
}
