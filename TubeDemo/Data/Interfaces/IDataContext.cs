using System.Collections.Generic;
using TubeDemo.Data.Models;

namespace TubeDemo.Data.Interfaces
{
    public interface IDataContext
    {
        List<TubeSegmentInfo> ReadAll();
    }
}
