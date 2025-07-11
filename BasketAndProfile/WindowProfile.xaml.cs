using EkatBooks.BasketAndProfile;
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
using System.Windows.Shapes;

namespace EkatBooks
{
    /// <summary>
    /// Логика взаимодействия для WindowProfile.xaml
    /// </summary>
    public partial class WindowProfile : Window
    {


        private int userId;

        public WindowProfile()
        {
            InitializeComponent();

            // Загрузка сессии при запуске
            UserSession.LoadSession();
           

            // Если пользователь уже вошел, сразу показываем профиль
            if (UserSession.IsLoggedIn)
            {
                this.WindowProfileFrame.Navigate(new PageUnSuccessCreation(this.WindowProfileFrame, UserSession.CurrentUserId));
            }
            else
            {
                this.WindowProfileFrame.Navigate(new PageProfile_1(this.WindowProfileFrame));
            }
        }

        public WindowProfile(int userId)
        {
            InitializeComponent();

            if (userId > 0)
            {
                // Сохраняем ID в менеджере сессий
                UserSession.CurrentUserId = userId;
                UserSession.SaveSession();

                this.WindowProfileFrame.Navigate(new PageUnSuccessCreation(this.WindowProfileFrame, userId));
            }
            else
            {
                this.WindowProfileFrame.Navigate(new PageProfile_1(this.WindowProfileFrame));
            }
        }

        public void Logout()
        {
            UserSession.ClearSession();
            this.WindowProfileFrame.Navigate(new PageProfile_1(this.WindowProfileFrame));
        }
        //public void NavigateToCreateProfile()
        //{
        //    // Навигация на страницу создания профиля
        //    this.WindowProfileFrame.Navigate(new PageCreateProfile());
        //}
        //private void PageCreateProfile(object sender, RoutedEventArgs e)
        //{
        //    ProfileFrame.Navigate(new PageCreateProfile());



        //}
        //private void TextBox_GotFocusLogin(object sender, RoutedEventArgs e)
        //{

        //    if (inputTextBoxLogin.Text == "Логин")
        //    {
        //        inputTextBoxLogin.Text = "";
        //        inputTextBoxLogin.Foreground = new SolidColorBrush(Colors.Black);
        //    }

        //}


        //private void TextBox_LostFocusLogin(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(inputTextBoxLogin.Text))
        //    {
        //        inputTextBoxLogin.Text = "Логин";
        //        inputTextBoxLogin.Foreground = new SolidColorBrush(Colors.Gray);
        //    }
        //}
        //private void TextBox_GotFocusPassword(object sender, RoutedEventArgs e)
        //{

        //    if (inputTextBoxPassword.Text == "Пароль")
        //    {
        //        inputTextBoxPassword.Text = "";
        //        inputTextBoxPassword.Foreground = new SolidColorBrush(Colors.Black);
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
        //public void Window_Deactivated(object sender, EventArgs e)
        //{

        //    this.Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        this.Close();
        //    }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);


        //}



    }
}
