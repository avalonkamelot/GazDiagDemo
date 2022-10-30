using System;
using System.Collections.Generic;
using System.Linq;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;

namespace TubeDemo.Data.Repository
{
    public class TubeSegmentsRepository : ITubeSegmentsRepository
    {
        protected List<TubeSegmentInfo> storage = new List<TubeSegmentInfo>();

        public event EventHandler<DataChangedEventArgs>? ItemsChanged;

        public TubeSegmentsRepository(IDataContext dataCtx)
        {
            this.storage = dataCtx.ReadAll();
        }

        public IEnumerable<TubeSegmentInfo> GetAll(out string errMessage)
        {
            errMessage = String.Empty;
            return storage.AsEnumerable();
        }

        public void Truncate(out string errMessage)
        {
            errMessage = String.Empty;
            storage.Clear();
            ItemsChanged?.Invoke(this, new DataChangedEventArgs(DataAction.Truncated, null));
        }

        public bool Add(TubeSegmentInfo item, out string errMessage)
        {
            errMessage = String.Empty;
            storage.Add(item);
            ItemsChanged?.Invoke(this, new DataChangedEventArgs(DataAction.Added, item));
            return true;
        }

        public bool Remove(TubeSegmentInfo item, out string errMessage)
        {
            errMessage = String.Empty;
            storage.Remove(item);
            ItemsChanged?.Invoke(this, new DataChangedEventArgs(DataAction.Deleted, item));
            return true;
        }

        public void SetMock()
        {
            string errMessage;

            Add(new TubeSegmentInfo() { Distance = 2, Angle=0, Width=3,Hegth=2, IsDefect=true, Name= "Коррозия" }, out errMessage);
            Add(new TubeSegmentInfo() { Distance = 12, Angle = 2, Width = 2, Hegth = 2, IsDefect = false, Name = "Тест" }, out errMessage);
            //Add(new TubeSegmentInfo() { Distance = 2 }, out errMessage);
            //Add(new TubeSegmentInfo() { Distance = 3, Name = "Верт.шов" }, out errMessage);
        }
    }
}
