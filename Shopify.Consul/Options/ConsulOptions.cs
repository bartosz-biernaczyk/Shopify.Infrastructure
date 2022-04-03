namespace Shopify.Consul.Options
{
    public class ConsulOptions
    {
        public string? Name { get; internal set; }
        public string? Address { get; internal set; }
        public int Port { get; internal set; }
        public string[]? Tags { get; internal set; }
        public string? PingEndpoint { get; internal set; }
        public TimeSpan PingInterval { get; internal set; }
        public TimeSpan Timeout { get; internal set; }
        public TimeSpan DeregisterAfter { get; internal set; }
        public bool Enabled { get; internal set; }
        public bool UseHttpClient { get; internal set; }
        public string? ClientKey { get; internal set; }
    }
}
