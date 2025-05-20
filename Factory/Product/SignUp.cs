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
using System.Xml.Linq;

namespace AudioPlayer
{
    public partial class SignUp : Form
    {
        private MusicPlayerFacade playerFacade;


        public SignUp()
        {
            InitializeComponent();
            playerFacade = new MusicPlayerFacade(new AxWMPLib.AxWindowsMediaPlayer());

        }

        private void txtConPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text == txtConPass.Text)
            {
                btnSignIn.Enabled = true;
                lblError.Text = "";
            }
            else
            {
                lblError.Text = "Password does not match! ";
                btnSignIn.Enabled = false;
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;
            string email= txtEmail.Text;


            bool isSuccess = playerFacade.Signup(username, password,email);
            if (isSuccess)
            {
                MessageBox.Show("User added successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Login loginForm = new Login("User");
                loginForm.ShowDialog();
            }else
            {
                txtUsername.Text = "";
                txtPass.Text = "";
                txtConPass.Text = "";
                txtEmail.Text = "";

            }
          
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
           

            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignUp_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

    }

}
