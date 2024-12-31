namespace Agora.Common.Contracts.Tests;

public class PagedRequestBuilderTests
{
    public class Builder
    {
        [Fact]
        public void CreatesSimpleRequest()
        {
            var request = PagedRequest.Create(1, 1).Build();

            Assert.Equal(1, request.Page);
            Assert.Equal(1, request.PageSize);
            Assert.Empty(request.Sorts);
        }

        [Fact]
        public void CreatesFilterRequest()
        {
            var request = PagedRequest.Create(1, 1).WithFilter(new TestFilter()).Build();

            Assert.IsType<PagedRequest<TestFilter>>(request);
            Assert.NotNull(request.Filter);
        }

        [Fact]
        public void CreatesSortRequest()
        {
            var request = PagedRequest.Create(1, 1).WithSort([new("field")]).Build();
            
            Assert.NotNull(request.Sorts);
            Assert.Single(request.Sorts);
        }
    }
}