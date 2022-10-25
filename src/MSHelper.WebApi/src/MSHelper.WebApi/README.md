# MSHelper.WebApi : Clean and robust API definition.
:star: Star us on GitHub — it motivates us a lot!

# Endpoints
With the usage of Web API package, you can define the endpoints more fluently, without the need of using a full ASP.NET Core MVC package and deriving from Controller. It’s more of an extension of the built-in `IRouteBuilder` abstraction allowing to define routing and deal with HTTP requests.

## Installation

This document is for the latest MSHelper.WebApi **1.0.0 release and later**.

`dotnet add package MSHelper.WebApi`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Extend Program.cs -> `CreateDefaultBuilder()` with `AddWebApi()` that will add the required services.

```
public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddMSHelper()
            .AddWebApi()
            .Build())

```
  

To define custom endpoints, invoke `UseEndpoints()` as the `IApplicationBuilder` extension within `Configure()` method. Then, you can make use of `Get()`, `Post()`, `Put()`, `Delete()` methods.
```
public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddMSHelper()
            .AddWebApi()
            .Build())
        .Configure(app => app
            .UseEndpoints(endpoints => endpoints
                .Get("", ctx => ctx.Response.WriteAsync("Hello"))
                .Get<GetParcel, ParcelDto>("parcels/{parcelId}")
                .Get<GetParcels, IEnumerable<ParcelDto>>("parcels")
                .Delete<DeleteParcel>("parcels/{parcelId}")
                .Post<AddParcel>("parcels", (req, ctx) => ctx.Response.Created($"parcels/{req.ParcelId}"))))

```

As you can see, generic extensions can be used when defining the endpoints (although it’s not required). Whenever you define a generic endpoint with a type T, it will bind the incoming request to the new instance of T (think of it as something similar to command).


To automatically handle the incoming request, you can implement IRequest marker interface for type T and create an `IRequestHandler<T>` that will be invoked automatically.

```
public class DeleteParcel : IRequest
{
    public Guid ParcelId { get; }

    public DeleteParcel(Guid parcelId)
    {
        ParcelId = parcelId;
    }
}

public class DeleteParcelHandler : IRequestHandler<DeleteParcel, int>
{
    public async Task<int> HandleAsync(DeleteParcel request)
    {
        // Deleted a parcel, let's return its ID.
        return request.ParcelId;
    }
}

```


## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
