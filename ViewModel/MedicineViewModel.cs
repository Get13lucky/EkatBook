using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkatBooks.ViewModel
{
    class MedicineViewModel
    {
        private readonly BooksContext _context;

        public ObservableCollection<Book> Books { get; set; }

        public MedicineViewModel()
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
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == 5) 
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по возрастанию цены
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по убыванию цены
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по названию (А-Я)
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по названию (Я-А)
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по дате (новые книги первыми)
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }

        // Метод для сортировки по дате (старые книги первыми)
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
                if (book.IdCategory == 5)
                {
                    Books.Add(book);
                }
            }
        }
    }

}
