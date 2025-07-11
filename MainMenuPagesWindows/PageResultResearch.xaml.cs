using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace EkatBooks.MainMenuPagesWindows
{
    /// <summary>
    /// Логика взаимодействия для PageResultResearch.xaml
    /// </summary>
    public partial class PageResultResearch : Page
    {
        public string ValueInput { get; set; }

        
        public PageResultResearch(string valueInput)
        {
            InitializeComponent();
            ValueInput = valueInput;  
            LoadDataAsync();

            if (ValueInput != "Найти книгу...")
            {
                ResultLabel.Content = $"Результат поиска \"{ValueInput}\""; // Обновляем текст метки
            }
            else
            {
                ResultLabel.Content = "Ничего не найдено";
            }
                

        }



        private async Task LoadDataAsync()
        {
            var viewModel = (PageResultResearchViewModel)DataContext;
            await viewModel.LoadBooksAsync(ValueInput);  
        }
        private void PageBasket(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new PageBasket());



        }
    }
}

