using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Ui.Desktop
{
    public class IntToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte correctedValue = (byte)(255 - (int)value);

            //byte byteValue;
            //if (byte.TryParse((string)value, out byteValue))
            //    return new SolidColorBrush(Color.FromRgb(byteValue, byteValue, byteValue));
            //else
            //    return new SolidColorBrush();
            
            return new SolidColorBrush(Color.FromRgb(correctedValue, correctedValue, correctedValue));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
