# MSHelper.LoadBalancing.Fabio : Load Balancning integration with fabio
:star: Star us on GitHub — it motivates us a lot!

## Overview
Provides `FabioMessageHandler` (used by `IHttpClient`) that integrates with [Fabio](https://github.com/fabiolb/fabio) load balancer. In order to use Fabio, it is required to configure Consul as described above.

## Installation

This document is for the latest MSHelper.LoadBalancing.Fabio **1.0.0 release and later**.

`dotnet add package MSHelper.LoadBalancing.Fabio`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

-- [MSHelper.HTTP](https://www.nuget.org/packages/MSHelper.HTTP)

-- [MSHelper.Discovery.Consul](https://www.nuget.org/packages/MSHelper.Discovery.Consul)

## Usage
Extend `IMSHelperBuilder` with `AddFabio()` that will register the required services.

```
public static IMSHelperBuilder RegisterMSHelper(this IMSHelperBuilder builder)
{
    builder
        .AddHttpClient()
        .AddConsul()
        .AddFabio();
    // Other services.

    return builder;
}

```

## Options

--- enabled - determines whether Fabio integration is going to be available.

--- url - URL of the Fabio service.

--- service - name of the service group used for the Consul registration.


### appsettings.json

```
"fabio": {
  "enabled": true,
  "url": "http://localhost:9999",
  "service": "some-service"
}

```


##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
