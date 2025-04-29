using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM_Antipub.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                int totalHours = (int)ts.TotalHours;
                return $"{totalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
            }
            return "00:00:00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Опционально: реализовать, если нужно редактирование
            return Binding.DoNothing;
        }
    }
}
