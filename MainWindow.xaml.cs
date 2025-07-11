using EkatBooks.MainMenuPagesWindows;
using EkatBooks.PagesOfTrends;
using Ekaterinburg.PagesOfCategories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EkatBooks
{

    public partial class MainWindow : Window
    {
        private int userId;
        //bool isDarkTheme = false;

        // Создаем поля для хранения ссылок на страницы
        //private StoreWindow storewindow;
        //private PublishingHouse publishinghouse;
        //private NewsPage newspage;
        //private BusinessLit businessLit;
        //private ReturnMain returnMain;
        //private RightsPage rightsPage;

        public MainWindow()
        {
            InitializeComponent();
            userId = GetCurrentUserId();

            // Инициализируем страницы
            //storewindow = new StoreWindow();
            //publishinghouse = new PublishingHouse();
            //newspage = new NewsPage();
            //businessLit = new BusinessLit();
            //returnMain = new ReturnMain();
            //rightsPage = new RightsPage();

            // Отображаем начальную страницу в MainFrame
            /*MainFrame.Navigate(returnMain);*/ // Или другая страница по умолчанию
            MainFrame.Navigate(new ReturnMain());
        }
        private int GetCurrentUserId()
        {
            // Теперь просто возвращаем ID из UserSession
            return UserSession.CurrentUserId;
        }

        //private void ChangeColour(object sender, RoutedEventArgs e)
        //{
        //    // Теперь у нас есть ссылки на все страницы, и мы можем работать с ними напрямую
        //    if (isDarkTheme == false)
        //    {
        //        /*Color uniqueColour = Color.FromRgb(36/*, 36, 3*6);*/
        //        //Color uniqueColour_2 = Color.FromRgb(200, 200, 200);

        //        MainGrid.Background = Brushes.Black;
        //        returnMain.ReturnMainText.Foreground = Brushes.White;

        //        MainStack.Background = Brushes.Black;
        //        MainStack_2.Background = Brushes.Black;
        //        MainWrapPanel.Background = Brushes.Black;
        //        MainMenu.Background = Brushes.Black;
        //        MainTabControl.Background = Brushes.Black;
        //        MainButton_1.Background = Brushes.Black;

        //        storewindow.StorePageStack.Background = Brushes.Black;
        //        publishinghouse.PublishingHouseBackround.Background = Brushes.Black;
        //        newspage.NewsPageBackround.Background = Brushes.Black;
        //        businessLit.BusinessListBackround.Background = Brushes.Black;
        //        returnMain.ReturnMainBackround.Background = Brushes.Black;
        //        rightsPage.RightsPageBackround.Background = Brushes.Black;

        //        isDarkTheme = true;
        //    }
        //    else
        //    {
        //        returnMain.ReturnMainText.Foreground = Brushes.White;

        //        MainGrid.Background = Brushes.Green;
        //        MainStack.Background = Brushes.Green;
        //        MainStack_2.Background = Brushes.Green;
        //        MainWrapPanel.Background = Brushes.Green;
        //        MainMenu.Background = Brushes.Green;
        //        MainTabControl.Background = Brushes.Green;
        //        MainButton_1.Background = Brushes.Green;

        //        storewindow.StorePageBackround.Background = Brushes.Green;
        //        publishinghouse.PublishingHouseBackround.Background = Brushes.Green;
        //        newspage.NewsPageBackround.Background = Brushes.Green;
        //        businessLit.BusinessListBackround.Background = Brushes.Green;
        //        returnMain.ReturnMainBackround.Background = Brushes.Green;
        //        rightsPage.RightsPageBackround.Background = Brushes.Green;

        //        isDarkTheme = false;
        //    }
        //}
        private void StoreOpen(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StoreWindow());

           

        }
            bool check = false;
        // Объявите это поле в классе
        private WindowProfile _windowProfile;

        private void WindowProfileOpen(object sender, RoutedEventArgs e)
        {
            // Если окно уже существует
            if (_windowProfile != null)
            {
                // Если окно не закрыто (просто невидимо)
                if (!_windowProfile.IsVisible)
                {
                    _windowProfile.Show();
                }
                // Активируем окно (выводим на передний план)
                _windowProfile.Activate();
                return;
            }

            // Создаём новое окно, если его не было
            if (UserSession.IsLoggedIn)
            {
                _windowProfile = new WindowProfile(UserSession.CurrentUserId);
            }
            else
            {
                _windowProfile = new WindowProfile();
            }

            // Обработчик закрытия окна, чтобы сбросить ссылку
            _windowProfile.Closed += (s, args) => _windowProfile = null;

            // Показываем окно
            _windowProfile.Show();
        }


        private void PublishingHouse(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PublishingHouse());

       

        }
        private void PageBasket(object sender, RoutedEventArgs e)
        {
            //PageScrollViewer.ScrollToTop();
            MainFrame.Navigate(new PageBasket());



        }
        private void BusinessLit(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BusinessLit());



        }

        private void PageRecommend(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageRecommend());



        }

        private void PageFilm(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageFilm());



        }
        private void PageTop(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageTop());
        }

        private void PageNobel(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageNobel());



        }

        private void PageByChance(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageByChance());



        }

        private void NewsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NewsPage());



        }
        private void RightsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RightsPage());



        }
        private void PageComputerCategory(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageComputerCategory());



        }
        private void PagePubHis(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagePubHis());



        }
        private void PageMedical(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageMedical());



        }
        private void PagePsycho(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PagePsycho());



        }
        private void PageArt(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageArt());



        }
        private void PageLaw(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageLaw());



        }
        private void PageScienceEdu(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageScienceEdu());



        }
        private void ReturnMain(object sender, RoutedEventArgs e)
        {
            //if (isDarkTheme==false)
            //{
            //    returnMain.ReturnMainText.Foreground = Brushes.Black;
            //}
            //else
            //{
            //    returnMain.ReturnMainText.Foreground = Brushes.White;
            //}
              MainFrame.Navigate(new ReturnMain());
        }



        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (inputTextBox.Text == "Найти книгу...")
            {
                inputTextBox.Text = "";
                inputTextBox.Foreground = new SolidColorBrush(Colors.Black); 
            }
        }


       

        private void Button_ClickTelegram(object sender, RoutedEventArgs e)
        {
            string channelUrl = "https://telegram.org/";
                Process.Start(new ProcessStartInfo
                {
                    FileName = channelUrl,
                    UseShellExecute = true
                });
        }
        private void Button_ClickWhatsUp(object sender, RoutedEventArgs e)
        {
            string channelUrl = "https://www.whatsapp.com/";
            Process.Start(new ProcessStartInfo
            {
                FileName = channelUrl,
                UseShellExecute = true
            });
        }

       

        private void NewsBooks(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageNewsBooks());
        }

        private void PageBestSeller(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageBestSeller());
        }

        private void PageSoonBooks(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageSoonBooks());
        }

        private void PageResultResearch(object sender, RoutedEventArgs e)
        {
         
            string searchText = inputTextBox.Text;
            PageResultResearch pageResultResearch = new PageResultResearch(searchText);
            MainFrame.Navigate(pageResultResearch);
        

        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(inputTextBox.Text))
            {
                if (inputTextBox.Text != "Найти книгу...")
                {
                    ResearchButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    
                }

            }
        }

        private bool isMouseOverButton = false;

        private void buttonLoupe_MouseEnter(object sender, MouseEventArgs e)
        {
            isMouseOverButton = true;
        }

        private void buttonLoupe_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseOverButton = false;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Проверяем, не наведена ли мышь на кнопку
            if (string.IsNullOrEmpty(inputTextBox.Text) || inputTextBox.Text != "Найти книгу..." && !isMouseOverButton)
            {
                inputTextBox.Text = "Найти книгу...";
                inputTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }



    }
}
