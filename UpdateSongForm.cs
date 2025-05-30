using AudioPlayer.Adapter.Database;
using AudioPlayer.Observer.Observers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class UpdateSongForm : Form
    {
        private readonly SqlClientAdapter dbHelper;
        private readonly ObserverManager _observerManager = ObserverManager.Instance;
        private readonly CommonHelperFacadeController facade = new CommonHelperFacadeController(null);

        private DataTable dataTable;
        private string name;
        private string currentFormName = "UpdateSongForm";

        public UpdateSongForm()
        {
            InitializeComponent();
            this.dbHelper = SqlClientAdapter.getInstance();
        }

        public UpdateSongForm(string name)
        {
            InitializeComponent();
            this.dbHelper = SqlClientAdapter.getInstance();
            this.name = name;
        }

        private void UpdateSongForm_Load(object sender, EventArgs e)
        {
            ClsVisitedForms.VisitedForms.Add(name + " admin has visited " + currentFormName);
            ActivityLOG a = new ActivityLOG();
            string userVisited = string.Join(", ", ClsVisitedForms.VisitedForms);
            a.InsertActivityLog(name, currentFormName, userVisited);

            LoadSongs();
        }

        private void LoadSongs()
        {
            string query = "SELECT * FROM Songs";

            DataTable dataTable = dbHelper.ExecuteQuery(query);

            if (dataTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the SongID from the selected row
                int songID = Convert.ToInt32(selectedRow.Cells[0].Value);

                // Update the corresponding row in the database
                string updateQuery = "UPDATE Songs SET Title = @title, Artist = @artist, Album = @album, Duration = @duration, FilePath = @filePath WHERE SongID = @songID";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@title", txtTitle.Text),
                    new SqlParameter("@artist", txtArtist.Text),
                    new SqlParameter("@album", txtAlbum.Text),
                    new SqlParameter("@duration", Convert.ToInt32(txtDuration.Text)),
                    new SqlParameter("@filePath", txtFilePath.Text),
                    new SqlParameter("@songID", songID)
                };

                dbHelper.ExecuteNonQuery(updateQuery, parameters);


                MessageBox.Show("Song updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Notify all observers about the update
                List<string> emails = dbHelper.GetUserEmailsFromDb();
                _observerManager.RegisterObserver(new EmailObserver(emails, facade));

                string message = facade.BuildSongNotificationMessage(
                    "updated",
                    txtTitle.Text,
                    txtArtist.Text,
                    txtAlbum.Text,
                    Convert.ToInt32(txtDuration.Text));

                _observerManager.NotifyObservers(message);

                // Refresh the DataGridView to show updated data
                LoadSongs();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Please select a song to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearTextBoxes()
        {
            txtTitle.Clear();
            txtArtist.Clear();
            txtAlbum.Clear();
            txtDuration.Clear();
            txtFilePath.Clear();
        }

        private void btnSelectSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MPEG Audio Files|*.mp3;*.wav;*.wma|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected song file path
                string filePath = openFileDialog.FileName;
                txtFilePath.Text = filePath;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtSongID.Text = row.Cells[0].Value.ToString();
                txtTitle.Text = row.Cells[1].Value.ToString();
                txtArtist.Text = row.Cells[2].Value.ToString();
                txtAlbum.Text = row.Cells[3].Value.ToString();
                txtDuration.Text = row.Cells[4].Value.ToString();
                txtFilePath.Text = row.Cells[5].Value.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            ActivityLOG a = new ActivityLOG();
            a.ClearVisitedForms();
            Application.Exit();
        }
    }
}