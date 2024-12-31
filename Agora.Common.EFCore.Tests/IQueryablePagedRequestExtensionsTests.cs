using Agora.Common.Contracts;
using Agora.Common.EFCore;
using MockQueryable;

namespace Agora.Common.Domain.Tests;

public class IQueryablePagedRequestExtensionsTests
{
    public class TakePageAsyncMethod
    {
        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenQueryIsNull()
        {
            IQueryable<object>? query = null;
            var request = new PagedRequest();

            await Assert.ThrowsAsync<ArgumentNullException>(() => query!.TakePageAsync(request));
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenRequestIsNull()
        {
            var query = new List<object>().AsQueryable();
            PagedRequest? request = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => query.TakePageAsync(request!));
        }

        [Fact]
        public async Task ShouldReturnEmptyCollectionWhenQueryIsEmpty()
        {
            var query = new List<object>().BuildMock();
            var request = new PagedRequest();
            var result = await query.TakePageAsync(request);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ShouldReturnCollectionWithOneItemWhenQueryHasOneItem()
        {
            var query = new List<object> { new object() }.BuildMock();
            var request = new PagedRequest();
            var result = await query.TakePageAsync(request);
            Assert.Single(result);
        }

        [Fact]
        public async Task ShouldReturnCollectionWithOneItemWhenQueryHasTwoItemsAndPageSizeIsOne()
        {
            var query = new List<object> { new(), new() }.BuildMock();
            var request = new PagedRequest { PageSize = 1 };
            var result = await query.TakePageAsync(request);
            Assert.Single(result);
        }


        [Fact]
        public async Task ShouldReturnCollectionWithTwoItemsWhenQueryHasTwoItemsAndPageSizeIsTwo()
        {
            var query = new List<object> { new(), new() }.BuildMock();
            var request = new PagedRequest { PageSize = 2 };
            var result = await query.TakePageAsync(request);
            Assert.Equal(2, result.Count);
        }
    }
}
