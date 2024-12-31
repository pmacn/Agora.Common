
namespace Agora.Common.Contracts.Tests;

public class PagedResponseTests
{
    public class TheConstructor
    {
        [Theory]
        [InlineData(-1, 10, 10)]
        [InlineData(10, 0, 10)]
        [InlineData(10, -1, 10)]
        [InlineData(10, 10, -1)]
        public void ThrowsExceptionWhenInvalid(int page, int pageSize, int totalCount)
        {
            Assert.Throws<ArgumentException>(() => new PagedResponse<int>(page, pageSize, totalCount, []));
        }
    }
    
    public class ThePageCountProperty
    {
        [Theory]
        [InlineData(20, 10, 1)]
        [InlineData(20, 20, 1)]
        [InlineData(20, 21, 2)]
        [InlineData(20, 40, 2)]
        [InlineData(20, 1200, 60)]
        [InlineData(20, 1201, 61)]
        public void ReturnsCorrectCount(int pageSize, int totalCount, int expectedPageCount)
        {
            var sut = new PagedResponse<int>(1, pageSize, totalCount, []);

            Assert.Equal(expectedPageCount, sut.PageCount);
        }
    }
}
