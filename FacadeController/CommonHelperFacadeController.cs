

using AudioPlayer.Facade;
using AudioPlayer.Services;
using AxWMPLib;
using System.Data;
using System.Windows.Forms;

public class CommonHelperFacadeController
{
    private readonly AudioPlayerService audioService;
    private readonly DatabaseService dbService;
    private readonly ListBoxLoader listBoxLoader;
    private readonly AuthValidator authValidator;
    private readonly EmailService emailService;

    public CommonHelperFacadeController(AxWindowsMediaPlayer mediaPlayer)
    {
        audioService = new AudioPlayerService(mediaPlayer);
        dbService = new DatabaseService();
        listBoxLoader = new ListBoxLoader();
        authValidator = new AuthValidator();
        emailService = new EmailService();
    }

    // Load all songs into a ListBox
    public void LoadSongs(ListBox lbSongs)
    {
        lbSongs.Items.Clear();
        var dt = dbService.GetAllSongs();
        listBoxLoader.LoadFromDataTable(lbSongs, dt, "title");
    }

    // Play song by title 
    public void PlaySongByTitle(string title)
    {
        var dt = dbService.GetSongDetails(title);
        if (dt.Rows.Count > 0)
        {
            string path = dt.Rows[0]["FilePath"].ToString();
            audioService.Play(path);
        }
    }

    public DataTable getSongs()
    {
        return dbService.GetSongs();
    }

    // Load all playlists
    public void LoadPlaylists(ListBox lbPlaylists)
    {
        lbPlaylists.Items.Clear();
        var dt = dbService.GetPlaylists();
        listBoxLoader.LoadFromDataTable(lbPlaylists, dt, "PlaylistName");
    }

    // Load songs from a playlist
    public void LoadSongsFromPlaylist(string playlistName, ListBox lbSongs)
    {
        lbSongs.Items.Clear();
        var dt = dbService.GetSongsInPlaylist(playlistName);
        listBoxLoader.LoadFromDataTable(lbSongs, dt, "Title");
    }

    // Validate user login
    public bool ValidateLogin(string username, string password, string person)
    {
        return authValidator.ValidateLogin(username, password, person);
    }

    // Validate signup
    public bool Signup(string username, string password, string email)
    {
        return authValidator.ValidateSignup(username, password, email);
    }

    // Send email notifications
    public void SendEmail(string toEmail, string subject, string message)
    {
        emailService.SendEmail(toEmail, subject, message);
    }

    // Build a notification message about song operations
    public string BuildSongNotificationMessage(string operation, string title, string artist, string album, int duration)
    {
        return $"🎵 A song has been {operation} in the Audio Player!\n\n" +
               $"📌 Title: {title}\n" +
               $"🎤 Artist: {artist}\n" +
               $"💿 Album: {album}\n" +
               $"⏱️ Duration: {duration} sec\n" +
               $"Stay tuned! 🎧";
    }
}
