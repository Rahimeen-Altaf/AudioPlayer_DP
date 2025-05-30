using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AudioPlayer.Adapter.Database
{
    public class SqlClientAdapter : DatabaseTarget
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

        public override DataTable ExecuteQuery(string query)
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

        public override DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
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

        public override void ExecuteNonQuery(string query, List<SqlParameter> parameters)
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

        public override List<string> GetUserEmailsFromDb()
        {
            string query = "SELECT Email FROM [AudioPlayer1].[dbo].[User_LoginSignup]";
            DataTable dt = ExecuteQuery(query); // removed the stray dot before ExecuteQuery

            List<string> emails = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string email = row["Email"].ToString();
                if (!string.IsNullOrEmpty(email))
                    emails.Add(email);
            }

            return emails;
        }
    }
}