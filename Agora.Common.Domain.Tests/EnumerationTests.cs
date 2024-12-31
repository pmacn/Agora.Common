namespace Agora.Common.Domain.Tests;
public class EnumerationTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.False(enumeration.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEnumeration()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.False(enumeration.Equals(new object()));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameType()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.False(enumeration.Equals(OtherTestEnumeration.Option1));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameId()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.False(enumeration.Equals(TestEnumeration.Option2));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameId()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.True(enumeration.Equals(TestEnumeration.Option1));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameInstance()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.True(enumeration.Equals(enumeration));
        }
    }

    public class TheGetHashCodeMethod
    {
        [Fact]
        public void ShouldReturnSameHashCodeWhenSameId()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.Equal(enumeration.GetHashCode(), TestEnumeration.Option1.GetHashCode());
        }

        [Fact]
        public void ShouldReturnDifferentHashCodeWhenDifferentId()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.NotEqual(enumeration.GetHashCode(), TestEnumeration.Option2.GetHashCode());
        }
    }

    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnName()
        {
            var enumeration = TestEnumeration.Option1;
            Assert.Equal("Test Option 1", enumeration.ToString());
        }
    }

    public class TheGetAllMethod
    {
        [Fact]
        public void ShouldReturnAllEnumerations()
        {
            var enumerations = Enumeration.GetAll<TestEnumeration>();
            Assert.Equal(2, enumerations.Count());
            Assert.Contains(TestEnumeration.Option1, enumerations);
            Assert.Contains(TestEnumeration.Option2, enumerations);
        }
    }

    public class TheFromValueMethod
    {
        [Fact]
        public void ShouldReturnEnumerationByValue()
        {
            var enumeration = Enumeration.FromValue<TestEnumeration>(1);
            Assert.Equal(TestEnumeration.Option1, enumeration);
        }

        [Fact]
        public void ShouldThrowWhenEnumerationNotFound()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumeration.FromValue<TestEnumeration>(3));
        }
    }

    private sealed class TestEnumeration : Enumeration
    {
        public static readonly TestEnumeration Option1 = new(1, "Test Option 1");
        public static readonly TestEnumeration Option2 = new(2, "Test Option 2");

        public TestEnumeration(int id, string name) : base(id, name)
        {
        }
    }

    private sealed class OtherTestEnumeration : Enumeration
    {
        public static readonly OtherTestEnumeration Option1 = new(1, "Other Test Option 1");

        public OtherTestEnumeration(int id, string name) : base(id, name)
        {
        }
    }
}
