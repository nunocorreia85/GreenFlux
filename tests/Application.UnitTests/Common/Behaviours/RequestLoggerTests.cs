using System.Threading;
using System.Threading.Tasks;
using GreenFlux.Application.Common.Behaviours;
using GreenFlux.Application.Common.Interfaces;
using GreenFlux.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace GreenFlux.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<ILogger<CreateTodoItemCommand>> _logger;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateTodoItemCommand>>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object);

            await requestLogger.Process(new CreateTodoItemCommand {ListId = 1, Title = "title"},
                new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object);

            await requestLogger.Process(new CreateTodoItemCommand {ListId = 1, Title = "title"},
                new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}