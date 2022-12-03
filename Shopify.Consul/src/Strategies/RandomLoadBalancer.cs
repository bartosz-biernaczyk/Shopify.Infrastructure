using Shopify.Consul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Shopify.Consul.UnitTests")]
namespace Shopify.Consul.Strategies
{
    internal class RandomLoadBalancer : ILoadBalancer
    {
        private static readonly Random random = new Random();

        public ServiceAgent LoadBalance(IDictionary<string, ServiceAgent> listedServices)
        => listedServices?.Values.ElementAt(random.Next(0, listedServices.Count));
    }
}
