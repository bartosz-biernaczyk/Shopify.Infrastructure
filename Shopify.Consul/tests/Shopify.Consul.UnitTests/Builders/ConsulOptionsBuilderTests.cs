using Shopify.Consul.Builders;
using Shopify.Consul.Options;

namespace Shopify.Consul.UnitTests.Builders
{
    public class ConsulOptionsBuilderTests
    {
        private readonly ConsulOptionsBuilder builder;

        public ConsulOptionsBuilderTests() => builder = new ConsulOptionsBuilder();

        [Fact]
        public void Build_WhenCalled_ReturnCopyOfSettings()
        {
            // Act
            var options = builder.Build();

            options.Enabled = true;

            // Assert
            Assert.False(builder.Build().Enabled);
        }

        [Fact]
        public void Enable_WhenCalled_SetCorrectData()
        {
            // Arrange
            var consulUri = new Uri("http://consul-host:8500");

            // Act
            builder.Enable(consulUri);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.True(options.Enabled);
            Assert.Equal(consulUri.ToString(), options.ConsulUrl);
        }

        [Fact]
        public void UseHttpClient_WhenCalledWithoutClientKey_SetDefaultName()
        {
            // Act
            builder.UseHttpClient();

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.True(options.UseHttpClient);
            Assert.Equal("consul", options.ClientKey);
        }

        [Fact]
        public void UseHttpClient_WhenCalledWithCustomClientKey_SetCustomName()
        {
            // Arrange
            string expectedCustomKey = "consulCustomKey";

            // Act
            builder.UseHttpClient(expectedCustomKey);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.True(options.UseHttpClient);
            Assert.Equal(expectedCustomKey, options.ClientKey);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("address")]
        public void WithAddress_WhenCalledWithAnyAddress_SetCorrectData(string address)
        {
            // Act
            builder.WithAddress(address);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.Equal(address, options.ServiceAddress);
        }

        [Theory]
        [InlineData("")]
        [InlineData("serviceName")]
        [InlineData("Name")]
        [InlineData("ServiceName1")]
        public void WithName_WhenCalledWithAnyName_SetLoweredServiceName(string name)
        {
            // Act
            builder.WithName(name);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.Equal(name.ToLower(), options.ServiceName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("endpoint")]
        public void WithPingEndpoint_WhenCalled_SetLoweredServiceName(string endpoint)
        {
            // Act
            builder.WithPingEndpoint(endpoint);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.Equal(endpoint, options.PingUrl);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(555)]
        [InlineData(55555)]
        public void WithPort_WhenCalled_SetCorrectPort(int port)
        {
            // Act
            builder.WithPort(port);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.Equal(port, options.Port);
        }

        [Fact]
        public void WithTimeSettings_WhenCalled_SetCorrectPort()
        {
            // Arrange
            TimeSpan expectedPingInterval = TimeSpan.FromSeconds(10);
            TimeSpan expectedTimeout = TimeSpan.FromSeconds(15);
            TimeSpan expectedDeregisterAfter = TimeSpan.FromSeconds(20);

            // Act
            builder.WithTimeSettings(expectedPingInterval, expectedTimeout, expectedDeregisterAfter);

            // Assert
            var options = GetOptions(builder);

            Assert.NotNull(options);
            Assert.Equal(expectedPingInterval, options.PingInterval);
            Assert.Equal(expectedTimeout, options.Timeout);
            Assert.Equal(expectedDeregisterAfter, options.DeregisterAfter);
        }


        private static ConsulOptions GetOptions(ConsulOptionsBuilder builder)
            => builder.Build();
    }
}
