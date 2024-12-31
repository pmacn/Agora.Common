
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Agora.Common.Contracts;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string _format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (stringValue == null)
        {
            return default;
        }

        return DateOnly.ParseExact(stringValue, _format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format, CultureInfo.InvariantCulture));
    }
}
