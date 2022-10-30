using TubeDemo.Data.Models;

namespace TubeDemo.Data.Interfaces
{
    public enum DataAction
    {
        Added,
        Deleted,
        Truncated
    }

    public class DataChangedEventArgs
    {
        public DataAction DataAction { get; set; }
        public IModel? Item { get; set; }

        public DataChangedEventArgs(DataAction dataAction, IModel? item)
        {
            DataAction = dataAction;
            Item = item;
        }
    }
}
