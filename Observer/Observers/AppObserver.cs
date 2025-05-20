using AudioPlayer;
using System.Windows.Forms;
using System;
using AudioPlayer.Observer.Interfaces;

public class AppObserver : IObserver
{
    public void Update(string message)
    {
        MessageBox.Show($"📣 Notification: {message}", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }
}