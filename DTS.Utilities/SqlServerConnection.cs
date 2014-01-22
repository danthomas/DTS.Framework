using System;
using System.Data.SqlClient;

namespace DTS.Utilities
{
    public class SqlServerConnection
    {
        private readonly string _server;
        private readonly string _database;
        
        public SqlServerConnection()
        { }

        public SqlServerConnection(string server, string database)
        {
            _server = server;
            _database = database;
        }

        public void ExecuteNonQuery(string format, params object[] args)
        {
            ExecuteNonQuery(_server, _database, format, args);
        }

        public static void ExecuteNonQuery(string server, string database, string format, params object[] args)
        {
            ExecuteInConnection(server, database, (connection) =>
            {
                using (SqlCommand command = new SqlCommand(format.FormatEx(args), connection))
                {
                    command.ExecuteNonQuery();
                }
            });
        }

        private static void ExecuteInConnection(string server, string database, Action<SqlConnection> action)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder { DataSource = server, InitialCatalog = database, IntegratedSecurity = true };

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                action(connection);
            }
        }
    }
}
