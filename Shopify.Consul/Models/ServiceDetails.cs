namespace Shopify.Consul.Models
{
    public record ServiceDetails
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string[]? Tags { get; set; }
        public string? Address { get; set; }
        public string? Kind { get; set; } = string.Empty;
        public int Port { get; set; }
        public ServiceCheck[]? Checks { get; set; }
        public Weight? Weights { get; set; }
        public Connect? Connect { get; set; }
    }

}

