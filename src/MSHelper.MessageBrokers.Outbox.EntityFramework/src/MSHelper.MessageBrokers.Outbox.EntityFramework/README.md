# MSHelper.MessageBrokers.Outbox.EntityFramework : Outbox Pattern with Entity Framework
:star: Star us on GitHub — it motivates us a lot!

## Overview
Provides exactly-once processing and guaranteed message delivery features based on inbox and outbox patterns. Currently supported storage:

--- In memory (mostly for the testing purposes)

--- SQL using Entity Framework dotnet add package MSHelper.MessageBrokers.Outbox.EntityFramework

--- Mongo dotnet add package MSHelper.MessageBrokers.Outbox.Mongo

## Installation

This document is for the latest MSHelper.MessageBrokers.Outbox.EntityFramework **1.0.0 release and later**.

`dotnet add package MSHelper.MessageBrokers.Outbox.EntityFramework`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

-- [MSHelper.MessageBrokers.Outbox](https://www.nuget.org/packages/MSHelper.MessageBrokers.Outbox)

## Usage

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddMessageOutbox(outbox => outbox.AddMongo());

    //other registrations    
    return builder.Build();
}

```

```
"outbox": {
  "enabled": true,
  "type": "sequential",
  "expiry": 3600,
  "intervalMilliseconds": 2000,
  "inboxCollection": "inbox",
  "outboxCollection": "outbox"
}

```



##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
