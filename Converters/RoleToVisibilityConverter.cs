using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EkatBooks.Converters
{
    public class RoleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int roleId && parameter is string requiredRole)
            {
                if (int.TryParse(requiredRole, out int requiredRoleId))
                {
                    return roleId == requiredRoleId ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}