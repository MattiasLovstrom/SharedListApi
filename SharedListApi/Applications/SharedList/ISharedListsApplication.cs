using System.Collections.Generic;

namespace SharedListApi.Applications.SharedList
{
    public interface ISharedListsApplication
    {
        SharedList Create(SharedList list);
        void Delete(string id);
        IEnumerable<SharedList> Read(string listCollectionId, string listId = null, int skip = 0, int take = 10);
        SharedList Update(SharedList list);
    }
}