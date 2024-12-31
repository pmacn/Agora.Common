
namespace Agora.Common.Contracts;

/// <summary>
/// Represents a generic API response with the HTTP response message and an optional error.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Initializes a new instance of the ApiResponse class.
    /// </summary>
    /// <param name="message">The HttpResponseMessage returned by the API call.</param>
    /// <param name="error">Optional SerializableError containing error details.</param>
    public ApiResponse(HttpResponseMessage message, ApiError? error = null)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Error = error;
    }

    /// <summary>
    /// Gets the HTTP response message.
    /// </summary>
    public HttpResponseMessage Message { get; }

    /// <summary>
    /// Gets the optional error information associated with the response.
    /// </summary>
    public ApiError? Error { get; }
}

/// <summary>
/// Represents a generic API response with a specific payload type, along with the standard HTTP response message and an optional error.
/// </summary>
/// <typeparam name="T">The type of the payload in the API response.</typeparam>
public class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// Initializes a new instance of the ApiResponse class with a payload.
    /// </summary>
    /// <param name="message">The HttpResponseMessage returned by the API call.</param>
    /// <param name="payload">The payload of type T contained in the response.</param>
    /// <param name="error">Optional SerializableError containing error details.</param>
    public ApiResponse(HttpResponseMessage message, T? payload, ApiError? error = null)
        : base(message, error)
    {
        Payload = payload;
    }

    /// <summary>
    /// Gets the payload of the response.
    /// </summary>
    public T? Payload { get; }
}