using Shopify.Consul.Options;

namespace Shopify.Consul.Builders
{
    public interface IConsulOptionsBuilderFinal : IBuilder<ConsulOptions>
    {
        IConsulEndpointOptionsBuilder WithPingEndpoint(string pingEndpoint);
        IConsulOptionsBuilderFinal UseHttpClient(string key = "consul");
    }
}
