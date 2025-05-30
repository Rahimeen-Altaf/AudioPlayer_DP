using AudioPlayer.Observer.Interfaces;
using System;
using System.Collections.Generic;

public class ObserverManager : IObservable
{
    private static ObserverManager _instance;

    private static readonly object _lock = new object();

    private ObserverManager() { }

    public static ObserverManager Instance
    {
        get
        {
            lock (_lock) // Ensures only one thread can access this block at a time
            {
                if (_instance == null)
                    _instance = new ObserverManager();
                return _instance;
            }
        }
    }

    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }
}
