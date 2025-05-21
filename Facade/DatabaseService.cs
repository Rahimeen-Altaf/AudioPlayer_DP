using AudioPlayer.Adapter.Database;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DatabaseService
{
    private readonly IDatabaseAdapter dbAdapter;

    public DatabaseService()
    {
        dbAdapter = SqlClientAdapter.getInstance();
    }

    public DataTable GetAllSongs()
    {
        return dbAdapter.ExecuteQuery("SELECT title FROM Songs");

    }

    public DataTable GetSongDetails(string title)
    {
        string query = "SELECT SongID, Title, Artist, Album, Duration, FilePath FROM Songs WHERE Title = @title";
        return dbAdapter.ExecuteQuery(query, new List<SqlParameter> {
            new SqlParameter("@title", title)
        });
    }

    public DataTable GetPlaylists()
    {
        string query = "SELECT PlaylistName FROM Playlists";
        return dbAdapter.ExecuteQuery(query);
    }

    public DataTable GetSongsInPlaylist(string playlistName)
    {
        string query = "SELECT Songs.SongID, Songs.Title FROM Songs " +
                       "INNER JOIN PlaylistSongs ON Songs.SongID = PlaylistSongs.SongID " +
                       "INNER JOIN Playlists ON PlaylistSongs.PlaylistID = Playlists.PlaylistID " +
                       "WHERE Playlists.PlaylistName = @playlistName";

        return dbAdapter.ExecuteQuery(query, new List<SqlParameter> {
            new SqlParameter("@playlistName", playlistName)
        });

    }
}
