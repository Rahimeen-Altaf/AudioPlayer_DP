using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class AdminCRUD : Form
    {

        private string currentFormName = "AdminCRUD";
        private string name;

        public AdminCRUD()
        {
            InitializeComponent();


        }
        public AdminCRUD(string name)
        {
            InitializeComponent();
            this.name = name;

        }


        private void cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdminOperationContext context = new AdminOperationContext();

            switch (cmbOperation.SelectedItem.ToString())
            {
                case "Insert Song":
                    context.SetStrategy(new InsertSongStrategy());

                    break;
                case "Delete Song":
                    context.SetStrategy(new DeleteSongStrategy());
                    break;
                case "Update Song":
                    context.SetStrategy(new UpdateSongStrategy());
                    break;
                default:
                    MessageBox.Show("Please Select A Valid Operation.", "Operation Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            context.ExecuteStrategy(name);
        }


        private void AdminCRUD_Load(object sender, EventArgs e)
        {
            ClsVisitedForms.VisitedForms.Add(name + " admin has visited " + currentFormName);
            ActivityLOG a = new ActivityLOG();
            string userVisited = string.Join(", ", ClsVisitedForms.VisitedForms);
            a.InsertActivityLog(name, currentFormName, userVisited);
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            ActivityLOG a = new ActivityLOG();
            a.ClearVisitedForms();
            Application.Exit();
        }
    }
}
