using Nest;
using System;

namespace SharedListApi.Applications.Search
{
    public class SearchClient
    {
        public static ElasticClient Instance
        {
            get
            {
#if DEBUG
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
#else
                var settings = new ConnectionSettings(new Uri("http://10.1.1.5:9200"))
#endif
                    .DefaultIndex("sharedlistindex")
                        .DefaultMappingFor<ListCollection.ListCollection>(
                            m => m.IndexName("listcollection1")
                                  .IdProperty(x => x.Id))
                        .DefaultMappingFor<SharedList.SharedList>(
                            m => m.IndexName("sharedlist")
                                  .IdProperty(x => x.Id));
                var client = new ElasticClient(settings);

                // Remap an index 
                // change the index name, then reindex all old documents to the new index
                // delete the old index
                // curl - X POST "localhost:9200/_reindex?pretty" - H "Content-Type: application/json" - d "{\"source\": {\"index\": \"listcollection\"},\"dest\": {\"index\": \"listcollection1\"}}"


                //client.Indices.Create("sharedlist", c => c.Map<SharedList.SharedList>(m => m.AutoMap<SharedList.SharedList>()));
                //client.Indices.Create("listcollection1", c => c.Map<ListCollection.ListCollection>(m => m.AutoMap<ListCollection.ListCollection>()));

                return client;
            }
        }

        public void IndexDocument()
        {

        }
    }
}
