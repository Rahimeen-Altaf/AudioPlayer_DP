public abstract class AdminOperationContext
{
    protected IAdminOperationStrategy _strategy;

    public void SetStrategy(IAdminOperationStrategy strategy)
    {
        _strategy = strategy;
    }

    public virtual void ExecuteStrategy(string username)
    {
        _strategy?.Execute(username);
    }
}
