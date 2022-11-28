namespace Shopify.Consul.Exceptions
{
    internal abstract class ConsulException : Exception
    {
        public ConsulException(string message) : base(message)
        {

        }

        public ConsulException(string message, params object[]? args) : this(string.Format(message, args))
        {
        }
    }
}
