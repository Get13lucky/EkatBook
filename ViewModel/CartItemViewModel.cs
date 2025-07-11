using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EkatBooks
{

    public class CartItemViewModel : INotifyPropertyChanged
    {
        private int quantity;

        // Уникальный идентификатор элемента корзины
        public int CartItemId { get; set; }

        // Идентификатор книги
        public int BookId { get; set; }

        // Название книги
        public string Title { get; set; }

        // Цена за единицу товара
        public decimal Price { get; set; }

        // Количество товара в корзине
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged(nameof(Quantity));  // Уведомляем об изменении количества
                    OnPropertyChanged(nameof(TotalPrice)); // Пересчитываем и уведомляем об изменении общей цены
                }
            }
        }

        // Путь к изображению обложки книги
        public string CoverImage { get; set; }

        // Общая цена (количество * цена за единицу)
        public decimal TotalPrice => Price * Quantity;

        // Событие для уведомления об изменениях свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для уведомления об изменениях
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
