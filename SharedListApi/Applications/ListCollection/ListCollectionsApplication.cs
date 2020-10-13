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

        public ListCollection Create(string name, string type)
        {
            var newCollection = new ListCollection()
            {
                Id = CreateId(name),
                Name = name,
                Type = type
            };
            new ListCollectionRepository().Create(newCollection);
            

            return newCollection;
        }

        public void Delete(string id)
        {
            new ListCollectionRepository().Delete(id);
        }

        public IEnumerable<ListCollection> List(string id = null, int skip = 0, int take = 100)
        {
            return new ListCollectionRepository().Read(id, skip, take);
        }
    }
}
