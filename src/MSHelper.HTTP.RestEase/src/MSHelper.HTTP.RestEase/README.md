# MSHelper.Discovery.Consul : Service Discovery
:star: Star us on GitHub — it motivates us a lot!

## Overview
Provides `ConsulServiceDiscoveryMessageHandle`r (used by `IHttpClient`) that integrates with Consul service discovery mechanism.


## Installation

This document is for the latest MSHelper.Discovery.Consul **1.0.0 release and later**.

`dotnet add package MSHelper.Discovery.Consul`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

-- [MSHelper.HTTP](https://www.nuget.org/packages/MSHelper.HTTP)

## Usage
Extend `IMSHelperBuilder` with `AddConsul()` that will register the required services.

```
public static IMSHelperBuilder RegisterMSHelper(this IMSHelperBuilder builder)
{
    builder
        .AddHttpClient()
        .AddConsul();
    // Other services.

    return builder;
}

```

## Options

--- enabled - determines whether Consul integration is going to be available.

--- url - URL of the Consul service.

--- service - name of the service group (multiple instances of the same service will use the same service name).

--- address - address of the service.

--- port - port under which the service is available.

--- pingEnabled - register health checks from Consul to validate the service availability (if the service will be offline, it will be removed after the pingInterval and removeAfterInterval timeouts).

--- pingEndpoint - an endpoint that should be called when performing the healt check by Consul.

### appsettings.json

```
"consul": {
  "enabled": true,
  "url": "http://localhost:8500",
  "service": "some-service",
  "address": "localhost",
  "port": "5000",
  "pingEnabled": true,
  "pingEndpoint": "ping",
  "pingInterval": 3,
  "removeAfterInterval": 3
}

```


##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
