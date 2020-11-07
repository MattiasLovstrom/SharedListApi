using System;
using System.Collections.Generic;
using System.Linq;

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
            var types = new List<ListType> {
                new ListType
                {
                    Id = "list",
                    Name ="list",
                    Columns = new List<ColumnType>{
                        new ColumnType 
                        { 
                            Id = "done",
                            Type = "done"
                        },
                        new ColumnType
                        {
                            Id = "text",
                            Type = "text"
                        }
                    }
                },
                new ListType
                {
                    Id = "traning",
                    Name ="traning",
                    Columns = new List<ColumnType>{
                        new ColumnType
                        {
                            Type = "text",
                            Id = "text"
                        },
                        new ColumnType
                        {
                            Type = "number",
                            Id = "weight",
                            Intervals = new List<Interval>
                            {
                                new Interval {Start = 1, Stop = 9, Step = 1},
                                new Interval {Start = 10, Stop = 150, Step = 10}
                            }
                        },
                        new ColumnType
                        {
                            Type = "number",
                            Id = "sets",
                            Intervals = new List<Interval>
                            {
                                new Interval {Start = 1, Stop = 10, Step = 1}
                            }
                        },
                        new ColumnType
                        {
                            Type = "number",
                            Id = "reps",
                            Intervals = new List<Interval>
                            {
                                new Interval {Start = 1, Stop = 20, Step = 1}
                            }
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
                            Id = "text",
                            Type = "text"
                        },
                        new ColumnType
                        {
                            Type = "number",
                            Id = "carbohydrates",
                            Intervals = new List<Interval>
                            {
                                new Interval {Start = 50, Stop = 1000, Step = 50}
                            }
                        },
                        new ColumnType
                        {
                            Type = "number",
                            Id = "protein", 
                            Intervals = new List<Interval>
                            {
                                new Interval {Start = 1, Stop = 100, Step = 1}
                            }
                        }
                    }
                }
            };

            if (id == null)
            {
                return types;
            }

            return types.Where(x => x.Id == id);
        }

        public ListType Update(ListType listType)
        {
            throw new NotImplementedException();
        }
    }

    public class Interval
    {
        public int Start { get; set; }
        public int Stop { get; set; }
        public int Step { get; set; }
    }
}
