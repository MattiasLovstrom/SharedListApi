using Nest;
using SharedListApi.Applications.Cache;
using SharedListApi.Applications.ListCollection;
using SharedListApi.Applications.Search;
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
            
            var response = SearchClient.Instance.IndexDocument(list);
            if (!response.IsValid)
            {
                throw new ApplicationException(response.ServerError.Error.Reason);
            }

            _cache.Add<SharedList>(CreateCacheKey(list.Id), list);

            return list;
        }

        public IEnumerable<SharedList> Read(string listCollectionId, string id = null, int skip = 0, int take = 10)
        {
            var musts = new List<QueryContainer> {
                new QueryContainer(new TermQuery
                    {
                        Field = "listCollectionId",
                        Value = listCollectionId
                     })
            };

            if (id != null)
            {
                var cached = _cache.Get<SharedList>(CreateCacheKey(id));
                if (cached != null) return new List<SharedList> { cached }; 
                musts.Add(new QueryContainer(new TermQuery
                {
                    Field = "id",
                    Value = id
                }));
            }

            var query = new BoolQuery();
            query.Must = musts;

            var searchRequest = new SearchRequest
            {
                From = 0,
                Size = 1000,
                Query = query,
                Sort = new List<ISort>
                {
                    new FieldSort{Field="created", Order=SortOrder.Descending}
                }
            };

            List<SharedList> documents = new List<SharedList>();
            foreach(var document in SearchClient.Instance.Search<SharedList>(searchRequest).Documents)
            {
                if (_cache.Get<string>(CreateDeletedCacheKey(document.Id)) != null) continue;
                var cached = _cache.Get<SharedList>(CreateCacheKey(document.Id));
                documents.Add(cached != null ? cached : document);
            }

            return documents;
        }

        public SharedList Update(SharedList list)
        {
            var response = SearchClient.Instance.Update<SharedList>(list.Id, u => u.Index("sharedlist").Doc(list));
            if (!response.IsValid)
            {
                throw new ApplicationException(response.ServerError.Error.Reason);
            }

            _cache.Add<SharedList>(CreateCacheKey(list.Id), list);

            return list;
        }

        public void Delete(string id)
        {
            var response = SearchClient.Instance.Delete<SharedList>(id);
            if (!response.IsValid)
            {
                throw new ApplicationException(response.ServerError.Error.Reason);
            }
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
