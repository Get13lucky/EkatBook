using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkatBooks
{
    public class PageUnSuccessCreationViewModel
    {
        private readonly BooksContext _context;

        public ObservableCollection<Userwpf> userWpf { get; set; }


        public PageUnSuccessCreationViewModel()
        {
            _context = new BooksContext();
            userWpf = new ObservableCollection<Userwpf>();
        }

        public async Task LoadUserByIdAsync(int userId)
        {
            var users = await _context.Userwpfs
                                      .Where(u => u.IdUser == userId) 
                                      .Select(u => new
                                      {
                                          u.Login,
                                          u.Email,
                                          u.Name,
                                          u.NumberPhone
                                      })
                                      .ToListAsync();

            userWpf.Clear();  

            foreach (var user in users)
            {
                userWpf.Add(new Userwpf
                {
                    Login = user.Login,
                    Email = user.Email,
                    Name = user.Name,
                    NumberPhone = user.NumberPhone
                });
            }
        }
    }
}
