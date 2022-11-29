using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Shopify.Consul.Models;
using Shopify.Consul.Services;

namespace Shopify.Consul.UnitTests.Services
{
    public class ConsulHostedServiceTests
    {
        private readonly ServiceDetails serviceDetails;
        private readonly Mock<IConsulService> consulServiceMock;
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceScope serviceScopeMock;
        private readonly IServiceScopeFactory scopeFactoryMock;
        private readonly Mock<ILogger<ConsulHostedService>> loggerMock;
        private readonly ConsulHostedService hostedService;

        public ConsulHostedServiceTests()
        {
            serviceDetails = new ServiceDetails();

            consulServiceMock = new Mock<IConsulService>();
            serviceProvider = Mock.Of<IServiceProvider>(provider => provider.GetService(typeof(IConsulService)) == consulServiceMock.Object && provider.GetService(typeof(ServiceDetails)) == serviceDetails);
            serviceScopeMock = Mock.Of<IServiceScope>(scope => scope.ServiceProvider == serviceProvider);
            scopeFactoryMock = Mock.Of<IServiceScopeFactory>(factory => factory.CreateScope() == serviceScopeMock);
            loggerMock = new Mock<ILogger<ConsulHostedService>>();

            hostedService = new ConsulHostedService(scopeFactoryMock, loggerMock.Object);
        }

        [Fact]
        public async Task StartAsync_SuccessfullRegistration_RegisterServiceAndLogInformation()
        {
            // Arrange
            const int ExpectedInformationLogs = 2;
            HttpResponseMessage successHttpResponse = new() { StatusCode = System.Net.HttpStatusCode.OK };

            CancellationToken cancellationToken = CancellationToken.None;
            consulServiceMock.Setup(mock => mock.RegisterAsync(It.IsAny<ServiceDetails>(), cancellationToken)).ReturnsAsync(successHttpResponse);

            // Act
            await hostedService.StartAsync(cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.RegisterAsync(It.IsAny<ServiceDetails>(), cancellationToken), Times.Once);
            
            loggerMock.Verify(mock =>
            mock.Log(LogLevel.Information, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Exactly(ExpectedInformationLogs));

        }

        [Fact]
        public async Task StartAsync_ErrorDuringRegistration_LogError()
        {
            // Arrange
            HttpResponseMessage failHttpResponse = new() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            CancellationToken cancellationToken = CancellationToken.None;

            consulServiceMock.Setup(mock => mock.RegisterAsync(It.IsAny<ServiceDetails>(), cancellationToken)).ReturnsAsync(failHttpResponse);

            // Act
            await hostedService.StartAsync(cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.RegisterAsync(It.IsAny<ServiceDetails>(), cancellationToken), Times.Once);

            loggerMock.Verify(mock =>
            mock.Log(LogLevel.Information, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

            loggerMock.Verify(mock =>
            mock.Log(LogLevel.Error, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
        }

        [Fact]
        public async Task StopAsync_SuccessfullDeregistration_DeregisterServiceAndLogInformation()
        {
            // Arrange
            const int ExpectedInformationLogs = 2;
            
            var serviceId = "service-id";
            serviceDetails.ID = serviceId;

            HttpResponseMessage successHttpResponse = new() { StatusCode = System.Net.HttpStatusCode.OK };

            CancellationToken cancellationToken = CancellationToken.None;
            consulServiceMock.Setup(mock => mock.DeregisterAsync(serviceId, cancellationToken)).ReturnsAsync(successHttpResponse);

            // Act
            await hostedService.StopAsync(cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.DeregisterAsync(serviceId, cancellationToken), Times.Once);

            loggerMock.Verify(x =>
            x.Log(LogLevel.Information, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Exactly(ExpectedInformationLogs));
        }

        [Fact]
        public async Task StopAsync_ErrorDuringDeregistration_LogError()
        {
            // Arrange
            HttpResponseMessage failHttpResponse = new() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            CancellationToken cancellationToken = CancellationToken.None;
            var serviceId = "service-id";

            serviceDetails.ID = serviceId;

            consulServiceMock.Setup(mock => mock.DeregisterAsync(serviceId, cancellationToken)).ReturnsAsync(failHttpResponse);

            // Act
            await hostedService.StopAsync(cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.DeregisterAsync(serviceId, cancellationToken), Times.Once);

            loggerMock.Verify(x =>
            x.Log(LogLevel.Information, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

            loggerMock.Verify(x =>
            x.Log(LogLevel.Error, 0, It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
        }
    }
}
