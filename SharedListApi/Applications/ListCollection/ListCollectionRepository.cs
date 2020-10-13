using SharedListApi.Applications.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SharedListApi.Applications.ListCollection
{
    public class ListCollectionRepository : SqlRepository
    {
        public void Create(ListCollection listCollection)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertCommand, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = listCollection.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = listCollection.Name;
                    command.Parameters.Add("@type", SqlDbType.NVarChar).Value = listCollection.Type;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public IEnumerable<ListCollection> Read(string id, int skip, int take)
        {
            var list = new List<ListCollection>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ListCommand(id), connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ListCollection
                        {
                            Id = GetString(reader, "id"),
                            Name = GetString(reader, "name"),
                            Type = GetString(reader, "type")
                        });
                    }
                }
                connection.Close();
            }

            return list;
        }

        public static string GetString(SqlDataReader reader, string name)
        {
            var dbValue = reader[name] as string;
            if (dbValue == null || dbValue is DBNull) return null;

            return dbValue.Trim();
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

        private string ListCommand(string id)
        {
            var command = "select * from ListCollection where deleted is null ";

            if (!string.IsNullOrEmpty(id))
            {
                command = $"{command} and id='{id}'";
            }

            return command;
        }

        private string InsertCommand => $"INSERT INTO ListCollection (id, name, type) VALUES (@id, @name, @type)";

        private string DeleteCommand => $"UPDATE ListCollection SET deleted=@now WHERE id=@id";
    }
}
