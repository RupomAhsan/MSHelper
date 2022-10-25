# MSHelper.WebApi.CQRS : CQRS Integration
:star: Star us on GitHub — it motivates us a lot!

# Overview
To seamlessly integrate with command and query handlers that can be invoked either by internal HTTP API call or a message broker (just make sure that you don’t process queries asynchronously, as it doesn’t make much sense), CQRS integration with Web API has to be installed.

# Endpoints
With the usage of Web API package, you can define the endpoints more fluently, without the need of using a full ASP.NET Core MVC package and deriving from Controller. It’s more of an extension of the built-in `IRouteBuilder` abstraction allowing to define routing and deal with HTTP requests.

## Installation

This document is for the latest MSHelper.WebApi.CQRS **1.0.0 release and later**.

`dotnet add package MSHelper.WebApi.CQRS`

## Dependencies

-- [MSHelper.WebApi](https://www.nuget.org/packages/MSHelper.WebApi)

-- [MSHelper.CQRS.Queries](https://www.nuget.org/packages/MSHelper.CQRS.Queries)

-- [MSHelper.CQRS.Commands](https://www.nuget.org/packages/MSHelper.CQRS.Commands)

-- [MSHelper.CQRS.Events](https://www.nuget.org/packages/MSHelper.CQRS.Events)

## Usage
EEnsure that Web API extension is already registered, and change `UseEndpoints()` to `UseDispatcherEndpoints()`. When defining your endpoints, you will notice that now there are 2 additional (and optional) parameters - beforeDispatch and afterDispatch that can be used to alter or enrich the behavior of commands or queries before or after they will be invoked by dispatcher.

```
public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddMSHelper()
            .AddWebApi()
            .Build())
        .Configure(app => app
            .UseDispatcherEndpoints(endpoints => endpoints
                .Get("", ctx => ctx.Response.WriteAsync("Hello"))
                .Get<GetParcel, ParcelDto>("parcels/{parcelId}")
                .Get<GetParcels, IEnumerable<ParcelDto>>("parcels")
                .Delete<DeleteParcel>("parcels/{parcelId}")
                .Post<AddParcel>("parcels",
                    afterDispatch: (cmd, ctx) => ctx.Response.Created($"parcels/{cmd.ParcelId}"))))

```
  

To expose all of the commands and events as a sort of auto-documentation (might be helpful for integration with other services) (similarly to what Swagger does) under a custom endpoint (by default: _contracts) returning an array of commands and events objects using JSON format, invoke `UsePublicContracts<T>()` extension, where T can be a so-called marker attribute used to expose the selected types.

```
public class ContractAttribute : Attribute
{
}

[Contract]
public class DeleteParcel : IRequest
{
    public Guid ParcelId { get; }

    public DeleteParcel(Guid parcelId)
    {
        ParcelId = parcelId;
    }
}

public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddMSHelper()
            .AddWebApi()
            .Build())
        .Configure(app => app.UsePublicContracts<Contract>()
            .UseDispatcherEndpoints(endpoints => endpoints
                // Endpoints definition
            ))

```


#### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
