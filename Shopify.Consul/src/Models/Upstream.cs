namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class Upstream
    {
        public string DestinationName { get; set; }
        public int LocalBindPort { get; set; }
    }
#else
    public record Upstream(string DestinationName, int LocalBindPort);
#endif

}

