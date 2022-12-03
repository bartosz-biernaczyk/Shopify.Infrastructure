using Moq;
using Shopify.Consul.Services;
using Shopify.Consul.UnitTests.Mocks;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopify.Consul.UnitTests.Services
{
    public class ConsulServiceTests
    {
        private const string ServiceUri = "http://localhost:5000";

        private readonly IConsulService service;
        private readonly HttpClient httpClient;
        private readonly MockedHttpMessageHandler mockedHandler;

        public ConsulServiceTests()
        {
            mockedHandler = new MockedHttpMessageHandler();
            httpClient = new HttpClient(mockedHandler)
            {
                BaseAddress = new(ServiceUri)
            };

            service = new ConsulService(httpClient);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        public async Task DeregisterAsync_WithInvalidServiceId_ShouldThrow(string invalidServiceId)
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAnyAsync<Exceptions.ValidationException>(() => service.DeregisterAsync(invalidServiceId, cancellationToken));
        }

        [Theory]
        [InlineData("valid-service-id")]
        [InlineData("s_id")]
        [InlineData("ServiceId")]
        public async Task DeregisterAsync_WithValidServiceId_MakeHttpCallToCorrectEndpointWithLoweredServiceId(string validServiceId)
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            HttpRequestMessage? sentRequest = null;
            
            mockedHandler.SetBehavior((actualRequest, _) => 
            { 
                sentRequest = actualRequest; 

                return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)); 
            });

            // Act
            _ = await service.DeregisterAsync(validServiceId, cancellationToken);

            // Assert
            Assert.NotNull(sentRequest);
            Assert.Equal(sentRequest!.Method, HttpMethod.Put);
            Assert.Equal(sentRequest!.RequestUri!.OriginalString, $"{ServiceUri}/v1/agent/service/deregister/{validServiceId.ToLower()}");
        }

        [Fact]
        public async Task ListServicesAsync_NoServiceIsRegistered_MakeHttpCall()
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            HttpRequestMessage? sentRequest = null;

            mockedHandler.SetBehavior((actualRequest, _) =>
            {
                sentRequest = actualRequest;

                return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
            });

            // Act
            _ = await service.ListServicesAsync(cancellationToken);

            // Assert
            Assert.NotNull(sentRequest);
            Assert.Equal(sentRequest!.Method, HttpMethod.Get);
            Assert.Equal(sentRequest!.RequestUri!.OriginalString, $"{ServiceUri}/v1/agent/services");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("    ")]
        public async Task ListServicesByNameAsync_InvalidServiceName_ThrowException(string invalidServiceName)
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAsync<Exceptions.ValidationException>(() => service.ListServicesByNameAsync(invalidServiceName, cancellationToken));
        }

        [Theory]
        [InlineData("b")]
        [InlineData("13ba$%")]
        [InlineData("ServiceName")]
        [InlineData("Test_@Service Name")]
        public async Task ListServicesByNameAsync_ValidServiceName_MakeHttpCallWithLoweredServiceName(string validServiceName)
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            HttpRequestMessage? sentRequest = null;

            mockedHandler.SetBehavior((actualRequest, _) =>
            {
                sentRequest = actualRequest;

                return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
            });

            // Act
            _ = await service.ListServicesByNameAsync(validServiceName, cancellationToken);

            // Assert

            Assert.NotNull(sentRequest);
            Assert.Equal(sentRequest!.Method, HttpMethod.Get);
            Assert.Equal(sentRequest!.RequestUri!.OriginalString, @$"{ServiceUri}/v1/agent/services?filter=Service=={validServiceName.ToLower()}");
        }

    }
}
