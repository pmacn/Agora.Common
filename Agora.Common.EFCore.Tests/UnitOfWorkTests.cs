using Agora.Common.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Agora.CommonEFCore.Tests;

public class UnitOfWorkTests
{
    public class TestBed
    {
        public TestBed()
        {
            DbContextMock = new();
            Sut = new(DbContextMock.Object);
        }

        public Mock<DbContext> DbContextMock { get; }
        public UnitOfWork Sut { get; }
    }

    public class TheSaveAsyncMethod : TestBed
    {
        [Fact]
        public async Task ShouldCallDbContextSaveChangesAsync()
        {
            await Sut.SaveChangesAsync();

            DbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

    // TODO: Fix this test
    //public class TheBeginTransactionAsyncMethod : TestBed
    //{
    //    [Fact]
    //    public async void ShouldCallDatabaseBeginTransactionAsync()
    //    {
    //        var mockTransaction = new Mock<IDbContextTransaction>();
    //        var databaseMock = new Mock<DatabaseFacade>();
    //        DbContextMock.SetupGet(x => x.Database).Returns(databaseMock.Object);
    //        databaseMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
    //            .ReturnsAsync(mockTransaction.Object);

    //        await Sut.BeginTransactionAsync();

    //        databaseMock.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    //    }
    //}

    public class TheDisposeMethod
    {
        [Fact]
        public void ShouldCallDbContextDispose()
        {
            var dbContextMock = new Mock<DbContext>();
            using var sut = new UnitOfWork(dbContextMock.Object);

            sut.Dispose();

            dbContextMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
