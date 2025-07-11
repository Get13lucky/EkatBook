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

namespace EkatBooks.BasketAndProfile
{
    /// <summary>
    /// Логика взаимодействия для PageSuccessCreation.xaml
    /// </summary>
    public partial class PageSuccessCreation : Page
    {
        private Frame _windowProfileFrame;

        public PageSuccessCreation(Frame windowProfileFrame)
        {
            InitializeComponent();
            _windowProfileFrame = windowProfileFrame;

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _windowProfileFrame.Navigate(new PageProfile_1(_windowProfileFrame));

        }
    }
}
