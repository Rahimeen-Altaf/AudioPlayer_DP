using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Adapter.Database
{
    public class DatabaseHelperAdapter : IDatabaseAdapter
    {
        private readonly DatabaseHelper dbHelper = DatabaseHelper.Instance;

        public DataTable ExecuteQuery(string query)
        {
            return dbHelper.ExecuteQuery(query);
        }

        public void ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            dbHelper.ExecuteNonQuery(query, parameters);
        }
        public DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            return dbHelper.ExecuteQuery(query,parameters);
        }

    }

}
