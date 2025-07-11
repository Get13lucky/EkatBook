using EkatBooks.MainMenuPagesWindows;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EkatBooks
{
    public partial class RightsPage : Page
    {
        // Переопределяем конструктор страницы
   
        public RightsPage()
        {
            InitializeComponent();
            var viewModel = new RightsPageViewModel();
            DataContext = viewModel;

            // Добавьте проверку перед вызовом
            if (viewModel != null)
            {
                LoadDataAsync(viewModel);
            }
            else
            {
                MessageBox.Show("ViewModel is null!");
            }
        }

        

        // Асинхронный метод для загрузки данных
        private async void LoadDataAsync(RightsPageViewModel viewModel)
        {
            if (viewModel != null)
            {
                await viewModel.LoadAuthorsAsync();
            }
            else
            {
                MessageBox.Show("Не удалось загрузить данные, так как ViewModel не была инициализирована.");
            }
        }
    }
}
