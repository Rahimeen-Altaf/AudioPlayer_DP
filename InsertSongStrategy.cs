using AudioPlayer;


public class InsertSongStrategy : IAdminOperationStrategy
{
    public void Execute(string username)
    {
        InsertSongForm insert = new InsertSongForm(username);
        insert.ShowDialog();
    }
}