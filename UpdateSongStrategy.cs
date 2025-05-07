using AudioPlayer.Forms; // 👈 change this to match your actual form namespace

public class UpdateSongStrategy : IAdminOperationStrategy
{
    public void Execute(string username)
    {
        AudioPlayer.UpdateSongForm update = new UpdateSongForm(username);
        update.ShowDialog();
    }
}