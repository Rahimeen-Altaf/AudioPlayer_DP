using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Observer
{
    public class LoginValidator : IObservable
    {
        private List<ILoginObserver> _observers = new List<ILoginObserver>();

        public void RegisterObserver(ILoginObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterObserver(ILoginObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(bool isValid)
        {
            foreach (var observer in _observers)
            {
                observer.Update(isValid);
            }
        }

        public void ValidateLogin(string username, string password, string person)
        {
            string query = person == "Admin"
                ? "SELECT * FROM Admin_Login WHERE Adminname = @username AND Password = @password"
                : "SELECT * FROM User_LoginSignup WHERE Username = @username AND Password = @password";

            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@username", username),
            new SqlParameter("@password", password)
        };

            DatabaseHelper dbHelper = DatabaseHelper.Instance;
            DataTable dataTable = dbHelper.ExecuteQuery(query, parameters);

            bool isValid = dataTable.Rows.Count > 0;
            NotifyObservers(isValid);
        }
    }

}
