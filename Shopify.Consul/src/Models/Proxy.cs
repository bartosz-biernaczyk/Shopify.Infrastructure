namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class Proxy
    {
        Upstream[] Upstreams { get; set; }
    }
#else
    public record Proxy(Upstream[] Upstreams);
#endif

}

