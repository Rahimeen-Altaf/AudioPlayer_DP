using AudioPlayer.Observer.Interfaces;
using System.Collections.Generic;

namespace AudioPlayer.Observer.Observers
{
    public class EmailObserver : IObserver
    {
        private List<string> userEmails;
        private readonly CommonHelperFacadeController facade;

        public EmailObserver(List<string> emails, CommonHelperFacadeController _facade)
        {
            userEmails = emails;
            facade = _facade;
        }

        public void Update(string message)
        {
            foreach (var email in userEmails)
            {
                facade.SendEmail(email, "Audio Player Notification", message);
            }
        }
    }
}