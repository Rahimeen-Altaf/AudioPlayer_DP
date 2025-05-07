using System.Collections.Generic;
using System.Data.SqlClient;

public class SignUpValidator : ISignupObservable
{
    private List<ISignUpObserver> observers = new List<ISignUpObserver>();

    public void RegisterObserver(ISignUpObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(ISignUpObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(bool isSuccess)
    {
        foreach (var observer in observers)
        {
            observer.OnSignUpResult(isSuccess);
        }
    }

    public void ValidateSignup(string username, string password)
    {
        bool isSuccess = false;
        try
        {
            string query = "INSERT INTO User_LoginSignup (Username, Password) VALUES (@username, @password)";

            List<SqlParameter> parameters = new List<SqlParameter>
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

        NotifyObservers(isSuccess); 
    }
}
