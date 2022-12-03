using System;

namespace Shopify.Consul.Builders
{
    public interface IConsulOptionsBuilderInitial
    {
        IConsulOptionsBuilderServiceAddressStage Enable(Uri consulUri);
    }
}
