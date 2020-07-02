using System;
using System.Collections.Generic;
using Nest;

namespace SharedListApi.Applications.SharedList
{
    [ElasticsearchType(RelationName = "sharedlist")]
    public class SharedList
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Row> Rows = new List<Row>();
        [Keyword]
        public string listCollectionId { get; set; }

        public string LanguageId { get; set; }
        public DateTime? Deleted { get; internal set; }
    }
}