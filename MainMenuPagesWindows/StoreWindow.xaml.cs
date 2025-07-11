using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EkatBooks
{
    /// <summary>
    /// Логика взаимодействия для StoreWindow.xaml
    /// </summary>
    public partial class StoreWindow : Page
    {
        public StoreWindow()
        {
            InitializeComponent();
            // Инициализация карты при загрузке страницы
            Loaded += MapPage_Loaded;
        }
        private void MapPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Настройка провайдера карт
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapControl.MapProvider = OpenStreetMapProvider.Instance;

            // Настройка параметров карты
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 17;
            mapControl.Zoom = 13;
            mapControl.ShowCenter = false; // Скрыть маркер центра
            mapControl.DragButton = System.Windows.Input.MouseButton.Left; // Перетаскивание левой кнопкой мыши

            // Установка начальной позиции (Екатеринбург)
            mapControl.Position = new PointLatLng(56.82961030791585, 60.533907359890335);

            // Добавление маркеров магазинов
            AddStoreMarkers();
        }

        private void AddStoreMarkers()
        {
            // Создаем слой для маркеров
            GMapMarker marker1 = CreateMarker(new PointLatLng(56.83275066675518, 60.52411193277747), "Главный магазин");
           

            // Добавляем маркеры на карту
            mapControl.Markers.Add(marker1);
        
        }
        private void MapControl_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            // Помечаем событие как обработанное, чтобы оно не передавалось дальше в ScrollViewer
            e.Handled = true;

            // Вручную изменяем масштаб карты
            if (e.Delta > 0)
            {
                // Увеличение масштаба при прокрутке вверх
                mapControl.Zoom = Math.Min(mapControl.MaxZoom, mapControl.Zoom + 0.5);
            }
            else
            {
                // Уменьшение масштаба при прокрутке вниз
                mapControl.Zoom = Math.Max(mapControl.MinZoom, mapControl.Zoom - 0.5);
            }
        }
        private GMapMarker CreateMarker(PointLatLng position, string title)
        {
            // Создаем маркер
            GMapMarker marker = new GMapMarker(position)
            {
                Shape = new Ellipse
                {
                    Width = 15,
                    Height = 15,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                    Fill = Brushes.DarkRed,
                    ToolTip = title
                },
                Offset = new Point(-7.5, -7.5)
            };

            return marker;
        }

    }
}
