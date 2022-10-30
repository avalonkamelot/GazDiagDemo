using CsvHelper;
using CsvHelper.Excel;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;

namespace TubeDemo.Data.Contexts
{
    public class XlsContext : IDataContext
    {
        protected Stream datastream;

        public XlsContext(Stream data)
        {
            datastream = data;
        }

        public List<TubeSegmentInfo> ReadAll()
        {
            using (var reader = new CsvReader(new ExcelParser(datastream, CultureInfo.CurrentCulture)))
            {
                return reader.GetRecords<TubeSegmentInfo>().ToList();
            }
        }
    }
}
