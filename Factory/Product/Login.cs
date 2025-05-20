using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using AudioPlayer.Observer;
using AudioPlayer.Observer.Observers;

namespace AudioPlayer
{
    public partial class Login : Form
    {
        private string Person;
        //private ObserverManager _observerManager;
        //private OnlineObserver _onlineObserver;
        private MusicPlayerFacade playerFacade;
        public Login(string Person)
        {
            InitializeComponent();
            this.Person = Person;
            cmbPerson.Text = Person;

            playerFacade = new MusicPlayerFacade(null); // or pass actual mediaPlayer if needed


            //_observerManager = ObserverManager.Instance;
            //_onlineObserver = new OnlineObserver();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;

            // Trigger login validation
           bool isSuccess= playerFacade.ValidateLogin(username, password, Person);
            if (isSuccess)
            {
                HandleLoginSuccess();
            }
            else
            {
                HandleLoginFailure();
            }



        }

        public void HandleLoginSuccess()
        {
            MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //_observerManager.RegisterObserver(_onlineObserver);



            if (Person == "Admin")
            {
                AdminCRUD adminForm = new AdminCRUD(txtUsername.Text);
                adminForm.Show();
            }
            else if (Person == "User")
            {
                UserPanel userForm = new UserPanel(txtUsername.Text);
                

                userForm.Show();
            }

        }

        public void HandleLoginFailure()
        {
            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //private void txtConPass_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtPass.Text == txtConPass.Text)
        //    {
        //        btnLogin.Enabled = true;
        //        lblError.Text = "";
        //    }
        //    else
        //    {
        //        lblError.Text = "password does not match!";
        //        btnLogin.Enabled = false;
        //    }
        //}

        private void Login_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void lblLoginPage_Click(object sender, EventArgs e)
        {

        }

        private void cmbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}