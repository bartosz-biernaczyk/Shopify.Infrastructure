using Shopify.Consul.Services;

namespace Shopify.Consul.Http
{
    public class ServiceDiscoveryMessageHandler : DelegatingHandler
    {
        private readonly IServiceRegistryProvider serviceRegistryProvider;

        public ServiceDiscoveryMessageHandler(IServiceRegistryProvider serviceRegistryProvider)
        {
            this.serviceRegistryProvider = serviceRegistryProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = await GetDiscoveredServiceUriAsync(request.RequestUri!, cancellationToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<Uri?> GetDiscoveredServiceUriAsync(Uri source, CancellationToken cancellationToken)
        {
            var serviceAgent = await serviceRegistryProvider.GetAsync(source.Host, cancellationToken);

            if (serviceAgent is null)
            {
                return null;
            }

            return BuildUri(source, serviceAgent.Address!, serviceAgent.Port);
        }

        private static Uri BuildUri(Uri source, string host, int port)
        {
            var builder = new UriBuilder(source)
            {
                Host = host,
                Port = port
            };

            return builder.Uri;
        }
    }
}
