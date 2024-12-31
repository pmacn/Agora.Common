namespace Agora.Common.Domain.Tests;
public class ValueObjectTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var valueObject = new TestValueObject();
            Assert.False(valueObject.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotValueObject()
        {
            var valueObject = new TestValueObject();
            Assert.False(valueObject.Equals(new object()));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameType()
        {
            var valueObject = new TestValueObject();
            Assert.False(valueObject.Equals(new OtherTestValueObject()));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.False(valueObject.Equals(new TestValueObject { Property1 = "three", Property2 = "four" }));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.True(valueObject.Equals(new TestValueObject { Property1 = "one", Property2 = "two" }));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameInstance()
        {
            var valueObject = new TestValueObject();
            Assert.True(valueObject.Equals(valueObject));
        }

        [Fact]
        public void ShouldReturnSameHashCodeWhenSameValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.Equal(valueObject.GetHashCode(), new TestValueObject { Property1 = "one", Property2 = "two" }.GetHashCode());
        }

        [Fact]
        public void ShouldReturnDifferentHashCodeWhenDifferentValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.NotEqual(valueObject.GetHashCode(), new TestValueObject { Property1 = "three", Property2 = "four" }.GetHashCode());
        }
    }

    public class TheEqualityOperator
    {
        [Fact]
        public void ShouldReturnFalseWhenLeftIsNull()
        {
            TestValueObject? left = null;
            var right = new TestValueObject();
            Assert.False(left == right);
        }

        [Fact]
        public void ShouldReturnFalseWhenRightIsNull()
        {
            var left = new TestValueObject();
            TestValueObject? right = null;
            Assert.False(left == right);
        }

        [Fact]
        public void ShouldReturnTrueWhenBothAreNull()
        {
            TestValueObject? left = null;
            TestValueObject? right = null;
            Assert.True(left == right);
        }

        [Fact]
        public void ShouldReturnFalseWhenLeftIsNotSameType()
        {
            var left = new TestValueObject();
            var right = new OtherTestValueObject();
            Assert.False(left == right);
        }

        [Fact]
        public void ShouldReturnFalseWhenRightIsNotSameType()
        {
            var left = new TestValueObject();
            var right = new OtherTestValueObject();
            Assert.False(left == right);
        }

        [Fact]
        public void ShouldReturnFalseWhenLeftIsNotSameValue()
        {
            var left = new TestValueObject { Property1 = "one", Property2 = "two" };
            var right = new TestValueObject { Property1 = "three", Property2 = "four" };
            Assert.False(left == right);
        }

        [Fact]
        public void ShouldReturnTrueWhenLeftIsSameValue()
        {
            var left = new TestValueObject { Property1 = "one", Property2 = "two" };
            var right = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.True(left == right);
        }

        [Fact]
        public void ShouldReturnTrueWhenSameInstance()
        {
            var valueObject = new TestValueObject();
            Assert.True(valueObject == valueObject);
        }
    }

    public class TheInequalityOperator
    {
        [Fact]
        public void ShouldReturnTrueWhenLeftIsNull()
        {
            TestValueObject? left = null;
            var right = new TestValueObject();
            Assert.True(left != right);
        }

        [Fact]
        public void ShouldReturnTrueWhenRightIsNull()
        {
            var left = new TestValueObject();
            TestValueObject? right = null;
            Assert.True(left != right);
        }

        [Fact]
        public void ShouldReturnFalseWhenBothAreNull()
        {
            TestValueObject? left = null;
            TestValueObject? right = null;
            Assert.False(left != right);
        }

        [Fact]
        public void ShouldReturnTrueWhenLeftIsNotSameType()
        {
            var left = new TestValueObject();
            var right = new OtherTestValueObject();
            Assert.True(left != right);
        }

        [Fact]
        public void ShouldReturnTrueWhenRightIsNotSameType()
        {
            var left = new TestValueObject();
            var right = new OtherTestValueObject();
            Assert.True(left != right);
        }

        [Fact]
        public void ShouldReturnTrueWhenLeftIsNotSameValue()
        {
            var left = new TestValueObject { Property1 = "one", Property2 = "two" };
            var right = new TestValueObject { Property1 = "three", Property2 = "four" };
            Assert.True(left != right);
        }

        [Fact]
        public void ShouldReturnFalseWhenLeftIsSameValue()
        {
            var left = new TestValueObject { Property1 = "one", Property2 = "two" };
            var right = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.False(left != right);
        }

        [Fact]
        public void ShouldReturnFalseWhenLeftIsSameInstance()
        {
            var left = new TestValueObject();
            Assert.False(left != left);
        }

        [Fact]
        public void ShouldReturnFalseWhenRightIsSameInstance()
        {
            var right = new TestValueObject();
            Assert.False(right != right);
        }
    }

    public class TheGetHashCodeMethod
    {
        [Fact]
        public void ShouldReturnSameHashCodeWhenSameValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.Equal(valueObject.GetHashCode(), new TestValueObject { Property1 = "one", Property2 = "two" }.GetHashCode());
        }

        [Fact]
        public void ShouldReturnDifferentHashCodeWhenDifferentValue()
        {
            var valueObject = new TestValueObject { Property1 = "one", Property2 = "two" };
            Assert.NotEqual(valueObject.GetHashCode(), new TestValueObject { Property1 = "three", Property2 = "four" }.GetHashCode());
        }
    }

    internal class TestValueObject : ValueObject
    {
        public string Property1 { get; set; } = string.Empty;
        public string Property2 { get; set; } = string.Empty;

        protected override IEnumerable<object?> EqualityComponents => [Property1, Property2];
    }

    internal class OtherTestValueObject : ValueObject
    {
        public string Property1 { get; set; } = string.Empty;
        public string Property2 { get; set; } = string.Empty;

        protected override IEnumerable<object?> EqualityComponents => [Property1, Property2];
    }
}
