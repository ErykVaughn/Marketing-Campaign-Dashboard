using MySql.Data.MySqlClient;
using BackendAPI.Database;

public class MySqlDatabaseWrapper : IDatabaseWrapper
{
    private readonly string _connectionString;

    public MySqlDatabaseWrapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map)
    {
        var results = new List<T>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(map(reader));
                }
            }
        }
        return results;
    }

    public T ExecuteScalar<T>(string query, params MySqlParameter[] parameters)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var result = command.ExecuteScalar();
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }
    }

    public List<T> GetEntitiesByQuery<T>(string query, Func<MySqlDataReader, T> map, params MySqlParameter[] parameters)
    {
        var results = new List<T>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(map(reader));
                    }
                }
            }
        }
        return results;
    }
}
