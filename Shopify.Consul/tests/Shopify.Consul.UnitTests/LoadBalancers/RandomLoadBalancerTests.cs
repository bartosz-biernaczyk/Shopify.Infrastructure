using FluentAssertions;
using Shopify.Consul.Models;
using Shopify.Consul.Strategies;

namespace Shopify.Consul.UnitTests.LoadBalancers
{
    public class RandomLoadBalancerTests
    {
        private RandomLoadBalancer loadBalancer;

        public RandomLoadBalancerTests()
        {
            loadBalancer = new RandomLoadBalancer();
        }

        [Fact]
        public void LoadBalance_CollectionWithOneItem_ReturnSingleItem()
        {
            // Arrange
            var serviceKey = "key";
            var registeredServices = new Dictionary<string, ServiceAgent>()
            {
                [serviceKey] = new ServiceAgent() { Service = "test"}
            };

            // Act
            var result = loadBalancer.LoadBalance(registeredServices);

            // Assert
            result.Should().BeEquivalentTo(registeredServices[serviceKey]);
        }

        [Fact]
        public void LoadBalance_CollectionWithMultipleItems_ReturnSingleItem()
        {
            // Arrange
            
            var registeredServices = new Dictionary<string, ServiceAgent>()
            {
                ["key1"] = new ServiceAgent() { Service = "Service1" },
                ["key2"] = new ServiceAgent() { Service = "Service2" },
                ["key3"] = new ServiceAgent() { Service = "Service3" }
            };

            // Act
            var result = loadBalancer.LoadBalance(registeredServices);

            // Assert
            Assert.NotNull(result);
        }
    }
}