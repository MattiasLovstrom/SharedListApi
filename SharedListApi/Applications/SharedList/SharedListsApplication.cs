using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SharedListApi.Applications.SharedList
{
    public class SharedListsApplication : ISharedListsApplication
    {
        private List<SharedList> _sharedLists = new List<SharedList>();

        public SharedList Create(SharedList list)
        {
            var created = DateTime.UtcNow;
            list.Id = HttpUtility.UrlEncode(list.Name + created.ToString("-yyyyMMdd-HHmmss"));
            list.Created = created;
            _sharedLists.Add(list);

            return list;
        }

        public IEnumerable<SharedList> Read(string listCollectionId, string id = null)
        {
            return _sharedLists.Where(
                x => x.listCollectionId == listCollectionId
                && (id == null || x.Id == id));
        }

        public SharedList Update(SharedList list)
        {
            Delete(list.Id);
            _sharedLists.Add(list);

            return list;
        }

        public void Delete(string id)
        {
            _sharedLists.Remove(_sharedLists.Where(x => x.Id == id).Single());
        }
    }
}
