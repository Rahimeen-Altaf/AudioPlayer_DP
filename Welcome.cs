using AudioPlayer.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AudioPlayer
{
    public partial class Welcome : Form

    {

        private IFormFactory formFactory;


        public Welcome()
        {
            InitializeComponent();
        }

        private void cmbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPerson.SelectedItem.ToString() == "Admin Login")
            {
                btnSignUp.Enabled = false;
            }
            else if (cmbPerson.SelectedItem.ToString() == "User Login")
            {
                btnSignUp.Enabled = true;
            }
            else { }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbPerson.SelectedItem == null)
            {
                MessageBox.Show("Please Select Your Role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string role = cmbPerson.SelectedItem.ToString();
            string formType = (role == "Admin Login") ? "AdminLogin" : "UserLogin";
            if(formType== "AdminLogin")
            {
                formFactory = new ConcreteAdminLoginForm();
                Form loginForm = formFactory.CreateForm();
                loginForm.Show();

            }else if(formType == "UserLogin")
            {
                formFactory = new ConcreteUserLoginForm();
                Form loginForm = formFactory.CreateForm();
                loginForm.Show();
            }else
            {
                MessageBox.Show("Invalid Role");
                    return;
            }

        }
        private void btnSignUp_Click(object sender, EventArgs e)
        {

            formFactory = new ConcreteSignUpForm();
            Form signUpForm = formFactory.CreateForm();
            signUpForm.Show();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            btnSignUp.Enabled = false;
        }
    }

}


//ALTER TABLE[RAH].[dbo].[User_LoginSignup]
//ADD
//    [Status] VARCHAR(10) DEFAULT 'Offline',
//    [Email] VARCHAR(100);
