using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using AudioPlayer.Observer;

namespace AudioPlayer
{
    public partial class Login : Form
    {
        private string Person;
        private LoginValidator _loginValidator;
        private LoginObserver _loginFormObserver;

        public Login(string Person)
        {
            InitializeComponent();
            this.Person = Person;
            cmbPerson.Text = Person;

            _loginValidator = new LoginValidator();
            _loginFormObserver = new LoginObserver(this); 
            _loginValidator.RegisterObserver(_loginFormObserver); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;

            // Trigger login validation
            _loginValidator.ValidateLogin(username, password, Person);

        }
       
        public void HandleLoginSuccess()
        {
            MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Person == "Admin")
            {
                AdminCRUD adminForm = new AdminCRUD(txtUsername.Text);
                adminForm.ShowDialog();
            }
            else if (Person == "User")
            {
                UserPanel userForm = new UserPanel(txtUsername.Text);
                

                userForm.ShowDialog();
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