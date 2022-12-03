using System;
using System.Runtime.CompilerServices;

namespace Shopify.Consul.Options
{
    public class ConsulOptions
    {
        public bool Enabled { get; set; }
        public string ConsulUrl { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int Port { get; set; }
        public string[] Tags { get; set; }
        public string PingUrl { get; set; }
        public TimeSpan PingInterval { get; set; }
        public TimeSpan Timeout { get; set; }
        public TimeSpan DeregisterAfter { get; set; }
        public bool UseHttpClient { get; set; }
        public string ClientKey { get; set; }

        internal ConsulOptions Copy()
        {
            return new ConsulOptions()
            {
                Enabled = Enabled,
                ConsulUrl = ConsulUrl,
                ServiceName = ServiceName,
                ServiceAddress = ServiceAddress,
                Port = Port,
                Tags = Tags,
                PingUrl = PingUrl,
                PingInterval = PingInterval,
                Timeout = Timeout,
                DeregisterAfter = DeregisterAfter,
                UseHttpClient = UseHttpClient,
                ClientKey = ClientKey
            };
        }
    }
}
