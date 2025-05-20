using AudioPlayer.Observer.Interfaces;
using System;
using System.Collections.Generic;

public class ObserverManager
{
    private static ObserverManager _instance;
    private static readonly object _lock = new object();
    private List<IObserver> observers = new List<IObserver>();

    private ObserverManager() { }

    public static ObserverManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new ObserverManager();
                return _instance;
            }
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }
}
