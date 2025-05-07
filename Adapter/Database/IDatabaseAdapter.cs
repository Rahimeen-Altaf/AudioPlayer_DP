using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Adapter.Database
{
    public interface IDatabaseAdapter
    {
        DataTable ExecuteQuery(string query);
        DataTable ExecuteQuery(string query, List<SqlParameter> parameters);
        void ExecuteNonQuery(string query, List<SqlParameter> parameters);
    }

}
