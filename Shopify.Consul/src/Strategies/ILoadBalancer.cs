using Shopify.Consul.Models;
using System.Collections.Generic;

namespace Shopify.Consul.Strategies
{
    public interface ILoadBalancer
    {
        ServiceAgent LoadBalance(IDictionary<string, ServiceAgent> listedServices);
    }
}
