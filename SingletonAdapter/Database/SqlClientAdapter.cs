using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AudioPlayer.Adapter.Database
{
    public class SqlClientAdapter : IDatabaseAdapter
    {
        private static SqlClientAdapter _instance;

        private readonly string connectionString;

        private SqlClientAdapter()
        {
            connectionString = "Data Source=R-A-H\\SQLEXPRESS;Initial Catalog=AudioPlayer1;Integrated Security=True";
        }

        public static SqlClientAdapter getInstance()
        {
                if (_instance == null)
                {
                    _instance = new SqlClientAdapter();
                }
                return _instance;
        }

        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                command.ExecuteNonQuery();
            }
        }
    }
}