
using AudioPlayer;

public class DeleteSongStrategy : IAdminOperationStrategy
{
    public void Execute(string username)
    {
        DeleteSong delete = new DeleteSong(username);
        delete.ShowDialog();
    }
}