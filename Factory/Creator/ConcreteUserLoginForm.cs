using AudioPlayer.Factory;
using AudioPlayer;
using System.Windows.Forms;

public class ConcreteUserLoginForm : IFormFactory
{
    public Form CreateForm()
    {
        return new Login("User");
    }
}
