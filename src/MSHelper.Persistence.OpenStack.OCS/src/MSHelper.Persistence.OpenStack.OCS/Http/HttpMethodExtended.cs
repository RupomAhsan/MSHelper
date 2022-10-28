using System.Net.Http;

namespace MSHelper.Persistence.OpenStack.OCS.Http;

internal static class HttpMethodExtended
{
    public static HttpMethod Copy => new("COPY");
}