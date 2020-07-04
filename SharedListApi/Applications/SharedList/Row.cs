using System;
using System.Collections.Generic;

namespace SharedListApi.Applications.SharedList
{
    public class Row
    {
        public string Text { get; set; }

        public bool Checked { get; set; }

        public List<Column> Columns { get; set; }
    }
}