# MSHelper.CQRS.Queries : Queries
:star: Star us on GitHub — it motivates us a lot!

# Overview
Adds an ability to create and process queries in the sense of [CQRS](https://martinfowler.com/bliki/CQRS.html).

## Installation

This document is for the latest MSHelper.CQRS.Queries **1.0.0 release and later**.

`dotnet add package Convey.CQRS.Queries`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Implement `IQuery<TResult>` interface in the selected class:

```
public class GetAccount : IQuery<AccountDto>  
{    
  public Guid Id { get; set; }  
}
```
  

Create dedicated query handler class that implements `IQueryHandler<TQuery, TResult>` interface with `HandleAsync()` method:

```

public class GetAccountHandler : IQueryHandler<GetAccount, AccountDto>
{
    public Task<AccountDto> HandleAsync(GetAccount query)
    {
        //put the handling code here
    }
}
```

You can easily register all query handlers in DI container by calling `AddQueryHandlers()` method on `IConveyBuilder`:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddQueryHandlers();

    //other registrations    
    return builder.Build();
}
```

Dispatching a particular query object can be also done using MSHelper package. Start with registering in-memory dispatcher on your `IMSHelperBuilder` by calling a `AddInMemoryQueryDispatcher()` method:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddQueryHandlers()
        .AddInMemoryQueryDispatcher();

    //other registrations    
    return builder.Build();
}
```

Then simply inject `IQueryDispatcher` into a class and call `DispatchAsync()` method:

```
public class AccountsService
{
    private readonly IQueryDispatcher _dispatcher;

    public AccountsService(IQueryDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    } 

    public Task<AccountDto> GetAccountAsync(Guid id)
        => _dispatcher.DispatchAsync(new GetAccount { Id = id });
}
```


## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
