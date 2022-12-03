namespace Shopify.Consul.Models
{
#if NETSTANDARD2_0_OR_GREATER
    public class Weight
    {
        public int Passing { get; set; }
        public int Warning { get; set; }
    }
#else
    public record Weight(int Passing, int Warning);
#endif
}
