using Shopify.Consul.Models;

namespace Shopify.Consul.Services
{
    public interface IServiceRegistryProvider
    {
        Task<ServiceAgent?> GetAsync(string name, CancellationToken token);
    }
}
