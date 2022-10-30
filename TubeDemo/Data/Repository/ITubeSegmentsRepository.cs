using System.Collections.Generic;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;

namespace TubeDemo.Data.Repository
{
    public interface ITubeSegmentsRepository : IRepository
    {
        IEnumerable<TubeSegmentInfo> GetAll(out string errMessage);
        void Truncate(out string errMessage);
        bool Add(TubeSegmentInfo item, out string errMessage);
        bool Remove(TubeSegmentInfo obj, out string errMessage);
    }
}
