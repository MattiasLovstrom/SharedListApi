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
                            Id = ((string)reader["id"])?.Trim(),
                            Name = ((string)reader["name"])?.Trim()
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
                using (SqlCommand command = new SqlCommand(InsertCommand, connection))
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

        private string InsertCommand => $"INSERT INTO ListCollection (id, name) VALUES (@id, @name)";

        private string DeleteCommand => $"UPDATE ListCollection SET deleted=@now WHERE id=@id)";

    }
}
