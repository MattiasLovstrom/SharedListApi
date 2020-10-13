using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedListApi.Applications.ListTypesApplication
{
    public class ListTypesApplication : IListTypesApplication
    {
        public ListType Create(ListType listType)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListType> Read(string id, int skip, int take)
        {
            return new List<ListType> {
                new ListType
                {
                    Id = "list",
                    Name ="list",
                    Columns = new List<ColumnType>{
                        new ColumnType
                        {
                            Type = "done"
                        },
                        new ColumnType
                        {
                            Type = "text"
                        }
                    }
                },
                new ListType
                {
                    Id = "traning",
                    Name ="training",
                    Columns = new List<ColumnType>{
                        new ColumnType
                        {
                            Type = "text"
                        },
                        new ColumnType
                        {
                            Type = "weight"
                        },
                        new ColumnType
                        {
                            Type = "sets"
                        },
                        new ColumnType
                        {
                            Type = "reps"
                        }
                    }
                },
                new ListType
                {
                    Id = "calories",
                    Name ="calories",
                    Columns = new List<ColumnType>{
                        new ColumnType
                        {
                            Type = "text"
                        },
                        new ColumnType
                        {
                            Type = "calory"
                        },
                        new ColumnType
                        {
                            Type = "protine"
                        }
                    }
                }
            };
        }

        public ListType Update(ListType listType)
        {
            throw new NotImplementedException();
        }
    }
}
