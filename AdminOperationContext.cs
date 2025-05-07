public class AdminOperationContext
{
    private IAdminOperationStrategy _strategy;

    public void SetStrategy(IAdminOperationStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExecuteStrategy(string username)
    {
        _strategy?.Execute(username);
    }
}
