using MySql.Data.MySqlClient;
using BackendAPI.Database; // Add this at the top

namespace BackendAPI.Services


{
    public class GenericDatabaseService : IGenericDatabaseService
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public GenericDatabaseService(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }

        public List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map)
        {
            return _databaseWrapper.ExecuteQuery(query, map);
        }

        public T ExecuteScalar<T>(string query, params MySqlParameter[] parameters)
        {
            return _databaseWrapper.ExecuteScalar<T>(query, parameters);
        }

        public List<T> GetEntitiesByQuery<T>(string query, Func<MySqlDataReader, T> map, params MySqlParameter[] parameters)
        {
            return _databaseWrapper.GetEntitiesByQuery(query, map, parameters);
        }
    }
}
