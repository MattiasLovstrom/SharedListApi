using System;

namespace SharedListApi.Applications.SharedList
{
    public class Column
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}