using AudioPlayer.Factory;
using AudioPlayer;
using System.Windows.Forms;

public class ConcreteSignUpForm : IFormFactory
{
    public Form CreateForm()
    {
        return new SignUp();
    }
}
