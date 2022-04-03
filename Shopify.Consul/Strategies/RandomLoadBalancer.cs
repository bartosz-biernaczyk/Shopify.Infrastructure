using Shopify.Consul.Models;

namespace Shopify.Consul.Strategies
{
    public class RandomLoadBalancer : ILoadBalancer
    {
        private static readonly Random random = new();

        public ServiceAgent LoadBalance(IDictionary<string, ServiceAgent> listedServices)
        => listedServices.Values.ElementAt(random.Next(0, listedServices.Count));
    }
}
