using AudioPlayer.Observer.Enums;
using AudioPlayer;
using System.Windows.Forms;
using System;
using AudioPlayer.Observer.Interfaces;

public class AppObserver : IObserver
{
    private Login _loginForm;
    private SignUp _signUpForm;

    public AppObserver(Login loginForm)
    {
        _loginForm = loginForm;
    }

    public AppObserver(SignUp signUpForm)
    {
        _signUpForm = signUpForm;
    }

    public void Update(ObserverType type, bool isSuccess)
    {
        if (type == ObserverType.Login)
        {
            if (isSuccess)
                _loginForm.HandleLoginSuccess();
            else
                _loginForm.HandleLoginFailure();
        }
        else if (type == ObserverType.Signup)
        {
            if (isSuccess)
            {
                MessageBox.Show("User added successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Login loginForm = new Login("User");
                loginForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Something went wrong 😢 Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
