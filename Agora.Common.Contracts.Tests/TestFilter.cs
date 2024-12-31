namespace Agora.Common.Contracts.Tests;

public class TestFilter : IRequestFilter
{
    public string Name { get; set; } = "Fubar";

    public string ToQueryString()
    {
        return "";
    }
}
