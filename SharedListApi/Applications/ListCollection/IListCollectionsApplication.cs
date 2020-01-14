using System.Collections.Generic;

namespace SharedListApi.Applications.ListCollection
{
    public interface IListCollectionsApplication
    {
        ListCollection Create(string name);
        IEnumerable<ListCollection> List();
        void Delete(string id);
    }
}