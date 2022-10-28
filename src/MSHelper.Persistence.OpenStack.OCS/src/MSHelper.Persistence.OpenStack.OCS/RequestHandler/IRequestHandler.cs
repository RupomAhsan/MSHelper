using System;
using System.Threading.Tasks;
using MSHelper.Persistence.OpenStack.OCS.Http;

namespace MSHelper.Persistence.OpenStack.OCS.RequestHandler;

internal interface IRequestHandler
{
    Task<HttpRequestResult> Send(Func<IHttpRequestBuilder, IHttpRequestBuilder> httpRequestBuilder);
}