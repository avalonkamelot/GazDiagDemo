using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using TubeDemo.Data.Models;

namespace TubeDemo.Controls.View3D
{
    /// <summary>
    /// Interaction logic for Viewport3D.xaml
    /// </summary>
    public partial class Viewport3D : UserControl
    {
        public const double TUBE_DIAMETER = 2.0;
        public const double TUBE_DEFAULT_LENGTH = 20.0;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable),
                typeof(Viewport3D), new PropertyMetadata(null));


        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(TubeSegmentInfo),
                typeof(Viewport3D), new PropertyMetadata(null, (s, e) => 
                {
                    Visual3D obj;
                    if(e.OldValue!=null && (s as Viewport3D).segments.Visuals.TryGetValue(e.OldValue, out obj))
                    {
                        (obj as TubeSegmentVisual).IsSelected = false;
                    }
                    if (e.NewValue != null && (s as Viewport3D).segments.Visuals.TryGetValue(e.NewValue, out obj))
                    {
                        (obj as TubeSegmentVisual).IsSelected = true;
                    }

                }));
        public TubeSegmentInfo SelectedItem
        {
            get => (TubeSegmentInfo)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty DefaultDiameterProperty = DependencyProperty.Register("DefaultDiameter",
            typeof(double), typeof(Viewport3D), new FrameworkPropertyMetadata(TUBE_DIAMETER));
        public double DefaultDiameter
        {
            get => (double)GetValue(DefaultDiameterProperty);
            set => SetValue(DefaultDiameterProperty, value);
        }

        public static readonly DependencyProperty DefaultLengthProperty = DependencyProperty.Register("DefaultLength",
            typeof(double), typeof(Viewport3D), new FrameworkPropertyMetadata(TUBE_DEFAULT_LENGTH));
        public double DefaultLength
        {
            get => (double)GetValue(DefaultLengthProperty);
            set => SetValue(DefaultLengthProperty, value);
        }


        public static readonly DependencyProperty DefaultTubeBrushProperty = DependencyProperty.Register("DefaultTubeBrush",
            typeof(SolidColorBrush), typeof(Viewport3D), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(25, 0, 0, 0))));
        public SolidColorBrush DefaultTubeBrush
        {
            get => (SolidColorBrush)GetValue(DefaultTubeBrushProperty);
            set => SetValue(DefaultTubeBrushProperty, value);
        }

        //public Viewport3DVM ViewModel { get; private set; } = new Viewport3DVM();

        public Viewport3D()
        {
            //DataContext = ViewModel;
            DataContext = this;
            InitializeComponent();
        }

        
    }
}
