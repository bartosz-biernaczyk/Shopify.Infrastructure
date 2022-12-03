namespace Shopify.Consul.UnitTests.Mocks
{
    internal class MockedHttpMessageHandler : HttpMessageHandler
    {
        private Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> onSendBehavior;

        public MockedHttpMessageHandler()
        {
        }

        public MockedHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> func)
        {
            onSendBehavior = func;
        }

        public void SetBehavior(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> func)
        {
            this.onSendBehavior = func;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return onSendBehavior(request, cancellationToken);
        }
    }
}
