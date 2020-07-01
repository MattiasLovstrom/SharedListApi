using SharedListApi.Applications.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SharedListApi.Applications.Languages
{
    public class LanguageRepository : SqlRepository
    {
        public IEnumerable<Language> Read(string id, int skip, int take)
        {
            var list = new List<Language>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ListCommand(id), connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Language(
                            ((string)reader["id"])?.Trim(),
                            ((string)reader["name"])?.Trim()));
                    }
                }
                connection.Close();
            }

            return list;
        }

        public string ListCommand(string id)
        {
            var command = "select * from Language ";

            if (!string.IsNullOrEmpty(id))
            {
                command = $"{command} where id='{id}'";
            }

            return command;
        }
    }
}
