using System.Collections.Generic;

namespace SharedListApi.Applications.SharedList
{
    public interface ISharedListsApplication
    {
        SharedList Create(SharedList list);
        void Delete(string id);
        IEnumerable<SharedList> Read(string listCollectionId, string listId = null);
        SharedList Update(SharedList list);
    }
}