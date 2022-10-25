# MSHelper.Metrics.Prometheus : Prometheus Integration
:star: Star us on GitHub — it motivates us a lot!

## Overview
Adds capability of generating application metrics and exposing them via HTTP endpoint.

## Installation

This document is for the latest MSHelper.Metrics.Prometheus **1.0.0 release and later**.

`dotnet add package MSHelper.Metrics.Prometheus`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)


## Options
--- enabled - determines whether metrics endpoint is going to be available.

--- influxEnabled - if true metrics will be reported to InfluxDB.

--- prometheusEnabled - if true metrics will be formatted using Prometheus data model.

--- prometheusFormatter - if set to protobuf then protobuf output formatter is going to be used. Otherwise, text output formatter is picked.

--- InfluxUrl - connection string to InfluxDB instance e.g. http://localhost:8086. Required only if influxEnabled is true.

--- database - InfluxDB database name. Required only if influxEnabled is true.

--- interval - InfluxDB flush interval expressed in seconds. Required only if influxEnabled is true.

### appsettings.json

```
"prometheus": {
  "enabled": true,
  "endpoint": "/metrics"
}

```

### Usage
Inside Startup.cs extend `IMSHelperBuilder` with `AddMetrics()` and `IApplicationBuilder` with `UseMetrics()`:


```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddMetrics();

    //other registrations    
    return builder.Build();
}

public void Configure(this IApplicationBuilder app)
{
    app.UseMetrics();
}

```

The above code registers all required services and exposes some default application metrics at two HTTP endpoints:

--- http://host/metrics - depending on prometheusEnabled option, this endpoint exposes metrics that are either formatted using Prometheus data model or using standard, text formatter
--- http://host/metrics-text - this endpoint exposes metrics that are always formated using standard, text formatter

### Creating custom metrics

Once you finish MSHelper registration, you can create custom application metrics using the [AppMetrics](https://www.app-metrics.io/) library for .NET Core. There are six different metrics types you can use:

-- Gauges

-- Counters

-- Meters

-- Histograms

-- Timers

-- Apdex


To create a custom metrics inject `IMetricsRoot` into selected class:

```
public class MetricsRegistry
{
    private readonly IMetricsRoot _metricsRoot;
    private readonly CounterOptions _findDiscountsQueries = new CounterOptions { Name = "find-discount" };

    public MetricsRegistry(IMetricsRoot metricsRoot)
        => _metricsRoot = metricsRoot;
    
    public void IncrementFindDiscountsQuery()
        => _metricsRoot.Measure.Counter.Increment(_findDiscountsQueries);
}

```


##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
