using Newtonsoft.Json;
using Shopify.Consul.Exceptions;
using Shopify.Consul.Extensions;
using Shopify.Consul.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shopify.Consul.Services
{
    public class ConsulService : IConsulService
    {
        private readonly HttpClient httpClient;
        private const string MediaType = "application/json";
        private const string Version = "v1";

        public ConsulService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterAsync(ServiceDetails serviceDetails, CancellationToken cancellationToken)
        {
            if (serviceDetails is null)
            {
                throw new ValidationException(nameof(serviceDetails));
            }

            return await httpClient.PutAsync(HttpUtils.BuildUri($"{Version}/agent/service/register"), PrepareRequestPayload(serviceDetails), cancellationToken);
        }

        public async Task<HttpResponseMessage> DeregisterAsync(string serviceId, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(serviceId))
            {
                throw new ValidationException(nameof(serviceId));
            }

            return await httpClient.PutAsync(HttpUtils.BuildUri($"{Version}/agent/service/deregister/{serviceId}"), PrepareRequestPayload(new { }), token);
        }

        public async Task<IDictionary<string, ServiceAgent>> ListServices(CancellationToken token)
            => await ListFilteredServices(null, token);

        public async Task<IDictionary<string, ServiceAgent>> ListServicesByNameAsync(string serviceName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ValidationException(nameof(serviceName));
            }

            return await ListFilteredServices(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("filter", $"Service=={serviceName}") }, cancellationToken);
        }

        private async Task<IDictionary<string, ServiceAgent>> ListFilteredServices(KeyValuePair<string, string>[] queryParameters, CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync(HttpUtils.BuildUri($"{Version}/agent/services", queryParameters), cancellationToken);

            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<IDictionary<string, ServiceAgent>>(await response.Content.ReadAsStringAsync())
                : new Dictionary<string, ServiceAgent>();
        }

        private static StringContent PrepareRequestPayload(object payload)
            => new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, MediaType);
    }
}


