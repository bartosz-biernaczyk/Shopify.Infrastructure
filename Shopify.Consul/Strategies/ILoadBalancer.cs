using Shopify.Consul.Models;

namespace Shopify.Consul.Strategies
{
    public interface ILoadBalancer
    {
        ServiceAgent? LoadBalance(IDictionary<string, ServiceAgent> listedServices);
    }
}
