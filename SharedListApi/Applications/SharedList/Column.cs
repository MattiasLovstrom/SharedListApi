namespace SharedListApi.Applications.SharedList
{
    public class Column
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Id { get; internal set; }
    }
}