using Newtonsoft.Json;
using SharedListApi.Applications.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SharedListApi.Applications.SharedList
{
    public class SharedListRepository : SqlRepository
    {
        public void Create(SharedList list)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertCommand, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = list.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = list.Name;
                    command.Parameters.Add("@created", SqlDbType.DateTime).Value = list.Created;
                    command.Parameters.Add("@category", SqlDbType.NChar, 100).Value = list.Category ?? "";
                    command.Parameters.Add("@listcollection", SqlDbType.NVarChar).Value = list.listCollectionId;
                    command.Parameters.Add("@Language", SqlDbType.NVarChar).Value = list.LanguageId;
                    command.Parameters.Add("@rows", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(list.Rows);
                    command.ExecuteNonQuery();
                }

                foreach (var row in list.Rows)
                {
                    using (SqlCommand insertRowommand = new SqlCommand(InsertRowCommand, connection))
                    {
                        insertRowommand.Parameters.Add("@id", SqlDbType.NVarChar).Value = row.Id;
                        insertRowommand.Parameters.Add("@fkListId", SqlDbType.NVarChar).Value = list.Id;

                        insertRowommand.ExecuteNonQuery();
                    }

                    foreach (var column in row.Columns)
                    {
                        using (SqlCommand insertColumnCommand = new SqlCommand(InsertColumnCommand, connection))
                        {
                            insertColumnCommand.Parameters.Add("@id", SqlDbType.NVarChar).Value = column.Id;
                            insertColumnCommand.Parameters.Add("@fkRowId", SqlDbType.NVarChar).Value = row.Id;
                            insertColumnCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = "string";
                            insertColumnCommand.Parameters.Add("@value", SqlDbType.NVarChar).Value = column.Content;

                            insertColumnCommand.ExecuteNonQuery();
                        }
                    }
                }




                connection.Close();
            }
        }

        public IEnumerable<SharedList> Read(string listCollectionId, string id, int skip, int take)
        {
            var list = new List<SharedList>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ListCommand(listCollectionId, id), connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SharedList
                        {
                            Id = ((string)reader["id"])?.Trim(),
                            Name = ((string)reader["name"])?.Trim(),
                            Created = (DateTime)reader["created"],
                            Category = (string)reader["category"],
                            listCollectionId = (string)reader["listcollection"],
                            LanguageId = (string)reader["language"],
                            Rows = JsonConvert.DeserializeObject<List<Row>>((string)reader["rows"]),
                            Deleted = (DateTime?)(reader["deleted"] == DBNull.Value ? null : reader["deleted"])
                        });
                    }
                }
                connection.Close();
            }

            return list;
        }

        internal void Delete(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DeleteCommand, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    command.Parameters.Add("@now", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        internal void Update(SharedList list)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateCommand, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = list.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = list.Name;
                    command.Parameters.Add("@created", SqlDbType.DateTime).Value = list.Created;
                    command.Parameters.Add("@category", SqlDbType.NChar, 100).Value = list.Category ?? "";
                    command.Parameters.Add("@listcollection", SqlDbType.NVarChar).Value = list.listCollectionId;
                    command.Parameters.Add("@Language", SqlDbType.NVarChar).Value = list.LanguageId;
                    command.Parameters.Add("@rows", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(list.Rows);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private string ListCommand(string listCollectionId, string id)
        {
            var command = "select * from List where deleted is null ";

            if (!string.IsNullOrEmpty(id))
            {
                command += $"and id='{id}' ";
            }

            if (!string.IsNullOrEmpty(listCollectionId))
            {
                command += $"and ListCollection='{listCollectionId}' ";
            }

            command += "order by created DESC";

            return command;
        }

        private string InsertCommand => $"INSERT INTO List (id, name, created, category, listcollection, Language, rows) VALUES (@id, @name, @created, @category, @listcollection, @Language, @rows)";

        private string InsertRowCommand => $"INSERT INTO Row (id, fkListId) VALUES (@id, @fkListId)";

        private string InsertColumnCommand => $"INSERT INTO Column (id, type, value, fkRowId) VALUES (@id, @type, @value, @fkRowId)";

        private string UpdateCommand => $"Update List set name=@name, created=@created, category=@category, listcollection=@listcollection, Language=@Language, rows=@rows WHERE id=@id";

        private string DeleteCommand => $"UPDATE List SET deleted=@now WHERE id=@id";

    }
}
