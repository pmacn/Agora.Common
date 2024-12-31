using System.Text;
using System.Text.Json;

namespace Agora.Common.Contracts.Tests;
public class DateOnlyJsonConverterTests
{
    public class TestBed
    {
        public TestBed() {
            Sut = new DateOnlyJsonConverter();
        }

        public DateOnlyJsonConverter Sut { get; }
    }

    public class TheReadMethod : TestBed
    {
        [Fact]
        public void ShouldReturnDefaultWhenValueIsNull()
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("null"));
            reader.Read();
            var result = Sut.Read(ref reader, typeof(DateOnly), null!);

            Assert.Equal(default, result);
        }

        [Fact]
        public void ShouldReturnDateOnlyWhenValueIsNotNull()
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("\"2021-01-01\""));
            reader.Read();
            var result = Sut.Read(ref reader, typeof(DateOnly), null!);

            Assert.Equal(new DateOnly(2021, 1, 1), result);
        }

        [Fact]
        public void ShouldThrowWhenValueIsNotAString()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("1"));
                reader.Read();
                Sut.Read(ref reader, typeof(DateOnly), null!);
            });
        }
    }

    public class TheWriteMethod : TestBed
    {
        [Fact]
        public void ShouldWriteDateOnly()
        {
            var dateOnly = new DateOnly(2021, 1, 1);
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            Sut.Write(writer, dateOnly, null!);
            writer.Flush();

            var result = Encoding.UTF8.GetString(stream.ToArray());

            Assert.Equal("\"2021-01-01\"", result);
        }
    }
}
