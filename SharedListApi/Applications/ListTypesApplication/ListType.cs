using System.Collections.Generic;

namespace SharedListApi.Applications.ListTypesApplication
{
    public class ListType
    {
        public string Name { get; internal set; }
        public string Id { get; internal set; }
        public IEnumerable<ColumnType> Columns { get; set; }
    }
}