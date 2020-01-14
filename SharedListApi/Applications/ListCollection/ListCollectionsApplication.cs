using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SharedListApi.Applications.ListCollection
{
    public class ListCollectionsApplication : IListCollectionsApplication
    {
        private List<ListCollection> _listsCollections = new List<ListCollection>();

        public ListCollection Create(string name)
        {
            var newCollection = new ListCollection()
            {
                Id = HttpUtility.UrlEncode(name + DateTime.UtcNow.ToString("-yyyyMMdd-HHmmss")),
                Name = name
            };
            _listsCollections.Add(newCollection);

            return newCollection;
        }

        public void Delete(string id)
        {
            _listsCollections.Remove(_listsCollections.Where(x => x.Id == id).FirstOrDefault());
        }

        public IEnumerable<ListCollection> List()
        {
            return _listsCollections;
        }
    }
}
