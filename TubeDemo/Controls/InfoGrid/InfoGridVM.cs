using System.Collections.ObjectModel;
using System.ComponentModel;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;
using TubeDemo.Data.Repository;
using TubeDemo.Utils;

namespace TubeDemo.Controls.InfoGrid
{

    public class InfoGridVM : INotifyPropertyChanged
    {
        public InfoGridVM()
        {
            
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion


        private ITubeSegmentsRepository? _repository;
        public ITubeSegmentsRepository? Repository
        {
            get => _repository;
            set
            {
                if(_repository!=null)
                {
                    _repository.ItemsChanged -= _repository_ItemsChanged;
                }
                _repository = value;
                if (_repository != null)
                {
                    _repository.ItemsChanged += _repository_ItemsChanged;
                }
                Refresh();
                OnPropertyChanged(nameof(Repository));
            }
        }

        private void _repository_ItemsChanged(object? sender, DataChangedEventArgs e)
        {
            if (e.Item is TubeSegmentInfo item)
            {
                if (e.DataAction == DataAction.Deleted)
                {
                    Items.Remove(item);
                }
                else if (e.DataAction == DataAction.Added)
                {
                    Items.Add(item);
                }
                else if (e.DataAction == DataAction.Truncated)
                {
                    Items.Clear();
                }
            }
        }

        public ObservableCollection<TubeSegmentInfo> Items { get; protected set; } = new ObservableCollection<TubeSegmentInfo>();

        private RelayCommand? deleteCommand;
        public RelayCommand DeleteCommand => deleteCommand ?? (deleteCommand = new RelayCommand((o) => Delete(o as TubeSegmentInfo)));

        private RelayCommand? refreshCommand;
        public RelayCommand RefreshCommand => refreshCommand ?? (refreshCommand = new RelayCommand((o) => Refresh()));


        private TubeSegmentInfo? _selectedItem;
        public TubeSegmentInfo? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        bool _isLoadData;
        public bool IsLoadData
        {
            get => _isLoadData;
            set
            {
                _isLoadData = value;
                OnPropertyChanged(nameof(IsLoadData));
            }
        }

        protected void Refresh()
        {
            IsLoadData = false;
            string errMessage = string.Empty;
            var res = Repository?.GetAll(out errMessage);
            Items.Clear();
            if (res != null && string.IsNullOrEmpty(errMessage))
            {
                foreach (var x in res)
                {
                    Items.Add(x);
                }
            }
            IsLoadData = true;
        }


        protected void Delete(TubeSegmentInfo? row)
        {
            if (row != null)
            {
                string errMessage = string.Empty;
                var res = Repository?.Remove(row, out errMessage);
            }
        }
    }
}
