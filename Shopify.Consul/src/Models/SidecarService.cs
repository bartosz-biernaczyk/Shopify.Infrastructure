namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class SidecarService
    {
        public Proxy Proxy { get; set; }
    }
#else
    public record SidecarService(Proxy Proxy);
#endif
}

