namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class ServiceAgent
    {
        public string ID { get; set; }
        public string Service { get; set; }
        public int Port { get; set; }
        public string[] Tags { get; set; }
        public string Address { get; set; }
        public Weight Weights { get; set; }
    }
#elif nullable
    public record ServiceAgent
    {
        public string? ID { get; set; }
        public string? Service { get; set; }
        public int Port { get; set; }
        public string[]? Tags { get; set; }
        public string? Address { get; set; }
        public Weight? Weights { get; set; }
    }
#else
    public record ServiceAgent
    {
        public string ID { get; set; }
        public string Service { get; set; }
        public int Port { get; set; }
        public string[] Tags { get; set; }
        public string Address { get; set; }
        public Weight Weights { get; set; }
    }
#endif
}
