using Nest;

namespace SharedListApi.Applications.ListCollection
{
    [ElasticsearchType(RelationName = "listcollection")]

    public class ListCollection
    {
        [Keyword]
        public string Id { get; set; }
        [Keyword]
        public string Name { get; set; }
    }
}