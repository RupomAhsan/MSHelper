# MSHelper.MessageBrokers.CQRS : CQRS Integration for Asynchronous messaging using RabbitMQ.
:star: Star us on GitHub — it motivates us a lot!

## Overview
MSHelper allows you to integrate asynchronous communication with CQRS principle providing set of extension methods for publishing/subscribing commands and events.

## Installation

This document is for the latest MSHelper.MessageBrokers.CQRS **1.0.0 release and later**.

`dotnet add package MSHelper.MessageBrokers.CQRS`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

-- [MSHelper.MessageBrokers](https://www.nuget.org/packages/MSHelper.MessageBrokers)

-- [MSHelper.CQRS.Commands](https://www.nuget.org/packages/MSHelper.CQRS.Commands)

-- [MSHelper.CQRS.Events](https://www.nuget.org/packages/MSHelper.CQRS.Events)


## Usage 
To subscribe for a particular command or event, invoke `UseRabbitMq()` method on `IApplicationBuilder` and call `SubscribeCommand<TCommand>()` or `SubscribeEvent<TCommand>()`.

```
public void Configure(this IApplicationBuilder app)
{
    app.UseRabbitMq()
       .SubscribeCommand<CreateUser>()
       .SubscribeEvent<UserCreated>();
}

```

Once the message is received, it gets distpatched using `ICommandDispatcher` or `IEventDispatcher`.

To publish a message simply inject `IBusPublsiher` into any class you want and invoke `SendAsync()` (for commands) or `PublishAsync()` (for event) passing the message and correlation context.

```
public class CustomBusPublisher
{
    private readonly IBusPublisher _publisher;

    public CustomBusPublisher(IBusPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task PublishCommandAsync<TCommand>(TCommand command) where T : class, ICommand
        => _publisher.SendAsync(command);

    public Task PublishEventAsync<TEvent>(TEvent @event) where T : class, IEvent
        => _publisher.PublishAsync(@event);
}

```

This package also allows you register async dispatcher instead of “in-memory”. Extend `IMSHelperBuilder` with `AddServiceBusCommandDispatcher()` or `AddServiceBusEventDispatchermethod()`:


```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddQueryHandlers()
        .AddServiceBusCommandDispatcher()
        .AddServiceBusEventDispatcher();

    //other registrations    
    return builder.Build();
}

```

##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
