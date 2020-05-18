using Nest;
using SharedListApi.Applications.Search;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharedListApi.Applications.ListCollection
{
    public class ListCollectionsApplication : IListCollectionsApplication
    {
        public static Dictionary<char, char> _replace = new Dictionary<char, char>
        {
            { 'å','a' },
            { 'ä','a' },
            { 'ö','o' }
        };
        public static string CreateId(string name, DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.UtcNow;
            }

            name = name.ToLower();
            foreach (var replace in _replace)
            {
                name = name.Replace(replace.Key, replace.Value);
            }
            name = Regex.Replace(name, @"[^a-zA-Z0-9]", "_");
            name = name + dateTime.Value.ToString("_yyyyMMdd_HHmmss");

            return name;
        }

        public ListCollection Create(string name)
        {
            var newCollection = new ListCollection()
            {
                Id = CreateId(name),
                Name = name
            };
            var response = SearchClient.Instance.IndexDocument(newCollection);
            if (!response.IsValid)
            {
                throw new ApplicationException(response.ServerError.Error.Reason);
            }

            return newCollection;
        }

        public void Delete(string id)
        {
            var response = SearchClient.Instance.Delete<ListCollection>(id);
            if (!response.IsValid)
            {
                throw new ApplicationException(response.ServerError.Error.Reason);
            }
        }

        public IEnumerable<ListCollection> List(string id = null, int skip = 0, int take = 100)
        {

            var searchRequest = new SearchRequest
            {
                From = skip,
                Size = take,
                Sort = new List<ISort>
                {
                    new FieldSort{Field="id", Order=SortOrder.Ascending}
                }
            };

            if (id != null)
            {
                searchRequest.Query = new TermQuery
                {
                    Field = "id",
                    Value = id
                };
            }
            else
            {
                searchRequest.Query = new MatchAllQuery();
            }

            var ret = SearchClient.Instance.Search<ListCollection>(searchRequest)
                .Documents;

            return ret;
        }
    }
}
