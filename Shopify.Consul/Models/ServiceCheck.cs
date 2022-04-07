namespace Shopify.Consul.Models
{
    public record ServiceCheck
    {
        public string? Interval { get; set; }
        public string? HTTP { get; set; }
        public string? Method { get; set; }
        public int SuccessBeforePassing { get; set; }
        public int FailuresBeforeWarning { get; set; }
        public int FailuresBeforeCritical { get; set; }
        public string? DeregisterCriticalServiceAfter { get; set; }
        public string? Timeout { get; set; }
    }
}
