using System.Globalization;


namespace Ui.ValueConverters
{
    public class IsIncomeColorValueConverter : IValueConverter
    {
        private readonly Color Important = Colors.WhiteSmoke;
        private readonly Color Default = Colors.LightGreen;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value ==  1)
            {
                return Important;
            }
            return Default;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
