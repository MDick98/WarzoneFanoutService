using Xunit;
using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using WarzoneFanout.Api.Controllers;
using System.Threading.Tasks;
using WarzoneFanout.Domain;
using WarzoneFanout.Application.Requests;
using WarzoneFanout.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WarzoneFanout.Api.UnitTests
{
    public class StatsControllerTests
    {
        private readonly Mock<IMediator> mediator;

        private readonly StatsController controller;

        public StatsControllerTests()
        {
            Mock<ILogger<StatsController>> logger = new();
            mediator = new Mock<IMediator>();

            controller = new StatsController(logger.Object, mediator.Object);
        }

        [Fact]
        public async Task GetAsync_Returns_AllStats()
        {
            // Arrange
            var username = "testuser";
            var expectedAllStats = new AllStats();

            mediator
                .Setup(m => m.Send(It.Is<GetStatsRequest>(r => r.Username == username), default))
                .ReturnsAsync(new StatsResponse { AllStats = expectedAllStats });

            // Act
            var result = await controller.GetAsync(username);

            // Assert
            Assert.Equal(expectedAllStats, result);
        }

        [Fact]
        public async Task GetAsync_Returns_NotFound_For_Invalid_Username()
        {
            // Arrange
            var username = "testuser";
            StatsResponse response = new();

            mediator.Setup(m => m.Send(It.Is<GetStatsRequest>(r => r.Username == username), default)).ReturnsAsync(response);

            // Act
            var result = await controller.GetAsync(username);

            // Assert
            Assert.Null(result);
        }
    }
}