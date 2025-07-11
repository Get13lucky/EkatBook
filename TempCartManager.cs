using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace EkatBooks
{
    public static class TempCartManager
    {
        // Список товаров в временной корзине
        public static List<CartItemViewModel> TempCartItems { get; set; } = new List<CartItemViewModel>();

        // Добавление товара во временную корзину
        public static bool AddToTempCart(Book book, int quantity = 1)
        {
            try
            {
                // Проверяем, есть ли уже такая книга в корзине
                var existingItem = TempCartItems.FirstOrDefault(item => item.BookId == book.IdBook);

                if (existingItem != null)
                {
                    // Книга уже есть в корзине, увеличиваем количество
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Добавляем новую книгу в корзину
                    TempCartItems.Add(new CartItemViewModel
                    {
                        CartItemId = -1, // Временный ID для новых элементов
                        BookId = book.IdBook,
                        Title = book.Title,
                        Price = book.Price,
                        Quantity = quantity,
                        CoverImage = book.CoverImage
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в временную корзину: {ex.Message}");
                return false;
            }
        }

        // Метод для перемещения товаров из временной корзины в постоянную при авторизации
        public static void MoveToUserCart(int userId)
        {
            if (TempCartItems.Count > 0)
            {
                foreach (var item in TempCartItems)
                {
                    CartManager.AddToCart(userId, item.BookId, item.Quantity);
                }

                // Очищаем временную корзину после перемещения
                TempCartItems.Clear();
            }
        }

    }
}
