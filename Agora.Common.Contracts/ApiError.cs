namespace Agora.Common.Contracts;

public abstract record ApiError
{
    protected ApiError(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

