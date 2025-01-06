using MySql.Data.MySqlClient; // Importing MySQL data library for database operations
using BackendAPI.Database; // Importing the backend API database namespace

// Class providing a wrapper for MySQL database operations, implementing the IDatabaseWrapper interface
public class MySqlDatabaseWrapper : IDatabaseWrapper
{
    private readonly string _connectionString; // Connection string for connecting to the database

    // Constructor to initialize the database wrapper with a connection string
    public MySqlDatabaseWrapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Executes a query and maps the result set to a list of objects of type T
    public List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map)
    {
        var results = new List<T>(); // List to store the mapped results
        using (var connection = new MySqlConnection(_connectionString)) // Create and open a new MySQL connection
        {
            connection.Open(); // Open the database connection
            using (var command = new MySqlCommand(query, connection)) // Prepare the SQL command
            using (var reader = command.ExecuteReader()) // Execute the command and get the result set
            {
                while (reader.Read()) // Iterate through the result set
                {
                    results.Add(map(reader)); // Map each row to an object of type T using the provided mapping function
                }
            }
        }
        return results; // Return the mapped results
    }

    // Executes a scalar query and returns a single value of type T
    public T ExecuteScalar<T>(string query, params MySqlParameter[] parameters)
    {
        using (var connection = new MySqlConnection(_connectionString)) // Create and open a new MySQL connection
        {
            connection.Open(); // Open the database connection
            using (var command = new MySqlCommand(query, connection)) // Prepare the SQL command
            {
                if (parameters != null) // Check if any parameters were provided
                {
                    command.Parameters.AddRange(parameters); // Add parameters to the command
                }

                var result = command.ExecuteScalar(); // Execute the scalar query and get the result
                return (T)Convert.ChangeType(result, typeof(T)); // Convert the result to type T and return it
            }
        }
    }

    // Executes a query with parameters and maps the result set to a list of objects of type T
    public List<T> GetEntitiesByQuery<T>(string query, Func<MySqlDataReader, T> map, params MySqlParameter[] parameters)
    {
        var results = new List<T>(); // List to store the mapped results
        using (var connection = new MySqlConnection(_connectionString)) // Create and open a new MySQL connection
        {
            connection.Open(); // Open the database connection
            using (var command = new MySqlCommand(query, connection)) // Prepare the SQL command
            {
                if (parameters != null && parameters.Length > 0) // Check if any parameters were provided
                {
                    command.Parameters.AddRange(parameters); // Add parameters to the command
                }

                using (var reader = command.ExecuteReader()) // Execute the command and get the result set
                {
                    while (reader.Read()) // Iterate through the result set
                    {
                        results.Add(map(reader)); // Map each row to an object of type T using the provided mapping function
                    }
                }
            }
        }
        return results; // Return the mapped results
    }
}
