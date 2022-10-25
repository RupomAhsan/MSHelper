# MSHelper.HTTP : HTTP
:star: Star us on GitHub — it motivates us a lot!

# Overview
Requests, service discovery, load balancing.
Enhances the built-in HttpClient with an IHttpClient interface with retry policy using Polly and adds a possibility to easily use Consul service discovery and Fabio load balancing mechanisms, as well as switching between the different implementations.

## Installation

This document is for the latest MSHelper.HTTP **1.0.0 release and later**.

`dotnet add package MSHelper.HTTP`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Extend `IMSHelperBuilder` with `AddHttpClient()` that will register the required services.

```
public static IMSHelperBuilder RegisterMSHelper(this IMSHelperBuilder builder)
{
    builder.AddHttpClient();
    // Other services.
    
    return builder;
}
```
  

Then, simply inject `IHttpClient` (and optionally `HttpClientOptions` to resolve services URLS) to execute HTTP requests.

```
public class SomeService
{
    private readonly string _webService1Url;
    private readonly IHttpClient _client;

    public SomeService(IHttpClient _client, HttpClientOptions options)
    {
        _client = _client;
        _webService1Url = options.Services["web-service1"];
    }

    public async Task FooAsync()
    {
        var dto = await _client.GetAsync<Dto>($"{_webService1Url}/data");
    }
}
```

## Usage
--- type - sets the `IHttpClient` message handler, if none is specified then the default handler will be used, other possible values: consul, fabio.
--- retries - number of HTTP request retries using an exponential backoff.
--- services - dictionary (map) of service_name:service_url values that can be used to invoke the other web services without a need to hardcode the configuration URLs, especially useful when service discovery mechanism or load balancer is available.

### appsettings.json

```
"httpClient": {
  "type": "",
  "retries": 2,
  "services": {
    "web-service1": "http://localhost:5050",
    "web-service2": "web-service2-from-dns"
  }
}

```


## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
