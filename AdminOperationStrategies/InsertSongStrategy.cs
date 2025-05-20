public class InsertSongStrategy : IAdminOperationStrategy
{

    public InsertSongStrategy()
    {
    }

    public void Execute(string username)
    {
        AudioPlayer.InsertSongForm insert = new AudioPlayer.InsertSongForm(username);
        insert.Show();
    }
}
