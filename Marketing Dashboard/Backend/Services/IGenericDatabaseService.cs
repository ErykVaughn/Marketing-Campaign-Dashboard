using MySql.Data.MySqlClient;
using BackendAPI.Services;  // Ensure this is present in your service files


public interface IGenericDatabaseService
{
    List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map);
    T ExecuteScalar<T>(string query, params MySqlParameter[] parameters);
    List<T> GetEntitiesByQuery<T>(string query, Func<MySqlDataReader, T> map, params MySqlParameter[] parameters);
}

