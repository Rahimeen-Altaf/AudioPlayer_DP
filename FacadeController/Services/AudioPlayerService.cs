using AxWMPLib;

public class AudioPlayerService
{
    private readonly AxWindowsMediaPlayer mediaPlayer;

    public AudioPlayerService(AxWindowsMediaPlayer player)
    {
        mediaPlayer = player;
    }

    public void Play(string filePath)
    {
        mediaPlayer.URL = filePath;
        mediaPlayer.Ctlcontrols.play();
    }

    public void Stop()
    {
        mediaPlayer.Ctlcontrols.stop();
    }
}
