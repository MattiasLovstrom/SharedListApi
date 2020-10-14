using System.Collections.Generic;

namespace SharedListApi.Applications.ListTypesApplication
{
    public class ColumnType
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<Interval> Intervals { get; set; }
    }
}