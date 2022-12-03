namespace Shopify.Consul.Exceptions
{
    internal class ValidationException : ConsulException
    {
        public string ParameterName { get; }

        public ValidationException(string parameterName) : base("{0} is incorrect.", parameterName)
        {
            this.ParameterName = parameterName;
        }
    }

}
