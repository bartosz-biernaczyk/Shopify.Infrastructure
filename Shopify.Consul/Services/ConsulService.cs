using Newtonsoft.Json;
using Shopify.Consul.Models;
using System.Text;

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

        public async Task<HttpResponseMessage> DeregisterAsync(string serviceId, CancellationToken token)
            => await httpClient.PutAsync($"{Version}/agent/service/deregister/{serviceId}", PrepareRequestPayload(new()), token);

        public async Task<IDictionary<string, ServiceAgent>> ListServicesAsync(string name, CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync($"{Version}/agent/services?filter=Service==\"{name}\"", cancellationToken);

            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<IDictionary<string, ServiceAgent>>(await response.Content.ReadAsStringAsync(cancellationToken))!
                : new Dictionary<string, ServiceAgent>();
        }

        public async Task<HttpResponseMessage> RegisterAsync(ServiceDetails serviceDetails, CancellationToken cancellationToken)
            => await httpClient.PutAsync($"{Version}/agent/service/register", PrepareRequestPayload(serviceDetails), cancellationToken);

        private static StringContent PrepareRequestPayload(object payload)
        {
            var serializedContent = JsonConvert.SerializeObject(payload);
            var requestPayload = new StringContent(serializedContent, Encoding.UTF8, MediaType);
            return requestPayload;
        }
    }
}


