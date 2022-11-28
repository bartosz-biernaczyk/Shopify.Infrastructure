using Shopify.Consul.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Shopify.Consul.Services
{
    public interface IServiceRegistryProvider
    {
        Task<ServiceAgent> GetAsync(string name, CancellationToken token);
    }
}
