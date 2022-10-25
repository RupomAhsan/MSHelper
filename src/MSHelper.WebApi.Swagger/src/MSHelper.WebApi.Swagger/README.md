# MSHelper.WebApi.Swagger : Swagger Integration
:star: Star us on GitHub — it motivates us a lot!

# Overview
To integrate Swagger documentation on top of Web API defined as a set of endpoints without the usage of full AddMvc() and using custom Controllers, it is required to install this package.

## Installation

This document is for the latest MSHelper.WebApi.Swagger **1.0.0 release and later**.

`dotnet add package MSHelper.WebApi.Swagger`

## Dependencies

-- [MSHelper.Swagger](https://www.nuget.org/packages/MSHelper.Swagger)

-- [MSHelper.WebApi](https://www.nuget.org/packages/MSHelper.WebApi)

## Usage
Invoke `AddWebApiSwaggerDocs()` and then `UseSwaggerDocs()` to use Swagger.

```
public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddMSHelper()
            .AddWebApi()
            .AddWebApiSwaggerDocs()
            .Build())
        .Configure(app => app..UseSwaggerDocs())
```
  


##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
