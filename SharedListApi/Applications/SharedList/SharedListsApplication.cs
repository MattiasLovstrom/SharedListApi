using SharedListApi.Applications.Cache;
using SharedListApi.Applications.ListCollection;
using System;
using System.Collections.Generic;

namespace SharedListApi.Applications.SharedList
{
    public class SharedListsApplication : ISharedListsApplication
    {
        private readonly ICacheApplication _cache;

        public SharedListsApplication(ICacheApplication cacheApplicaion)
        {
            _cache = cacheApplicaion;
        }

        public SharedList Create(SharedList list)
        {
            var created = DateTime.UtcNow;

            if (list.Created.Year <= 1)
            {
                list.Created = created;
            }

            list.Id = ListCollectionsApplication.CreateId(list.Name, created);
            new SharedListRepository().Create(list);
            _cache.Add<SharedList>(CreateCacheKey(list.Id), list);

            return list;
        }

        public IEnumerable<SharedList> Read(string listCollectionId, string id = null, int skip = 0, int take = 10)
        {
            if (id != null)
            {
                var cached = _cache.Get<SharedList>(CreateCacheKey(id));
                if (cached != null) return new List<SharedList> { cached }; 
            }


            
            List<SharedList> documents = new List<SharedList>();
            foreach(var document in new SharedListRepository().Read(listCollectionId, id, skip, take))
            {
                if (_cache.Get<string>(CreateDeletedCacheKey(document.Id)) != null) continue;
                var cached = _cache.Get<SharedList>(CreateCacheKey(document.Id));
                documents.Add(cached != null ? cached : document);
            }

            return documents;
        }

        public SharedList Update(SharedList list)
        {
            new SharedListRepository().Update(list);

            _cache.Add<SharedList>(CreateCacheKey(list.Id), list);

            return list;
        }

        public void Delete(string id)
        {
            new SharedListRepository().Delete(id);

            _cache.Add<string>(CreateDeletedCacheKey(id), id);
        }

        private string CreateDeletedCacheKey(string id)
        {
            return "Deleted_" + id;
        }

        private string CreateCacheKey(string id)
        {
            return id;
        }
    }
}
