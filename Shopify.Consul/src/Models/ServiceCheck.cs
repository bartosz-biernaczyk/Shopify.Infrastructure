namespace Shopify.Consul.Models
{

#if NETSTANDARD2_0_OR_GREATER
    public class ServiceCheck
    {
        public string Interval { get; set; }
        public string HTTP { get; set; }
        public string Method { get; set; }
        public int SuccessBeforePassing { get; set; }
        public int FailuresBeforeWarning { get; set; }
        public int FailuresBeforeCritical { get; set; }
        public string DeregisterCriticalServiceAfter { get; set; }
        public string Timeout { get; set; }
    }
#elif nullable
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
#else
    public record ServiceCheck
    {
        public string Interval { get; set; }
        public string HTTP { get; set; }
        public string Method { get; set; }
        public int SuccessBeforePassing { get; set; }
        public int FailuresBeforeWarning { get; set; }
        public int FailuresBeforeCritical { get; set; }
        public string DeregisterCriticalServiceAfter { get; set; }
        public string Timeout { get; set; }
    }
#endif

}
