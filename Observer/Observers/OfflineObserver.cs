using AudioPlayer.Observer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer.Observer.Observers
{
    public class OfflineObserver : IObserver
    {
        private List<string> offlineUserEmails;

        public OfflineObserver(List<string> offlineEmails)
        {
            offlineUserEmails = offlineEmails;
        }

        public void Update(string message)
        {
            // Right now, just show a simple MessageBox for demo
            // Later you can do email sending or other logic here
            string emails = string.Join(", ", offlineUserEmails);
            MessageBox.Show($"Offline users notified ({offlineUserEmails.Count}): {emails}\nMessage: {message}", "Offline Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
