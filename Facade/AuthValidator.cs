using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AudioPlayer.Facade
{
    public class AuthValidator
    {
        public  bool ValidateLogin(string username, string password, string person)
        {
            string query = person == "Admin"
                ? "SELECT * FROM Admin_Login WHERE Adminname = @username AND Password = @password"
                : "SELECT * FROM User_LoginSignup WHERE Username = @username AND Password = @password";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0;
        }


        public  bool ValidateSignup(string username, string password)
        {
            try
            {
                string query = "INSERT INTO User_LoginSignup (Username, Password) VALUES (@username, @password)";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password)
                };
                DatabaseHelper.Instance.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
