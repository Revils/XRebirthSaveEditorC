using System.Globalization;
using System.Windows.Data;
using System;

namespace XRebirthSaveEditorC
{
    public class DateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dtDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDate = dtDate.AddSeconds(System.Convert.ToDouble(value));
            return dtDate.ToString(culture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime valueDate = DateTime.Parse(System.Convert.ToString(value), culture);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (int) Math.Round((valueDate.Ticks - dateTime.Ticks) / 10000000.0);
        }
    }
}