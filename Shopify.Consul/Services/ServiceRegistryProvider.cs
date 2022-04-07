using Shopify.Consul.Models;
using Shopify.Consul.Strategies;

namespace Shopify.Consul.Services
{

    public class ServiceRegistryProvider : IServiceRegistryProvider
    {
        private readonly IConsulService consulService;
        private readonly ILoadBalancer loadBalancer;

        public ServiceRegistryProvider(IConsulService consulService, ILoadBalancer? loadBalancer = null)
        {
            this.consulService = consulService;
            this.loadBalancer = loadBalancer ?? new RandomLoadBalancer();
        }

        public async Task<ServiceAgent?> GetAsync(string name, CancellationToken token)
        {
            var services = await consulService.ListServicesAsync(name, token);

            return !services.Any() ? null : loadBalancer.LoadBalance(services);
        }
    }
}
