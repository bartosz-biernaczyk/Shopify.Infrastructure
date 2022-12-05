using Shopify.Consul.Constants;
using Shopify.Consul.Options;
using System;

namespace Shopify.Consul.Builders
{
    public class ConsulOptionsBuilder :
        IConsulOptionsBuilderInitial,
        IConsulOptionsBuilderServiceAddressStage,
        IConsulOptionsBuilderNameStage,
        IConsulOptionsBuilderFinal,
        IConsulOptionsBuilderPortStage,
        IConsulEndpointOptionsBuilder
    {
        private readonly ConsulOptions options;
        public ConsulOptionsBuilder()
        {
            options = new ConsulOptions();
        }

        public ConsulOptions Build() => options.Copy();

        public IConsulOptionsBuilderServiceAddressStage Enable(Uri consulUri)
        {
            options.Enabled = true;
            options.ConsulUrl = consulUri.ToString();

            return this;
        }

        public IConsulOptionsBuilderFinal UseHttpClient(string key = ConsulConstants.DefaultHttpKey)
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
            options.ServiceName = name.ToLower();
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

        public IConsulOptionsBuilderFinal WithTags(string[] tags)
        {
            options.Tags = tags;
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
