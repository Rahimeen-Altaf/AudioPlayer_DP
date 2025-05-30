using System;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Collections;
using WMPLib;
using AxWMPLib;
using AudioPlayer.Observer.Observers;


namespace AudioPlayer
{
    public partial class UserPanel : Form
    {
        private string currentFormName = "UserPanel";
        private CommonHelperFacadeController facade;
        private UIObserver onlineObserver;
        private readonly ObserverManager _observerManager = ObserverManager.Instance;
        string name;
        public UserPanel()
        {
            InitializeComponent();
            facade = new CommonHelperFacadeController(axWindowsMediaPlayer1);
            facade.LoadPlaylists(listBoxPlaylists);
        }
        public UserPanel(string username) : this()

        {
            name = username;
            facade = new CommonHelperFacadeController(axWindowsMediaPlayer1);
            onlineObserver = new UIObserver(lbSongs, facade);
            _observerManager.RegisterObserver(onlineObserver);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbluser.Text = name + "'s Profile";
            ClsVisitedForms.VisitedForms.Add(name + " user has visited " + currentFormName);
            string userVisited = string.Join(", ", ClsVisitedForms.VisitedForms);
            ActivityLOG a = new ActivityLOG();
            a.InsertActivityLog(name, currentFormName, userVisited);
            facade.LoadPlaylists(listBoxPlaylists);
        }

        private void btnSongs_Click(object sender, EventArgs e)
        {
            facade.LoadSongs(lbSongs);
        }

        private WaveOut waveOut;
        private AudioFileReader audioFileReader;
        private Timer timer;


        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if (audioFileReader != null && waveOut != null)
        //    {
        //        // Update the TrackBar position based on the current playback position
        //        tkbSongs.Value = (int)(audioFileReader.Position / (audioFileReader.Length / (double)tkbSongs.Maximum));
        //    }
        //}



        private DataTable dataTable;
        private string selectedSongFilePath;
        private void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedItem != null)
                facade.PlaySongByTitle(lbSongs.SelectedItem.ToString());
        }

       



        // Define a custom Song class to store the song information

        private void listBoxPlaylists_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBoxPlaylists.SelectedIndex >= 0)
            {
                string selectedPlaylist = listBoxPlaylists.SelectedItem.ToString();

                var facade = new CommonHelperFacadeController(axWindowsMediaPlayer1);  // Assuming axWindowsMediaPlayer1 is passed
                facade.LoadSongsFromPlaylist(selectedPlaylist, listBoxSongs);
            }
        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBoxPlaylists.SelectedItem != null)
                facade.LoadSongsFromPlaylist(listBoxPlaylists.SelectedItem.ToString(), listBoxSongs);
            // Clear existing rows in the dataGridView
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            ActivityLOG a = new ActivityLOG();
            a.ClearVisitedForms();
            Application.Exit();
        }
    }
}
