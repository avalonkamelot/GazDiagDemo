using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TubeDemo.Data.Interfaces;
using TubeDemo.Data.Models;

namespace TubeDemo.Data.Contexts
{
    public class CsvContext : IDataContext
    {
        static CsvConfiguration config = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };
        protected Stream datastream;


        public CsvContext(Stream data)
        {
            datastream = data;
        }

        public List<TubeSegmentInfo> ReadAll()
        {
            using (var reader = new StreamReader(datastream))
            using (var csv = new CsvReader(reader, config))
                return csv.GetRecords<TubeSegmentInfo>().ToList();

        }
    }
}
