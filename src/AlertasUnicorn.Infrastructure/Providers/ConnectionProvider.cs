using MySql.Data.MySqlClient;

namespace AlertasUnicorn.Infrastructure.Providers
{
    public class ConnectionProvider
    {
        private readonly string _connectionString;
        public ConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public void Dispose()
        {

        }
    }
}
