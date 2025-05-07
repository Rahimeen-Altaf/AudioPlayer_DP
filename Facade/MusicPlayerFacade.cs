using AudioPlayer.Facade;
using AxWMPLib;
using System.Data;
using System.Windows.Forms;

public class MusicPlayerFacade
{
    private readonly AudioPlayerService audioService;
    private readonly DatabaseService dbService;

    public MusicPlayerFacade(AxWindowsMediaPlayer mediaPlayer)
    {
        audioService = new AudioPlayerService(mediaPlayer);
        dbService = new DatabaseService();
    }



    public void LoadSongs(ListBox lbSongs)
    {
        lbSongs.Items.Clear();
        var dt = dbService.GetAllSongs();
        ListBoxLoader.LoadFromDataTable(lbSongs, dt, "title");
    }

    public void PlaySongByTitle(string title)
    {
        var dt = dbService.GetSongDetails(title);
        if (dt.Rows.Count > 0)
        {
            string path = dt.Rows[0]["FilePath"].ToString();
            audioService.Play(path);
        }
    }

    public void LoadPlaylists(ListBox listBoxPlaylists)
    {
        listBoxPlaylists.Items.Clear();
        var dt = dbService.GetPlaylists();
        ListBoxLoader.LoadFromDataTable(listBoxPlaylists, dt, "PlaylistName");
    }

    public void LoadSongsFromPlaylist(string playlistName, ListBox listBoxSongs)
    {
        listBoxSongs.Items.Clear();
        var dt = dbService.GetSongsInPlaylist(playlistName);
        ListBoxLoader.LoadFromDataTable(listBoxSongs, dt, "Title");
    }
}
