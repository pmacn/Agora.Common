namespace Agora.Common.Domain.Tests;

public class EntityTests
{
    internal sealed class TestEntity: Entity
    {
        public TestEntity(long id) => Id = id;
    };

    internal sealed class OtherTestEntity : Entity
    {
        public OtherTestEntity(long id) => Id = id;
    };

    public class EqualsMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenOtherIsNull()
        {
            var entity = new TestEntity(1);
            Assert.False(entity.Equals(null));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotEntityBase()
        {
            var entity = new TestEntity(1);
            Assert.False(entity.Equals(new object()));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameType()
        {
            long id = 1;
            var entity = new TestEntity(id);
            Assert.False(entity.Equals(new OtherTestEntity(id)));
        }

        [Fact]
        public void ShouldReturnFalseWhenOtherIsNotSameId()
        {
            long id = 1;
            long otherId = 2;
            TestEntity entity = new(id);
            Assert.False(entity.Equals(new TestEntity(otherId)));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameId()
        {
            long id = 1;
            var entity = new TestEntity(id);
            Assert.True(entity.Equals(new TestEntity(id)));
        }

        [Fact]
        public void ShouldReturnTrueWhenOtherIsSameInstance()
        {
            var entity = new TestEntity(1L);
            Assert.True(entity.Equals(entity));
        }
    }
}
