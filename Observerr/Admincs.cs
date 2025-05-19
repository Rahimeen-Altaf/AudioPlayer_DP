using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Observerr
{
    public class Admin : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        // Concrete business methods
        public void AddAudio(string title)
        {
            // Assume: audio added in DB
            Notify($"New audio added: {title}");
        }

        public void DeleteAudio(string title)
        {
            // DB logic
            Notify($"Audio deleted: {title}");
        }

        public void UpdateAudio(string oldTitle, string newTitle)
        {
            // DB logic
            Notify($"Audio updated from '{oldTitle}' to '{newTitle}'");
        }
    }

}
