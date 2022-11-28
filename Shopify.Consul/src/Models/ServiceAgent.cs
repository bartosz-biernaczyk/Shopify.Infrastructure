namespace Shopify.Consul.Models
{
    public record ServiceAgent
    {
        public string? ID { get; set; }
        public string? Service { get; set; }
        public int Port { get; set; }
        public string[]? Tags { get; set; }
        public string? Address { get; set; }
        public Weight? Weights { get; set; }
    }
}
