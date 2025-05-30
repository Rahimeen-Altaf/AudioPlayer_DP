using AudioPlayer.Observer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer.Observer.Observers
{
    public class UIObserver : IObserver
    {
        private ListBox _songsListBox;
        private CommonHelperFacadeController facade;

        public UIObserver(ListBox songsListBox, CommonHelperFacadeController _facade)
        {
            _songsListBox = songsListBox;
            facade = _facade;
        }
        public void Update(string message)
        {
            MessageBox.Show($"Notification: {message}", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (_songsListBox.InvokeRequired)
            {
                _songsListBox.Invoke(new Action(() => facade.LoadSongs(_songsListBox)));
            }
            else
            {
                facade.LoadSongs(_songsListBox);
            }
        }
    }
}
