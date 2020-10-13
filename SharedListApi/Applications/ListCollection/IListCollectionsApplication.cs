using System.Collections.Generic;

namespace SharedListApi.Applications.ListCollection
{
    public interface IListCollectionsApplication
    {
        ListCollection Create(string name, string type);
        IEnumerable<ListCollection> List(string id = null, int skip = 0, int take = 10);
        void Delete(string id);
    }
}