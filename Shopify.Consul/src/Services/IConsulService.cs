using Shopify.Consul.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shopify.Consul.Services
{
    public interface IConsulService
    {
        Task<HttpResponseMessage> RegisterAsync(ServiceDetails serviceDetails, CancellationToken token);
        Task<HttpResponseMessage> DeregisterAsync(string serviceId, CancellationToken token);
        Task<IDictionary<string, ServiceAgent>> ListServices(CancellationToken token);
        Task<IDictionary<string, ServiceAgent>> ListServicesByNameAsync(string name, CancellationToken token);
    }
}
