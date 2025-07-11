using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkatBooks.ViewModel
{
    class NobelViewModel
    {
        private readonly BooksContext _context;

        // Коллекция книг
        public ObservableCollection<Book> Books { get; set; }

        public NobelViewModel()
        {
            _context = new BooksContext();
            Books = new ObservableCollection<Book>();
        }

        // Метод для начальной загрузки данных
        public async Task LoadBooksAsync()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        // Метод для сортировки по возрастанию цены
        public async Task LoadBooksAsyncUptoPrice()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.Price)
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        // Метод для сортировки по убыванию цены
        public async Task LoadBooksAsyncDowntoPrice()
        {
            // Получаем книги, которые связаны с трендом "Новинки" (id_trend = 3), и сортируем их по цене по убыванию
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)  // Загружаем информацию об авторе
                                      .Include(b => b.IdCategoryNavigation)  // Загружаем информацию о категории
                                      .Where(b => _context.BookTrends
                                                           .Any(bt => bt.IdTrend == 7 && bt.IdBook == b.IdBook))  // Фильтруем книги по тренду "Новинки"
                                      .OrderByDescending(b => b.Price)  // Сортируем книги по цене (убывание)
                                      .ToListAsync();  // Выполняем запрос в базу данных

            // Очищаем коллекцию и добавляем отфильтрованные книги
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }



        // Метод для сортировки по названию (А-Я)
        public async Task LoadBooksAsyncUptoNameBook()
        {
            var books = await _context.Books
                                      .FromSqlRaw("SELECT * FROM public.book ORDER BY title")
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                await _context.Entry(book)
                              .Reference(b => b.IdAuthorNavigation)
                              .LoadAsync();

                await _context.Entry(book)
                              .Reference(b => b.IdCategoryNavigation)
                              .LoadAsync();

                Books.Add(book);
            }
        }

        // Метод для сортировки по названию (Я-А)
        public async Task LoadBooksAsyncDowntoNameBook()
        {
            var books = await _context.Books
                                      .FromSqlRaw("SELECT * FROM public.book ORDER BY title DESC")
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                await _context.Entry(book)
                              .Reference(b => b.IdAuthorNavigation)
                              .LoadAsync();

                await _context.Entry(book)
                              .Reference(b => b.IdCategoryNavigation)
                              .LoadAsync();

                Books.Add(book);
            }
        }

        // Метод для сортировки по дате (новые книги первыми)
        public async Task LoadBooksAsyncUptoDate()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderByDescending(b => b.PublicationDate)
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        // Метод для сортировки по дате (старые книги первыми)
        public async Task LoadBooksAsyncDowntoDate()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.PublicationDate)
                                      .Where(b => _context.BookTrends
                                                  .Where(bt => bt.IdTrend == 7)
                                                  .Select(bt => bt.IdBook)
                                                  .Contains(b.IdBook))
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    }
}
