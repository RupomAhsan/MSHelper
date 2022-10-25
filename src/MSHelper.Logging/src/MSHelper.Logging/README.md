# MSHelper.Logging : Centralized logging with Serilog and Seq.
:star: Star us on GitHub — it motivates us a lot!

# Overview
Adds the logging capability, by default uses Serilog for logging with 3 optional extensions (sinks):

--- Console
--- File
--- Seq

## Installation

This document is for the latest MSHelper.Logging **1.0.0 release and later**.

`dotnet add package MSHelper.Logging`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Extend Program.cs -> `CreateDefaultBuilder()` with `UseLogging()` that will add the required services and configure `ILogger` available in ASP.NET Core framework.

```
public static IWebHostBuilder GetWebHostBuilder(string[] args)
    => WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services.AddMSHelper().Build())
        .UseLogging();

```
  

Then, simply inject `ILogger<T>` (being ASP.NET Core built-in abstraction) to write the logs.

```
public class SomeService
{
    private readonly ILogger<SomeService> _logger;

    public SomeService(ILogger<SomeService> logger)
    {
        _logger = logger;
    }

    public void Foo()
    {
        _logger.LogInformation("Foo");
    }
}

```

## Options
--- applicationName - sets the optional application name property used for log enrichment.
--- serviceId - sets the optional service id property used for log enrichment.
--- excludePaths - optional endpoints that should be excluded from logging (e.g. while performing the health checks by other services).
--- console.enabled - enables/disables console logger.
--- file.enabled - enables/disables file logger.
--- file.path - path to the file logs.
--- file.interval - how often should the new file with logs be created.
--- seq.enabled - enables/disables Seq logger.
--- seq.url - URL to Seq API.
--- seq.token - API key (if provided) used while sending logs to Seq.

### appsettings.json

```
"logger": {
  "level": "information",
  "excludePaths": ["/", "/ping", "/metrics"],
  "excludeProperties": [
    "api_key",
    "access_key",
    "ApiKey",
    "ApiSecret",
    "ClientId",
    "ClientSecret",
    "ConnectionString",
    "Password",
    "Email",
    "Login",
    "Secret",
    "Token"
  ],
  "console": {
    "enabled": true
  },
  "elk": {
    "enabled": false,
    "url": "http://localhost:9200"
  },
  "file": {
    "enabled": true,
    "path": "logs/logs.txt",
    "interval": "day"
  },
  "seq": {
    "enabled": true,
    "url": "http://localhost:5341",
    "apiKey": "secret"
  },
  "tags": {}
},

```
## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
