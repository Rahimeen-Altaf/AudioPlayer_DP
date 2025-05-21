using AudioPlayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using AudioPlayer.Adapter.Database;

public class ActivityLOG
{
    private readonly SqlClientAdapter dbHelper;

    public ActivityLOG()
    {
        dbHelper = SqlClientAdapter.getInstance();
    }

    public void InsertActivityLog(string name, string currentFormName, string userVisited)
    {
        string query = "INSERT INTO ActivityLog (Name, DateTime, Form_Name, UserVisited) VALUES (@Name, @DateTime, @Form_Name, @UserVisited)";

        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@Name", name),
            new SqlParameter("@DateTime", DateTime.Now),
            new SqlParameter("@Form_Name", currentFormName),
            new SqlParameter("@UserVisited", userVisited)
        };

        dbHelper.ExecuteNonQuery(query, parameters);
    }

    public void ClearVisitedForms()
    {
        ClsVisitedForms.VisitedForms.Clear();
    }
}
