using Shopify.Consul.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopify.Consul.Strategies
{
    public class RandomLoadBalancer : ILoadBalancer
    {
        private static readonly Random random = new Random();

        public ServiceAgent LoadBalance(IDictionary<string, ServiceAgent> listedServices)
        => listedServices?.Values.ElementAt(random.Next(0, listedServices.Count));
    }
}
