using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using TubeDemo.Data.Interfaces;

namespace TubeDemo.Data.Models
{
    public class TubeSegmentInfo : IModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion



        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        private double _distance;
        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private double _angle;
        public double Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                OnPropertyChanged(nameof(Angle));
            }
        }

        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _height;
        public double Hegth
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Hegth));
            }
        }

        private bool _isDefect;
        [BooleanTrueValues("yes")]
        [BooleanFalseValues("no")]
        public bool IsDefect
        {
            get => _isDefect;
            set
            {
                _isDefect = value;
                OnPropertyChanged(nameof(IsDefect));
            }
        }

        
    }
}
