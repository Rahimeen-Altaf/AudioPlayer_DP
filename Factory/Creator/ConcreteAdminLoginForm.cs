using AudioPlayer.Factory;
using AudioPlayer;
using System.Windows.Forms;

public class ConcreteAdminLoginForm : IFormFactory
{
    public Form CreateForm()
    {
        return new Login("Admin");
    }
}
