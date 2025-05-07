public interface IObservable
{
    void RegisterObserver(ILoginObserver observer);
    void UnregisterObserver(ILoginObserver observer);
    void NotifyObservers(bool isValid);
}
