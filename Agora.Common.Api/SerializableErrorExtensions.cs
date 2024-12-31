using Agora.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Agora.Common.Api;

public static class SerializableErrorExtensions
{
    public static void AddApiError(this SerializableError serializableError, ApiError apiError)
    {
        serializableError.Add(GetErrorKey(apiError.GetType()), apiError);
    }

    public static bool ContainsApiError<T>(this SerializableError serializableError) where T : ApiError
    {
        return serializableError.ContainsKey(GetErrorKey(typeof(T)));
    }

    public static List<string> GetErrors(this SerializableError serializableError, string key)
    {
        return serializableError[key] is not string[] error
            ? []
            : error.ToList();
    }

    public static T? GetApiError<T>(this SerializableError serializableError) where T : ApiError
    {
        if (serializableError.TryGetValue(GetErrorKey(typeof(T)), out object? error))
        {
            // Serialize the object to JSON and then deserialize it into the desired type
            string json = JsonSerializer.Serialize(error);
            return JsonSerializer.Deserialize<T>(json);
        }

        return null;
    }

    private static string GetErrorKey(Type type)
    {
        return type.FullName ?? type.Name;
    }
}
