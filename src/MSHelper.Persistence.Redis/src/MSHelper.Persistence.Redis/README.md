# MSHelper.Persistence.Redis : Redis Integration.
:star: Star us on GitHub — it motivates us a lot!

## Overview
Adds the [Redis](https://redis.io/) integration with .NET Core based on [IDistributedCache](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.distributed.idistributedcache) abstraction.ion with .NET Core.

## Installation

This document is for the latest MSHelper.Persistence.Redis **1.0.0 release and later**.

`dotnet add package MSHelper.Persistence.Redis`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)


## Usage
Extend `IMSHelperBuilder` with `AddRedis()` that will register the required services.

```
public static IMSHelperBuilder RegisterMSHelper(this IMSHelperBuilder builder)
{
    builder.AddRedis();
    // Other services.
    
    return builder;
}

```
In order to use Redis integration, inject built-in `IDistributedCache` interface.

```
public class SomeService
{
    private readonly IDistributedCache _cache;

    public SomeService(IDistributedCache cache)
    {
        _cache = cache;
    }
}

```

## Options

--- connectionString - connection string e.g. localhost.

--- instance - optional prefix, that will be added by default to all the keys.

### appsettings.json

```
"redis": {
  "connectionString": "localhost",
  "instance": "some-service:"
}

```



##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
