using AudioPlayer.Observer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer.Observer.Observers
{
    public class OnlineObserver : IObserver
    {
        private ListBox _songsListBox;
        private MusicPlayerFacade _playerFacade;

        public OnlineObserver(ListBox songsListBox, MusicPlayerFacade playerFacade)
        {
            _songsListBox = songsListBox;
            _playerFacade = playerFacade;
        }
        public void Update(string message)
        {
            MessageBox.Show($"📣 Notification: {message}", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (_songsListBox.InvokeRequired)
            {
                _songsListBox.Invoke(new Action(() => _playerFacade.LoadSongs(_songsListBox)));
            }
            else
            {
                _playerFacade.LoadSongs(_songsListBox);
            }
        }
    }
}
