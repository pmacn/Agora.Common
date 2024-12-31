
namespace Agora.Common.Contracts.Tests;

public class PagedRequestTests
{
    public class TestBed
    {
        public TestBed() {
            Builder = PagedRequest.Create();
        }

        public IPagedRequestBuilder Builder { get; }
    }

    public class CanCreateQueryString : TestBed
    {
        [Fact]
        public void WhenNoFiltersOrSorts()
        {
            var sut = Builder.Build();
            
            var result = sut.ToQueryString();

            Assert.Equal("?page=1&pageSize=20", result);
        }

        [Fact]
        public void WhenOneSort()
        {
            var sort = new SortDefinition("Name");
            var sut = Builder.WithSort([sort]).Build();
            
            var result = sut.ToQueryString();
            
            Assert.Contains("sort=Name", result);
        }

        [Fact]
        public void WhenTwoSorts()
        {
            var nameSort = new SortDefinition("Name");
            var ageSort = new SortDefinition("Age", SortDirection.Descending);
            var sut = Builder.WithSort([nameSort, ageSort]).Build();

            var result = sut.ToQueryString();

            Assert.Contains("&sort=Name,Age desc", result);
        }
    }
}
