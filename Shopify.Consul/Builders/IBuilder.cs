namespace Shopify.Consul.Builders
{
    public interface IBuilder<TOut>
    {
        TOut Build();
    }
}
