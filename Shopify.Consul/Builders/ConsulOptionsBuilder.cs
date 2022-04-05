using Shopify.Consul.Options;

namespace Shopify.Consul.Builders
{
    public class ConsulOptionsBuilder :
        IConsulOptionsBuilderInitial,
        IConsulOptionsBuilderNameStage,
        IConsulOptionsBuilderFinal,
        IConsulOptionsBuilderPortStage,
        IConsulEndpointOptionsBuilder
    {
        private readonly ConsulOptions options;
        public ConsulOptionsBuilder()
        {
            options = new();
        }

        public ConsulOptions Build() => options;

        public IConsulOptionsBuilderFinal UseHttpClient(string key = "consul")
        {
            options.UseHttpClient = true;
            options.ClientKey = key;
            return this;
        }

        public IConsulOptionsBuilderNameStage WithAddress(string address)
        {
            options.ServiceAddress = address;
            return this;
        }

        public IConsulOptionsBuilderPortStage WithName(string name)
        {
            options.ServiceName = name;
            return this;
        }

        public IConsulEndpointOptionsBuilder WithPingEndpoint(string pingEndpoint)
        {
            options.PingUrl = pingEndpoint;
            return this;
        }

        public IConsulOptionsBuilderFinal WithPort(int port)
        {
            options.Port = port;
            return this;
        }

        public IConsulOptionsBuilderFinal WithTimeSettings(TimeSpan pingInterval, TimeSpan timeout, TimeSpan deregisterAfter)
        {
            options.PingInterval = pingInterval;
            options.Timeout = timeout;
            options.DeregisterAfter = deregisterAfter;
            return this;
        }
    }
}
