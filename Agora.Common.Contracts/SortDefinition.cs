namespace Agora.Common.Contracts;

public class SortDefinition(string field, SortDirection direction = SortDirection.Ascending)
{
    public string Field { get; set; } = field ?? throw new ArgumentNullException(nameof(field));
    public SortDirection Direction { get; set; } = direction;

    internal string? ToQueryStringValue()
    {
        return Direction == SortDirection.Ascending ? Field : $"{Field} desc";
    }
}
