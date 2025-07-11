using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EkatBooks
{
    class PageReturnMainViewModel
    {
        private readonly BooksContext _context;

        public ObservableCollection<Book> Books { get; set; }

        public PageReturnMainViewModel()
        {
            _context = new BooksContext();
            Books = new ObservableCollection<Book>();
        }

        public async Task LoadBooksAsync()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.IdAuthorNavigation)
                    .Include(b => b.IdCategoryNavigation)
                    .ToListAsync();

                // Обработка изображений
                foreach (var book in books)
                {
                    if (!string.IsNullOrEmpty(book.CoverImage))
                    {
                        // Получаем только имя файла из полного пути
                        book.CoverImage = Path.GetFileName(book.CoverImage);
                    }
                }

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();

                // Загружаем все книги из контекста
                books = await _context.Books
                    .Include(b => b.IdAuthorNavigation)
                    .Include(b => b.IdCategoryNavigation)
                    .ToListAsync();

                // Очищаем ObservableCollection и добавляем новые данные
                Books.Clear();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            catch (Exception ex)
            {
                // Если произошла ошибка при загрузке, выводим ее в MessageBox
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        public async Task LoadBooksAsyncUptoPrice()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.Price)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
        public async Task LoadBooksAsyncDowntoPrice()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderByDescending(b => b.Price)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        public async Task LoadBooksAsyncUptoNameBook()
        {
            var books = await _context.Books
                                      .FromSqlRaw("SELECT * FROM public.book ORDER BY title")
                                      .ToListAsync();

            foreach (var book in books)
            {
                await _context.Entry(book)
                              .Reference(b => b.IdAuthorNavigation)
                              .LoadAsync();  

                await _context.Entry(book)
                              .Reference(b => b.IdCategoryNavigation)
                              .LoadAsync(); 
            }

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        public async Task LoadBooksAsyncDowntoNameBook()
        {
            var books = await _context.Books
                                      .FromSqlRaw("SELECT * FROM public.book ORDER BY title DESC")
                                      .ToListAsync();

            foreach (var book in books)
            {
                await _context.Entry(book)
                              .Reference(b => b.IdAuthorNavigation)
                              .LoadAsync();

                await _context.Entry(book)
                              .Reference(b => b.IdCategoryNavigation)
                              .LoadAsync();
            }

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        public async Task LoadBooksAsyncUptoDate()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderByDescending(b => b.PublicationDate)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
        public async Task LoadBooksAsyncDowntoDate()
        {
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.PublicationDate)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

    }
}
