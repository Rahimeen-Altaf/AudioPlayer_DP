using AudioPlayer;
using System;
using System.Windows.Forms;

public class SignUpObserver : ISignUpObserver
{
    private SignUp _signUpForm;

    public SignUpObserver(SignUp signUpForm)
    {
        _signUpForm = signUpForm;
    }

    public void OnSignUpResult(bool isSuccess)
    {
        if (isSuccess)
        {
            MessageBox.Show("User added successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login("User");
            loginForm.ShowDialog();
        }
        else
        {
            MessageBox.Show("Something went wrong, 😢 Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
