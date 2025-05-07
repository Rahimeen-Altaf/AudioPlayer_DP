public interface ISignupObservable
{
    void RegisterObserver(ISignUpObserver observer);
    void UnregisterObserver(ISignUpObserver observer);
    void NotifyObservers(bool isValid);
}
