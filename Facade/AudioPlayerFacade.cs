using AudioPlayer;
using AudioPlayer.Facade;
using AxWMPLib;
using System.Data;
using System.Windows.Forms;

public class MusicPlayerFacade
{
    private readonly AudioPlayerService audioService;
    private readonly DatabaseService dbService;
    private readonly ListBoxLoader listBoxLoader;
    private readonly AuthValidator authValidator;



    public MusicPlayerFacade(AxWindowsMediaPlayer mediaPlayer)
    {
        audioService = new AudioPlayerService(mediaPlayer);
        dbService = new DatabaseService();
        listBoxLoader= new ListBoxLoader();
        authValidator = new AuthValidator();
    }

    public void LoadSongs(ListBox lbSongs)
    {
        lbSongs.Items.Clear();
        var dt = dbService.GetAllSongs();
        listBoxLoader.LoadFromDataTable(lbSongs, dt, "title");
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

    public bool ValidateLogin(string username, string password, string person)
    {
        bool isSuccess= authValidator.ValidateLogin(username, password, person);
        if(isSuccess)
        {
            //obserber.add();
        }
        return isSuccess;

    }

    public bool Signup(string username, string password,string email)
    {
        return authValidator.ValidateSignup(username, password,email);
    }

    public void LoadPlaylists(ListBox listBoxPlaylists)
    {
        listBoxPlaylists.Items.Clear();
        var dt = dbService.GetPlaylists();
        listBoxLoader.LoadFromDataTable(listBoxPlaylists, dt, "PlaylistName");
    }

    public void LoadSongsFromPlaylist(string playlistName, ListBox listBoxSongs)
    {
        listBoxSongs.Items.Clear();
        var dt = dbService.GetSongsInPlaylist(playlistName);
        listBoxLoader.LoadFromDataTable(listBoxSongs, dt, "Title");
    }
}
