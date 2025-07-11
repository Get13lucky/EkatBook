using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace EkatBooks.MainMenuPagesWindows
{
    internal class RightsPageViewModel
    {
        private readonly BooksContext _context;

        public ObservableCollection<Author> Authors { get; set; }

        // Конструктор
        public RightsPageViewModel()
        {
            _context = new BooksContext();
            Authors = new ObservableCollection<Author>();


        }

        public async Task LoadAuthorsAsync()
        {
            try
            {



                var authorsList = await _context.Authors.ToListAsync();

                foreach (var author in authorsList)
                {
                    if (!string.IsNullOrEmpty(author.Photo))
                    {
                        // Получаем только имя файла из полного пути
                        author.Photo = Path.GetFileName(author.Photo);
                    }
                }

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();

                // Загружаем всех авторов из контекста
                authorsList = await _context.Authors.ToListAsync();

                // Очищаем ObservableCollection и добавляем новые данные
                Authors.Clear();
                foreach (var author in authorsList)
                {
                    Authors.Add(author);
                }
            }
            catch (Exception ex)
            {
                // Если произошла ошибка при загрузке, выводим ее в MessageBox
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}
