using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Observer
{
    public class LoginObserver : ILoginObserver
    {
        private Login _loginForm;

        public LoginObserver(Login loginForm)
        {
            _loginForm = loginForm;
        }

        public void Update(bool isValid)
        {
            if (isValid)
            {
                //email
                //firend ye active
                // acitivty log
                // login IP 
                _loginForm.HandleLoginSuccess();
                
            }
            else
            {
                _loginForm.HandleLoginFailure();
            }
        }
    }

}
