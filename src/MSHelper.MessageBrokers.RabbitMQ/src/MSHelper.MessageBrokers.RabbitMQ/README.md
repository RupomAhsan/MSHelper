# MSHelper.MessageBrokers.RabbitMQ : Asynchronous messaging using RabbitMQ.
:star: Star us on GitHub — it motivates us a lot!

## Overview
Adds the set of conventions and ease of use for RabbitMQ integration with .NET Core.

## Installation

This document is for the latest MSHelper.MessageBrokers.RabbitMQ **1.0.0 release and later**.

`dotnet add package MSHelper.MessageBrokers.RabbitMQ`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

-- [MSHelper.MessageBrokers](https://www.nuget.org/packages/MSHelper.MessageBrokers)

## Options
Provides RabbitMQ integration based on the official [RabbitMQ .NET Client](https://www.rabbitmq.com/dotnet.html) with highly customizable settings, support for custom naming conventions, templating, dead letter exchange and many more.

### appsettings.json

```
"rabbitMq": {
  "connectionName": "some-service",
  "retries": 3,
  "retryInterval": 2,
  "conventionsCasing": "snakeCase",
  "logger": {
    "enabled": true
  },
  "username": "guest",
  "password": "guest",
  "virtualHost": "/",
  "port": 5672,
  "hostnames": [
    "localhost"
  ],
  "requestedConnectionTimeout": "00:00:30",
  "requestedHeartbeat": "00:01:00",
  "socketReadTimeout": "00:00:30",
  "socketWriteTimeout": "00:00:30",
  "continuationTimeout": "00:00:20",
  "handshakeContinuationTimeout": "00:00:10",
  "networkRecoveryInterval": "00:00:05",
  "exchange": {
    "declare": true,
    "durable": true,
    "autoDelete": false,
    "type": "topic",
    "name": "stories"
  },
  "queue": {
    "declare": true,
    "durable": true,
    "exclusive": false,
    "autoDelete": false,
    "template": "some-service/."
  },
  "context": {
    "enabled": true,
    "header": "message_context"
  },
  "deadLetter": {
    "enabled": true,
    "prefix": "dlx-",
    "declare": true
  },
  "maxProducerChannels": 1000,
  "requeueFailedMessages": false,
  "spanContextHeader": "span_context"
},

```

## Usage

Inside Startup.cs extend `IMSHelperBuilder` with `AddRabbitMq()` that will register the required services.

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddRabbitMq();

    //other registrations    
    return builder.Build();
}

```
The above registration creates a unique connection to RabbitMQ and registers services required for publishing and subscribing the messages.

To subscribe to a particular message, invoke `UseRabbitMq()` method on `IApplicationBuilder` and call `Subscribe<TMessage>()` passing a function which is going to be executed once the message got received.


```
public void Configure(this IApplicationBuilder app)
{
    app.UseRabbitMq()
       .Subscribe<MyMessage>(async (serviceProvider, message, context) => 
       {
           //put your "on received" code here
       });
}

```

Once you subscribe to message a coresponding RabbitMQ **topic**, **exchange** and **routing key** should be created using the following conventions:
--- Exchange - {options.exchange.name}

--- Queue - {options.queue.template}

--- Routing key - {messageType}


The conventions can be overriden either by providing the custom implementation of `IConventionsBuilder`:

```
public interface IConventionsBuilder
{
    string GetRoutingKey(Type type);
    string GetExchange(Type type);
    string GetQueue(Type type);
}

```

Or by applying [Message] attribute on top of the class e.g.

```
[Message("users")]
public class UserCreated : IEvent
{
    public Guid UserId { get; }
    public string Name { get; }

    public UserCreated(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}

```

To publish a message simply inject `IBusPublisher` into any class you want and invoke `PublishAsync()` (or make use `IRabbitMqClient`) passing the message and the additional parameters,

```
public class CustomBusPublisher
{
    private readonly IBusPublisher _publisher;

    public CustomBusPublisher(IBusPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task PublishMessageAsync<T>(T message) => _publisher.PublishAsync(message);
}

```

### Error handling

During message processing there might be a chance that an exception will be thrown. We can distinguish two types of exceptions:

--- domain exception - informs that message cannot be further processed due to some domain logic like. PasswordToShortException

--- infrastructure exception - informs that message cannot be further processed due to infrastructure issues like. connecting to database etc.

In the first scenario, it’s better not to retry the processing (wrong password is not going to be better once we try again). n the second one, we can try a few times before we give up. MSHelper allows you to add this type of error handling using a simple mapper. Create a class that implements `IExceptionToMessageMapper` interface and register it in `IMSHelperBuilder`:


```
public class ExceptionToMessageMapper : IExceptionToMessageMapper
{
    public object Map(Exception exception, object message)
    {
        switch (exception)
        {
            // do simple pattern matching
        }

        return null;
    }
}

// Startup.cs

public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddRabbitMq()
        .AddExceptionToMessageMapper<ExceptionToMessageMapper>();

    //other registrations    
    return builder.Build();
}

```

If an exception will be thrown during message processing, a mapper is used to produce another message that will be automatically published to RabbitMQ. If exception->message mapping is not be defined, retry is going to be performed according to parameters provided in appsettings.json.

## Dead-letter exchange

[DLX](https://www.rabbitmq.com/dlx.html) support can be enabled via options:

```
"deadLetter": {
  "enabled": true,
  "prefix": "dlx-",
  "declare": true,
  "durable": true,
  "exclusive": false,
  "autoDelete": false,
  "ttl": 86400
}

```

Each message which is not defined as a part of rejected event mapping in `IExceptionToMessageMapper`, will be published to its own dead letter queue which will be named based on `{options.prefix}{queue}` e.g. for users queue, there would be dlx-users dead letter queue.



##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
