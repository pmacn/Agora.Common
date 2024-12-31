namespace Agora.Common.Contracts.Tests;

public class SortDefinitionsTests
{
    public class ToQueryStringMethod
    {
        [Fact]
        public void WhenNoSortDefinitions()
        {
            var sortDefinitions = new SortDefinitions();
            var queryString = sortDefinitions.ToQueryString();
            Assert.Empty(queryString);
        }

        [Fact]
        public void WhenOneSortDefinition()
        {
            var sortDefinitions = new SortDefinitions
            {
                new SortDefinition("Name")
            };

            var queryString = sortDefinitions.ToQueryString();

            Assert.Equal("&sort=Name", queryString);
        }

        [Fact]
        public void WhenTwoSortDefinitions()
        {
            var sortDefinitions = new SortDefinitions
            {
                new SortDefinition("Name"),
                new SortDefinition("Age", SortDirection.Descending)
            };

            var queryString = sortDefinitions.ToQueryString();

            Assert.Equal("&sort=Name,Age desc", queryString);
        }
    }
}
