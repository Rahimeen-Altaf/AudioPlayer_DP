using AudioPlayer.Observer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Observer.Interfaces
{
    public interface IObserver
    {
        void Update(ObserverType type, bool isSuccess);
    }

}
