using System;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using System.Reflection;

namespace EkatBooks.Converters
{
    public class ImageAuthorPathConverter : IValueConverter
    {
        // Относительный путь к папке с изображениями авторов (от корня проекта)
        private readonly string _relativePath = Path.Combine("authors");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string imageName)
            {
                // Получаем путь к исполняемому файлу (обычно bin/Debug/netX.Y)
                string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // Поднимаемся на 3 уровня вверх (из bin/Debug/netX.Y в корень проекта)
                if (baseDirectory != null)
                {
                    string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
                    string fullPath = Path.Combine(projectDirectory, _relativePath, imageName);

                    // Проверяем, существует ли файл (опционально)
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
            return value; // Обратное преобразование не требуется
        }
    }
}