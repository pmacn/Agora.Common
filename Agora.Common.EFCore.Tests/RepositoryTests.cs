using Agora.Common.Domain;
using Agora.Common.EFCore;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Agora.CommonEFCore.Tests;

public class RepositoryTests
{
    public class TheGetByIdAsyncMethod
    {
        [Fact]
        public async Task ReturnsNullIfNotFound()
        {
            var dbSetMock = new Mock<DbSet<TestAggregateRoot>>();
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(x => x.Set<TestAggregateRoot>()).Returns(dbSetMock.Object);
            var sut = new TestRepository(dbContextMock.Object);

            var result = await sut.FindAsync(1L);

            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnsTheAggregateRootIfFound()
        {
            var dbSetMock = new Mock<DbSet<TestAggregateRoot>>();
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(x => x.Set<TestAggregateRoot>()).Returns(dbSetMock.Object);
            var expectedResult = new TestAggregateRoot();
            dbSetMock.Setup(x => x.FindAsync(It.IsAny<long>())).ReturnsAsync(expectedResult);
            var sut = new TestRepository(dbContextMock.Object);

            var result = await sut.FindAsync(1);

            Assert.Same(expectedResult, result);
        }
    }

    public class TestAggregateRoot : AggregateRoot
    {
    }

    public class TestRepository(DbContext dbContext)
        : Repository<TestAggregateRoot>(dbContext)
    {
    }
}
