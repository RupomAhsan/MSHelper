using System.Net.Http;
using MSHelper.HTTP;

namespace MSHelper.Discovery.Consul.Http;

internal sealed class ConsulHttpClient : MSHelperHttpClient, IConsulHttpClient
{
    public ConsulHttpClient(HttpClient client, HttpClientOptions options, IHttpClientSerializer serializer,
        ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
        : base(client, options, serializer, correlationContextFactory, correlationIdFactory)
    {
    }
}