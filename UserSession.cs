using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkatBooks
{
    // Создайте статический класс для хранения данных сессии
    public static class UserSession
    {
        // Создайте статический класс для хранения данных сессии
        
            private static string sessionFilePath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "EkatBooks", "session.dat");

            public static int CurrentUserId { get; set; } = -1;
            public static bool IsLoggedIn => CurrentUserId > 0;

            // Методы для сохранения и загрузки сессии
            public static void SaveSession()
            {
                try
                {
                    // Создаем директорию, если она не существует
                    string directory = System.IO.Path.GetDirectoryName(sessionFilePath);
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }

                    // Сохраняем ID в файл
                    System.IO.File.WriteAllText(sessionFilePath, CurrentUserId.ToString());
                }
                catch (Exception)
                {
                    // Обработка ошибок записи файла
                }
            }

            public static void LoadSession()
            {
                try
                {
                    if (System.IO.File.Exists(sessionFilePath))
                    {
                        string idText = System.IO.File.ReadAllText(sessionFilePath);
                        if (int.TryParse(idText, out int userId))
                        {
                            CurrentUserId = userId;
                        }
                    }
                }
                catch (Exception)
                {
                    CurrentUserId = -1;
                }
            }

            public static void ClearSession()
            {
                CurrentUserId = -1;
                try
                {
                    if (System.IO.File.Exists(sessionFilePath))
                    {
                        System.IO.File.Delete(sessionFilePath);
                    }
                }
                catch (Exception)
                {
                    // Обработка ошибок удаления файла
                }
            }
        }
    }


