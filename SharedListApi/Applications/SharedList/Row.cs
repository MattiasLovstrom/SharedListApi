using System;
using System.Collections.Generic;

namespace SharedListApi.Applications.SharedList
{
    public class Row 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public IEnumerable<Column> Columns { get; set; }
    }
}