using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;
using TubeDemo.Data.Repository;

namespace TubeDemo.Controls.View3D
{
    public class Viewport3DVM : DependencyObject, INotifyPropertyChanged
    {

        public static readonly DependencyProperty DefaultDiameterProperty = DependencyProperty.Register("DefaultDiameter",
            typeof(double), typeof(Viewport3DVM), new FrameworkPropertyMetadata(2.0));
        public double DefaultDiameter
        {
            get => (double)GetValue(DefaultDiameterProperty);
            set => SetValue(DefaultDiameterProperty, value);
        }

        public static readonly DependencyProperty DefaultLengthProperty = DependencyProperty.Register("DefaultLength",
            typeof(double), typeof(Viewport3DVM), new FrameworkPropertyMetadata(20.0));
        public double DefaultLength
        {
            get => (double)GetValue(DefaultLengthProperty);
            set => SetValue(DefaultLengthProperty, value);
        }


        public static readonly DependencyProperty DefaultTubeBrushProperty = DependencyProperty.Register("DefaultTubeBrush",
            typeof(SolidColorBrush), typeof(Viewport3DVM), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(120, 0, 0, 0))));
        public SolidColorBrush DefaultTubeBrush
        {
            get => (SolidColorBrush)GetValue(DefaultTubeBrushProperty);
            set => SetValue(DefaultTubeBrushProperty, value);
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        public ObservableCollection<TubeSegmentInfo> Items { get; protected set; } = new ObservableCollection<TubeSegmentInfo>();

        
    }
}
