using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

public class DatabaseHelper
{
    private static readonly DatabaseHelper _instance = new DatabaseHelper();

    private readonly string connectionString;

    // Make the constructor private
    private DatabaseHelper()
    {
        connectionString = "Data Source=DESKTOP-HHUT6HH\\SQLEXPRESS;Initial Catalog=RAH;Integrated Security=True;Encrypt=False";
    }

    public static DatabaseHelper Instance => _instance;

    public DataTable ExecuteQuery(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
    }

    public DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
    }

    public void ExecuteNonQuery(string query, List<SqlParameter> parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null && parameters.Count > 0)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
