using FluentAssertions;
using Moq;
using Shopify.Consul.Models;
using Shopify.Consul.Services;
using Shopify.Consul.Strategies;

namespace Shopify.Consul.UnitTests.Services
{
    public class ServiceRegistryProviderTests
    {
        private readonly Mock<IConsulService> consulServiceMock;
        private readonly Mock<ILoadBalancer> loadBalancerMock;
        private readonly ServiceRegistryProvider registryProvider;

        public ServiceRegistryProviderTests()
        {
            consulServiceMock = new Mock<IConsulService>();
            loadBalancerMock = new Mock<ILoadBalancer>();

            registryProvider = new ServiceRegistryProvider(consulServiceMock.Object, loadBalancerMock.Object);
        }

        [Fact]
        public void Construct_WithNullConsulService_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>("consulService", () => new ServiceRegistryProvider(null));
        }



        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task GetAsync_EmptyOrNullServiceName_UseConsulServiceWithProvidedName(string name)
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;

            // Act
            await registryProvider.GetAsync(name, cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.ListServicesByNameAsync(name, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ServicesWithGivenNameHaveBeenRegistered_ReturnLoadBalancedResult()
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            string registeredServiceName = "RegisteredServiceName";

            var registeredServices = new Dictionary<string, ServiceAgent>()
            {
                ["testservice:1fb79ddc-94c6-4c6c-8f20-b99540e05c2e"] = new() { Service = "testservice", ID = "1fb79ddc-94c6-4c6c-8f20-b99540e05c2e" },
                ["testservice:f3505103-bdca-4c48-aedb-85cfcd7a7275"] = new() { Service = "testservice", ID = "f3505103-bdca-4c48-aedb-85cfcd7a7275" },
                ["testservice:a1ad595a-ef00-4d80-a2d5-233e445eda20"] = new() { Service = "testservice", ID = "a1ad595a-ef00-4d80-a2d5-233e445eda20" }
            };

            var expectedLoadBalancedResult = registeredServices.ElementAt(2).Value;

            consulServiceMock.Setup(mock => mock.ListServicesByNameAsync(registeredServiceName, cancellationToken)).ReturnsAsync(registeredServices);
            loadBalancerMock.Setup(mock => mock.LoadBalance(registeredServices)).Returns(expectedLoadBalancedResult);

            // Act
            var loadBalancedResult = await registryProvider.GetAsync(registeredServiceName, cancellationToken);

            // Assert
            consulServiceMock.Verify(mock => mock.ListServicesByNameAsync(registeredServiceName, cancellationToken), Times.Once);
            expectedLoadBalancedResult.Should().BeEquivalentTo(expectedLoadBalancedResult);
            loadBalancerMock.Verify(mock => mock.LoadBalance(registeredServices), Times.Once);
        }

        [Fact]
        public async Task GetAsync_NoServiceWithGivenNameHaveBeenRegistered_ReturnNull()
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            string registeredServiceName = "RegisteredServiceName";

            var registeredServices = new Dictionary<string, ServiceAgent>();

            consulServiceMock.Setup(mock => mock.ListServicesByNameAsync(registeredServiceName, cancellationToken)).ReturnsAsync(registeredServices);

            // Act
            var loadBalancedResult = await registryProvider.GetAsync(registeredServiceName, cancellationToken);

            // Assert
            Assert.Null(loadBalancedResult);
            consulServiceMock.Verify(mock => mock.ListServicesByNameAsync(registeredServiceName, cancellationToken), Times.Once);
            loadBalancerMock.Verify(mock => mock.LoadBalance(registeredServices), Times.Never);
        }

    }
}
