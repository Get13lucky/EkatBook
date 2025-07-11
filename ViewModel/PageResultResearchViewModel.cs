using EkatBooks.MainMenuPagesWindows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkatBooks
{
    public class PageResultResearchViewModel
    {
        private readonly BooksContext _context;

        public ObservableCollection<Book> Books { get; set; }

        public PageResultResearchViewModel()
        {
            _context = new BooksContext();
            Books = new ObservableCollection<Book>();
        }

        public async Task LoadBooksAsync(string searchQuery)
        {
            
            var books = await _context.Books
                                      .Include(b => b.IdAuthorNavigation)
                                      .Include(b => b.IdCategoryNavigation)
                                      .ToListAsync();

           
            Books.Clear();
            foreach (var book in books)
            {
                
                if (book.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    book.IdAuthorNavigation.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    Books.Add(book);
                }
            }

        }
    }

}
