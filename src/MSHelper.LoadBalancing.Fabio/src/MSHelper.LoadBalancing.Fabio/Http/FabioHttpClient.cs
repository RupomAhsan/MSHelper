using System.Net.Http;
using MSHelper.HTTP;

namespace MSHelper.LoadBalancing.Fabio.Http;

internal sealed class FabioHttpClient : MSHelperHttpClient, IFabioHttpClient
{
    public FabioHttpClient(HttpClient client, HttpClientOptions options, IHttpClientSerializer serializer,
        ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
        : base(client, options, serializer, correlationContextFactory, correlationIdFactory)
    {
    }
}