using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Agora.Common.Contracts;

/// <summary>
/// Base class for API clients used to make HTTP requests to external and internal APIs.
/// </summary>
public abstract class ApiClient
{
    /// <summary>
    /// The name of the HTTP header used for the tenant ID.
    /// </summary>
    private readonly ILogger<ApiClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _defaultOptions;

    /// <summary>
    /// Initializes a new instance of the ApiClient class.
    /// </summary>
    /// <param name="logger">The logger for capturing log messages.</param>
    /// <param name="httpClient">The HttpClient used for making HTTP requests.</param>
    /// <param name="defaultOptions">Optional JSON serializer options for requests and responses.</param>
    protected ApiClient(
        ILogger<ApiClient> logger,
        HttpClient httpClient,
        JsonSerializerOptions? defaultOptions = null)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _defaultOptions = defaultOptions ?? new JsonSerializerOptions();
    }

    /// <summary>
    /// Performs a GET request to the specified URI and deserializes the response to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="uri">The URI to send the GET request to.</param>
    /// <param name="token">A cancellation token to cancel the operation.</param>
    /// <param name="options">Optional JSON serializer options for this request.</param>
    /// <returns>An ApiResponse object containing the response data and status.</returns>
    protected async Task<ApiResponse<T>> Get<T>(string uri, CancellationToken token = default, JsonSerializerOptions? options = null)
    {
        _logger.LogDebug($"GET {uri}");
        var response = await _httpClient.GetAsync(uri, token);
        var effectiveOptions = options ?? _defaultOptions;
        return await CreateResponseAsync<T>(response, effectiveOptions);
    }

    /// <summary>
    /// Performs a POST request to the specified URI with the provided content and deserializes the response to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="uri">The URI to send the POST request to.</param>
    /// <param name="content">The content to send in the request body.</param>
    /// <param name="token">A cancellation token to cancel the operation.</param>
    /// <param name="options">Optional JSON serializer options for this request.</param>
    /// <returns>An ApiResponse object containing the response data and status.</returns>
    protected async Task<ApiResponse<T>> Post<T>(string uri, object content, CancellationToken token = default, JsonSerializerOptions? options = null)
    {
        _logger.LogDebug($"POST {uri}");
        var effectiveOptions = options ?? _defaultOptions;
        var response = await _httpClient.PostAsync(uri, CreateContent(content, effectiveOptions), token);
        return await CreateResponseAsync<T>(response, effectiveOptions);
    }

    /// <summary>
    /// Performs a PUT request to the specified URI with the provided content and deserializes the response to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="uri">The URI to send the PUT request to.</param>
    /// <param name="content">The content to send in the request body.</param>
    /// <param name="token">A cancellation token to cancel the operation.</param>
    /// <param name="options">Optional JSON serializer options for this request.</param>
    /// <returns>An ApiResponse object containing the response data and status.</returns>
    protected async Task<ApiResponse> Put(string uri, object content, CancellationToken token = default, JsonSerializerOptions? options = null)
    {
        _logger.LogDebug($"PUT {uri}");
        var effectiveOptions = options ?? _defaultOptions;
        var response = await _httpClient.PutAsync(uri, CreateContent(content, effectiveOptions), token);
        return await CreateResponseAsync(response, effectiveOptions);
    }

    /// <summary>
    /// Performs a PUT request to the specified URI with the provided content and deserializes the response to the specified type.
    /// </summary>
    /// <param name="uri">The URI to send the PUT request to.</param>
    /// <param name="content">The content to send in the request body.</param>
    /// <param name="token">A cancellation token to cancel the operation.</param>
    /// <param name="options">Optional JSON serializer options for this request.</param>
    /// <returns>An ApiResponse object containing the response data and status.</returns>
    protected async Task<ApiResponse<T>> Put<T>(string uri, object content, CancellationToken token = default, JsonSerializerOptions? options = null)
    {
        _logger.LogDebug($"PUT {uri}");
        var effectiveOptions = options ?? _defaultOptions;
        var response = await _httpClient.PutAsync(uri, CreateContent(content, effectiveOptions), token);
        return await CreateResponseAsync<T>(response, effectiveOptions);
    }

    /// <summary>
    /// Performs a DELETE request to the specified URI and deserializes the response to an ApiResponse.
    /// </summary>
    /// <param name="uri">The URI to send the DELETE request to.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="options">Optional JSON serializer options for this request.</param>
    /// <returns>An ApiResponse object containing the response data and status.</returns>
    protected async Task<ApiResponse> Delete(string uri, CancellationToken cancellationToken, JsonSerializerOptions? options = null)
    {
        _logger.LogDebug($"DELETE {uri}");
        var effectiveOptions = options ?? _defaultOptions;
        var response = await _httpClient.DeleteAsync(uri, cancellationToken);
        return await CreateResponseAsync(response, effectiveOptions);
    }

    private static StringContent CreateContent(object obj, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(obj, options);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<ApiResponse> CreateResponseAsync(HttpResponseMessage response, JsonSerializerOptions options)
    {
        if (response.IsSuccessStatusCode)
        {
            return new ApiResponse(response);
        }

        var error = await DeserializeContentSafelyAsync<ApiError>(response, options);
        return new ApiResponse(response, error);
    }

    private async Task<ApiResponse<T>> CreateResponseAsync<T>(HttpResponseMessage response, JsonSerializerOptions options)
    {
        return response.IsSuccessStatusCode ?
            await CreateSuccessResponseAsync<T>(response, options) :
            await CreateErrorResponseAsync<T>(response, options);
    }

    private async Task<ApiResponse<T>> CreateErrorResponseAsync<T>(HttpResponseMessage response, JsonSerializerOptions options)
    {
        var error = await DeserializeContentSafelyAsync<ApiError>(response, options);
        return new ApiResponse<T>(response, default, error);
    }

    private async Task<ApiResponse<T>> CreateSuccessResponseAsync<T>(HttpResponseMessage response, JsonSerializerOptions options)
    {
        var payload = await DeserializeContentSafelyAsync<T>(response, options);
        return new ApiResponse<T>(response, payload);
    }

    private async Task<T?> DeserializeContentSafelyAsync<T>(HttpResponseMessage response, JsonSerializerOptions options)
    {
        T? obj = default;
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            obj = JsonSerializer.Deserialize<T>(content, options);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"Failed to deserialize content to type {typeof(T)}. Content: {content}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during deserialization.");
        }

        return obj;
    }
}
