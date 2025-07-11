using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EkatBooks.ViewModel
{
    class ByChanceViewModel1
    {
        private readonly BooksContext _context;

        public ObservableCollection<Book> Books { get; set; }
        Random random = new Random();

        public ByChanceViewModel1()
        {
            _context = new BooksContext();
            Books = new ObservableCollection<Book>();
            Random random = new Random();

        }

        public async Task LoadBooksAsync()
        {
            int randomNumber = random.Next(1, 9);
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncUptoPrice()
        {
            int randomNumber = random.Next(1, 9);

            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.Price)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncDowntoPrice()
        {
            int randomNumber = random.Next(1, 9);

            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderByDescending(b => b.Price)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncUptoNameBook()
        {
            int randomNumber = random.Next(1, 9);

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
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncDowntoNameBook()
        {
            int randomNumber = random.Next(1, 9);

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
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncUptoDate()
        {
            int randomNumber = random.Next(1, 9);

            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderByDescending(b => b.PublicationDate)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }

        public async Task LoadBooksAsyncDowntoDate()
        {
            int randomNumber = random.Next(1, 9);

            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .OrderBy(b => b.PublicationDate)
                                      .ToListAsync();

            Books.Clear();
            foreach (var book in books)
            {
                if (book.IdCategory == randomNumber)
                {
                    Books.Add(book);
                }
            }
        }
    }
}