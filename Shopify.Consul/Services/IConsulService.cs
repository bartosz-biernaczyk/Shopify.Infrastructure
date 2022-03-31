using Shopify.Consul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Consul.Services
{
    public interface IConsulService
    {
        Task<HttpResponseMessage> RegisterAsync(ServiceDetails serviceDetails, CancellationToken token);
        Task<HttpResponseMessage> DeregisterAsync(string serviceId, CancellationToken token);
        Task<IDictionary<string, ServiceAgent>> ListServicesAsync(string name, CancellationToken token);
    }
}
