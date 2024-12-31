using MySql.Data.MySqlClient;
namespace BackendAPI.Database
{
    public interface IDatabaseWrapper
    {
        List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map);
        T ExecuteScalar<T>(string query, params MySqlParameter[] parameters);
        List<T> GetEntitiesByQuery<T>(string query, Func<MySqlDataReader, T> map, params MySqlParameter[] parameters);
    }
}
