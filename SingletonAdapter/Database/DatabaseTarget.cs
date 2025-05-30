using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AudioPlayer.Adapter.Database
{
    public class DatabaseTarget
    {
        public virtual DataTable ExecuteQuery(string query)
        {
            Console.WriteLine("Default ExecuteQuery called.");
            return new DataTable();
        }

        public virtual DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            Console.WriteLine("Default ExecuteQuery with parameters called.");
            return new DataTable();
        }

        public virtual void ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            Console.WriteLine("Default ExecuteNonQuery called.");
        }

        public virtual List<string> GetUserEmailsFromDb()
        {
            Console.WriteLine("Default GetUserEmailsFromDb called.");
            return new List<string>();
        }
    }
}
