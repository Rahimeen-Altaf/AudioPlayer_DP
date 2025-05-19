using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using AudioPlayer.Observer;
using AudioPlayer.Observer.Interfaces;
using AudioPlayer.Observer.Enums;

public class ObserverManager : IObservable
{
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(ObserverType type, bool isSuccess)
    {
        foreach (var observer in observers)
        {
            observer.Update(type, isSuccess);
        }
    }

    public void Validate(ObserverType type, string username, string password, string person = "")
    {
        bool isSuccess = false;

        if (type == ObserverType.Login)
        {
            string query = person == "Admin"
                ? "SELECT * FROM Admin_Login WHERE Adminname = @username AND Password = @password"
                : "SELECT * FROM User_LoginSignup WHERE Username = @username AND Password = @password";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            DataTable dataTable = DatabaseHelper.Instance.ExecuteQuery(query, parameters);
            isSuccess = dataTable.Rows.Count > 0;
        }
        else if (type == ObserverType.Signup)
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
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
        }

        NotifyObservers(type, isSuccess);
    }
}
