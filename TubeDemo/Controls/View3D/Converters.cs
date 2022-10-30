using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TubeDemo.Controls.View3D
{
    [ValueConversion(typeof(double), typeof(Point3DCollection))]
    public class TubeLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Point3DCollection))
                throw new InvalidOperationException("The target must be a Point3DCollection");

            return new Point3DCollection() {
                new Point3D(0,0,0),
                new Point3D((double)value, 0,0),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }

    [ValueConversion(typeof(SolidColorBrush), typeof(DiffuseMaterial))]
    public class BrushToMaterialConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return new DiffuseMaterial(value as SolidColorBrush);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }
}
