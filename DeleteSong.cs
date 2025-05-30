using AudioPlayer.Adapter.Database;
using AudioPlayer.Observer.Observers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class DeleteSong : Form
    {
        private readonly SqlClientAdapter dbHelper = SqlClientAdapter.getInstance();
        private readonly ObserverManager _observerManager = ObserverManager.Instance;
        private readonly CommonHelperFacadeController _facadeController = new CommonHelperFacadeController(null);

        private DataTable dataTable;
        private string name;
        private string currentFormName = "DeleteSongForm";
        public DeleteSong()
        {
            InitializeComponent();
            SqlClientAdapter dbHelper = SqlClientAdapter.getInstance();
        }
        public DeleteSong(string name)
        {
            InitializeComponent();
            SqlClientAdapter dbHelper = SqlClientAdapter.getInstance();
            this.name = name;
        }

        private void DeleteSong_Load(object sender, EventArgs e)
        {
            ClsVisitedForms.VisitedForms.Add(name + " admin has visited " + currentFormName);
            ActivityLOG a = new ActivityLOG();
            string userVisited = string.Join(", ", ClsVisitedForms.VisitedForms);
            a.InsertActivityLog(name, currentFormName, userVisited);
         
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            LoadSongs();
        }
        private void LoadSongs()
        {
            dataTable = _facadeController.getSongs();
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int songID = Convert.ToInt32(selectedRow.Cells[0].Value);
                string title = selectedRow.Cells[1].Value.ToString();
                string artist = selectedRow.Cells[2].Value.ToString();
                string album = selectedRow.Cells[3].Value.ToString();
                int duration = Convert.ToInt32(selectedRow.Cells[4].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this song?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string deleteQuery = "DELETE FROM Songs WHERE SongID = @songID";
                    List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@songID", songID)
            };

                    dbHelper.ExecuteNonQuery(deleteQuery, parameters);

                    // 🔥 Observer: Notify via Email
                    List<string> emails = dbHelper.GetUserEmailsFromDb();
                    var emailObserver = new EmailObserver(emails, _facadeController);
                    _observerManager.RegisterObserver(emailObserver);

                    string message = _facadeController.BuildSongNotificationMessage("deleted", title, artist, album, duration);
                    _observerManager.NotifyObservers(message);

                    // UI updates
                    LoadSongs();
                    MessageBox.Show("Song deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            ActivityLOG a = new ActivityLOG();
            a.ClearVisitedForms();
            Application.Exit();
        }
    }
}
