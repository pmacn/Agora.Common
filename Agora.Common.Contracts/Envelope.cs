namespace Agora.Common.Contracts;

/// <summary>
/// Represents a generic envelope for wrapping API responses. 
/// </summary>
/// <typeparam name="T">The type of the result contained in the envelope.</typeparam>
public class Envelope<T>
{
    /// <summary>
    /// Initializes a new instance of the Envelope class.
    /// </summary>
    /// <param name="result">The result of the API call.</param>
    /// <param name="errorMessage">The error message, if any.</param>
    protected internal Envelope(T? result, string? errorMessage)
    {
        Result = result;
        ErrorMessage = errorMessage;
        TimeGenerated = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the result of the API call. Can be null.
    /// </summary>
    public T? Result { get; }
    /// <summary>
    /// Gets the error message, if any, of the API call. Null if there is no error.
    /// </summary>
    public string? ErrorMessage { get; }
    /// <summary>
    /// Gets the time when the response was generated.
    /// </summary>
    public DateTime TimeGenerated { get; }
}

/// <summary>
/// Non-generic version of Envelope for responses without a specific result type.
/// </summary>
public class Envelope : Envelope<string?>
{
    /// <summary>
    /// Initializes a new instance of the Envelope class with an error message.
    /// </summary>
    /// <param name="errorMessage">The error message, if any.</param>
    protected Envelope(string? errorMessage)
        : base(null, errorMessage)
    {
    }

    /// <summary>
    /// Creates a successful response envelope with the specified result.
    /// </summary>
    /// <param name="result">The result to include in the response.</param>
    /// <returns>An envelope containing the result.</returns>
    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }


    /// <summary>
    /// Creates a successful response envelope without a specific result.
    /// </summary>
    /// <returns>An envelope indicating success without a specific result.</returns>
    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    /// <summary>
    /// Creates an error response envelope with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message for the response.</param>
    /// <returns>An envelope containing the error message.</returns>
    public static Envelope Error(string errorMessage)
    {
        return new Envelope(errorMessage);
    }
}
