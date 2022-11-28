using System.Runtime.Serialization;

namespace Shopify.Consul.Options
{
    public class ConsulOptions
    {
        public bool Enabled { get; set; }
        public string? ConsulUrl { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceAddress { get; set; }
        public int Port { get; set; }
        public string[]? Tags { get; set; }
        public string? PingUrl { get; set; }
        public TimeSpan PingInterval { get; set; }
        public TimeSpan Timeout { get; set; }
        public TimeSpan DeregisterAfter { get; set; }
        public bool UseHttpClient { get; set; }
        public string? ClientKey { get; set; }
    }
}
