using AudioPlayer;

public class UpdateSongStrategy : IAdminOperationStrategy
{
    public void Execute(string username)
    {
   UpdateSongForm update = new UpdateSongForm(username);
        update.Show();
    }
}