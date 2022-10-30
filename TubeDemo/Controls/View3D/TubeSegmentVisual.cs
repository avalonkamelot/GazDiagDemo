using HelixToolkit.Wpf;
using System;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using TubeDemo.Data.Models;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using DocumentFormat.OpenXml.Drawing;

namespace TubeDemo.Controls.View3D
{
    internal class TubeSegmentVisual : CuttingPlaneGroup
    {
        protected TubeVisual3D? tube;

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool),typeof(TubeSegmentVisual));
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }


        public TubeSegmentVisual(TubeSegmentInfo info)
        {
            UpdateCuttingPlanes(info);
            CreateVisualFromModel(info);
            this.Children.Add(tube);
            info.PropertyChanged += Info_PropertyChanged;
        }

        private void Info_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TubeSegmentInfo.Angle)
                || e.PropertyName == nameof(TubeSegmentInfo.Hegth))
            {
                UpdateCuttingPlanes(sender as TubeSegmentInfo);
            }
            else if(e.PropertyName == nameof(TubeSegmentInfo.Width)
                || e.PropertyName == nameof(TubeSegmentInfo.Distance))
            {
                this.Children.Remove(tube);
                CreateVisualFromModel(sender as TubeSegmentInfo);
                this.Children.Add(tube);
                UpdateCuttingPlanes(sender as TubeSegmentInfo);

            }
        }

        protected void CreateVisualFromModel(TubeSegmentInfo? info)
        {
            if (info == null) throw new ArgumentNullException();

            tube = new TubeVisual3D()
            {
                Diameter = Viewport3D.TUBE_DIAMETER + 0.1,
                IsPathClosed = false,
                Path = GetSegmentPosition(info)
            };

            BindingOperations.SetBinding(tube, TubeVisual3D.MaterialProperty,
                new MultiBinding
                {
                    Converter = new DefectStateToMaterialConverter(),
                    Bindings =
                    {
                        new Binding(nameof(TubeSegmentInfo.IsDefect)) { Source = info, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged },
                        new Binding { Source = this,Path = new PropertyPath(IsSelectedProperty), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged },
                    },
                });

            //BindingOperations.SetBinding(tube, TubeVisual3D.MaterialProperty,
            //    new Binding
            //    {
            //        Path = new PropertyPath(nameof(TubeSegmentInfo.IsDefect)),
            //        Source = info,
            //        Mode = BindingMode.OneWay,
            //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //        Converter = new DefectStateToMaterialConverter()
            //    });
        }

        protected static Point3DCollection GetSegmentPosition(TubeSegmentInfo? tubeSegmentInfo)
        {
            var dist = tubeSegmentInfo?.Distance ?? 0;
            var width = Math.Max(tubeSegmentInfo?.Width ?? 0.1, 0.1);
            return new Point3DCollection() {
                    new Point3D(dist, 0,0),
                    new Point3D(dist + width, 0,0),
                };
        }
        

        public void UpdateCuttingPlanes(TubeSegmentInfo? tubeSegmentInfo)
        {
            if (tubeSegmentInfo == null) throw new ArgumentNullException();

            double hours = tubeSegmentInfo.Angle;
            double segments =Math.Min( Math.Max(tubeSegmentInfo.Hegth,.5), 12);

            double ang = 90;
            double diff = 0;

            if (segments <= 6)
            {
                diff = segments;
                Operation = CuttingOperation.Intersect;
            }
            else
            {
                ang = (segments - 9) * 30;
                diff = 12 - segments;
                Operation = CuttingOperation.Subtract;
            }

            CuttingPlanes.Clear();
            CuttingPlanes.Add(new Plane3D(new Point3D(0, 0, 0), Hours2Normal(hours, ang)));
            CuttingPlanes.Add(new Plane3D(new Point3D(0, 0, 0), Hours2Normal(hours + 6 + diff, ang)));
            IsEnabled = false;
            IsEnabled = true;
        }

        

        protected Vector3D Hours2Normal(double h, double offset)
        {
            while (h >= 12)
                h = h - 12;

            double angle = (360 / 12) * h + offset;
            double rad = (angle) * Math.PI / 180;
            return new Vector3D(0, -1 * Math.Sin(rad), Math.Cos(rad));
        }

        //[ValueConversion(typeof(SolidColorBrush), typeof(DiffuseMaterial))]
        //public class DefectStateToMaterialConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //    {
        //        return new DiffuseMaterial(((bool)value) ? new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)) : new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)));
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
        //}


        public class DefectStateToMaterialConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                bool isDefect = (bool)values[0];
                bool isSelected = (bool)values[1];

                Color color = isDefect ? Color.FromArgb(125, 255, 0, 0) : Color.FromArgb(125, 0, 0, 255);                
                if (isSelected)
                {
                    color.A = 255;
                }

                SolidColorBrush brush = new SolidColorBrush(color); 
                return new DiffuseMaterial(brush);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
        }
    }
}
