using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace Agora.Common.Api.Tests;
public class ExceptionHandlerTests
{
    public class HandleAsyncMethod
    {
        [Fact]
        public async Task ShouldReturnInternalServerErrorWhenException()
        {
            var requestDelegate = new RequestDelegate((context) => throw new Exception());
            var exceptionHandler = new ExceptionHandler(requestDelegate, Mock.Of<ILogger<ExceptionHandler>>());
            var context = new DefaultHttpContext();

            await exceptionHandler.Invoke(context);

            Assert.Equal(
                StatusCodes.Status500InternalServerError,
                context.Response.StatusCode);
        }

        [Fact]
        public async Task ShouldLogErrorWhenException()
        {
            var loggerMock = new Mock<ILogger<ExceptionHandler>>();
            var requestDelegate = new RequestDelegate((context) => throw new Exception());
            var exceptionHandler = new ExceptionHandler(requestDelegate, loggerMock.Object);
            var context = new DefaultHttpContext();

            await exceptionHandler.Invoke(context);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ), Times.Once);
        }

        [Fact]
        public async void ShouldPassThroughResultIfNoException()
        {
            var requestDelegate = new RequestDelegate((context) => Task.CompletedTask);
            var exceptionHandler = new ExceptionHandler(requestDelegate, Mock.Of<ILogger<ExceptionHandler>>());
            var context = new DefaultHttpContext();

            await exceptionHandler.Invoke(context);

            Assert.Equal(
                StatusCodes.Status200OK,
                context.Response.StatusCode);
        }
    }
}
