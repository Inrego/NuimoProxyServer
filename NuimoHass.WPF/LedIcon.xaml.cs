using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

namespace NuimoHass.WPF
{
    /// <summary>
    /// Interaction logic for LedIcon.xaml
    /// </summary>
    public partial class LedIcon : UserControl
    {
        public static readonly DependencyProperty MatrixProperty = DependencyProperty.Register(
            "Matrix", typeof(Shared.LedMatrix), typeof(LedIcon), new PropertyMetadata(default(Shared.LedMatrix), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            
        }

        public Shared.LedMatrix Matrix
        {
            get { return (Shared.LedMatrix) GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }
        public LedIcon()
        {
            InitializeComponent();
        }

    }
    [ValueConversion(typeof(string), typeof(Brush))]
    public class LedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            return val ? Brushes.Black : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    [ValueConversion(typeof(string), typeof(double))]
    public class LedPositionConverterY : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            if (val.Length < 5)
                return 0;
            var x = System.Convert.ToInt32(val.Substring(3, 1));
            return x*3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    [ValueConversion(typeof(string), typeof(double))]
    public class LedPositionConverterX : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            if (val.Length < 5)
                return 0;
            var x = System.Convert.ToInt32(val.Substring(4, 1));
            return x * 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
