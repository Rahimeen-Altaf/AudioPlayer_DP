public class InsertSongStrategy : IAdminOperationStrategy
{
    public void Execute(string username)
    {
        AudioPlayer.InsertSongForm insert = new AudioPlayer.InsertSongForm(username);
        insert.ShowDialog();
    }
}