using System;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using System.Reflection;

namespace EkatBooks.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        // Относительный путь от корня проекта
        private readonly string _relativePath = Path.Combine("books");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string imageName)
            {
                // Получаем путь к директории исполняемого файла
                string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // В режиме отладки путь может указывать на bin/Debug, поэтому поднимаемся на несколько уровней вверх
                // Это зависит от структуры вашего проекта
                if (baseDirectory != null)
                {
                    // Поднимаемся на 3 уровня вверх из bin/Debug/netX.Y
                    string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;

                    // Комбинируем с относительным путем
                    string fullPath = Path.Combine(projectDirectory, _relativePath, imageName);

                    // Проверяем существование файла (опционально)
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // Не нужно преобразовывать обратно
        }
    }
}