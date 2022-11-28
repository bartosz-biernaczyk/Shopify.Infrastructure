namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class Connect
    {
        public bool Native { get; set; }
        public SidecarService ServiceDefinition { get; set; }
    };
#else
    public record Connect(bool Native, SidecarService ServiceDefinition);
#endif



}

