# MSHelper.CQRS.Events : Events
:star: Star us on GitHub — it motivates us a lot!

# Overview
Adds an ability to create and process Events in the sense of [CQRS](https://martinfowler.com/bliki/CQRS.html).

## Installation

This document is for the latest MSHelper.CQRS.Events **1.0.0 release and later**.

`dotnet add package MSHelper.CQRS.Events`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Implement `IEvent` or `IRejectedEvent` (marker) interface in the selected class. Since the event represents something that already happened, you should follow the convention:

--- keep all the events immutable
--- name of your events should kept in the past tense

```
public class AccountCreated : IEvent
{
    public Guid Id { get; }

    public AccountCreated(id)
    {
        Id = id;
    }
}
```
  

Create dedicated event handler class that implements `IEventHandler<TEvent>` interface with `HandleAsync()` method:

```
public class AccountCreatedHandler : IEventHandler<AccountCreated>
{
    public Task HandleAsync(AccountCreated @event)
    {
        //put the handling code here
    }
}
```

You can easily register all command handlers in DI container by calling `AddEventHandlers()` method on IMSHelperBuilder:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddEventHandlers();

    //other registrations    
    return builder.Build();
}
```

Dispatching a particular command object can be also done using MSHelper package. Start with registering in-memory dispatcher on your `IMSHelperBuilder` by calling a `AddInMemoryEventDispatcher()` method:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddCommandHandlers()
        .AddInMemoryEventDispatcher();

    //other registrations    
    return builder.Build();
}
```

Then simply inject `IEventDispatcher` into a class and call `DispatchAsync()` method:

```
public class AccountsService
{
    private readonly IEventDispatcher _dispatcher;

    public AccountsService(IEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    } 

    public Task PostProcessAccountCreation(AccountCreated @event)
        => _dispatcher.DispatchAsync(@event);
}
```


## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
