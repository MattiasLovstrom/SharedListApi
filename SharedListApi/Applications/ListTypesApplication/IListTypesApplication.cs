using System.Collections.Generic;

namespace SharedListApi.Applications.ListTypesApplication
{
    public interface IListTypesApplication
    {
        IEnumerable<ListType> Read(string id, int skip, int take);
        ListType Create(ListType listType);
        ListType Update(ListType listType);
        void Delete(string id);
    }
}