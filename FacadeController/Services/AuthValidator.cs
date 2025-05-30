using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AudioPlayer.Adapter.Database;

namespace AudioPlayer.Facade
{
    public class AuthValidator
    {
        private SqlClientAdapter dbAdapter;

        public AuthValidator()
        {
            dbAdapter = SqlClientAdapter.getInstance();
        }

        public bool ValidateLogin(string username, string password, string person)
        {
            string query = person == "Admin"
                ? "SELECT * FROM Admin_Login WHERE Adminname = @username AND Password = @password"
                : "SELECT * FROM User_LoginSignup WHERE Username = @username AND Password = @password";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            DataTable dt = dbAdapter.ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0;
        }

        public bool ValidateSignup(string username, string password, string email)
        {
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM User_LoginSignup WHERE Email = @email";
                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@email", email)
                };

                DataTable result = dbAdapter.ExecuteQuery(checkQuery, checkParams);
                int count = Convert.ToInt32(result.Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("An account with this email already exists!", "Signup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                string insertQuery = "INSERT INTO User_LoginSignup (Username, Password, Email) VALUES (@username, @password, @email)";
                var insertParams = new List<SqlParameter>
                {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password),
                    new SqlParameter("@email", email)
                };

                dbAdapter.ExecuteNonQuery(insertQuery, insertParams);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Signup failed!\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
