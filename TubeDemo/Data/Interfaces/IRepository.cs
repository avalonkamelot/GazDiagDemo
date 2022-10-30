using System;

namespace TubeDemo.Data.Interfaces
{
    public interface IRepository
    {
        event EventHandler<DataChangedEventArgs>? ItemsChanged;

    }
}
