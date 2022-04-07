namespace Shopify.Consul.Builders
{
    public interface IConsulEndpointOptionsBuilder
    {
        IConsulOptionsBuilderFinal WithTimeSettings(TimeSpan pingInterval, TimeSpan timeout, TimeSpan deregisterAfter);
    }
}
