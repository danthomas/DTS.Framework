using System;
using System.Data;
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
            ExecuteInConnection((connection, cmdText) =>
            {
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.ExecuteNonQuery();
                }
            }, server, database, format, args);
        }

        public static DataSet ExecuteDataSet(string server, string database, string format, params object[] args)
        {
            DataSet dataSet = new DataSet();

            ExecuteInConnection((connection, cmdText) =>
            {
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                }
            }, server, database, format, args);

            return dataSet;
        }

        private static void ExecuteInConnection(Action<SqlConnection, string> action, string server, string database, string format, params object[] args)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder { DataSource = server, InitialCatalog = database, IntegratedSecurity = true };

            using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                string script = format.FormatEx(args);

                foreach (string cmdText in script.Split(new[] { "GO" + Environment.NewLine, Environment.NewLine + "GO" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    action(connection, cmdText);
                }
            }
        }
    }
}
