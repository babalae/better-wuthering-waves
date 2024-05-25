namespace BetterWutheringWaves.GameTask.AutoGeniusInvokation.Exception; // TODO: change this namespace to BetterWutheringWaves.GameTask.Common.Exception

public class RetryException : System.Exception
{
    public RetryException() : base()
    {
    }

    public RetryException(string message) : base(message)
    {
    }
}
