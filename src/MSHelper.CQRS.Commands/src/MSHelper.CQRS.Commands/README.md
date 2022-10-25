# MSHelper.CQRS.Commands : Commands
:star: Star us on GitHub — it motivates us a lot!

# Overview
Adds an ability to create and process Commands in the sense of [CQRS](https://martinfowler.com/bliki/CQRS.html).

## Installation

This document is for the latest MSHelper.CQRS.Commands **1.0.0 release and later**.

`dotnet add package MSHelper.CQRS.Commands`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)

## Usage
Implement `ICommand` (marker) interface in the selected class. Since the command represents the user’s intention you should follow the convention:

--- keep all the commands immutable
--- name of your commands should be imperative

```
public class CreateAccount : ICommand
{
    public Guid Id { get; }
    public string Email { get; }
    public string Password { get; }

    public CreateUser(id, email, password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}
```
  

Create dedicated command handler class that implements `ICommandHandler<TCommand>` interface with `HandleAsync()` method:

```
public class CreateAccountHandler : ICommandHandler<CreateAccount>
{
    public Task HandleAsync(CreateAccount command)
    {
        //put the handling code here
    }
}
```

You can easily register all command handlers in DI container by calling `AddCommandHandlers()` method on IMSHelperBuilder:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddCommandHandlers();

    //other registrations    
    return builder.Build();
}
```

Dispatching a particular command object can be also done using MSHelper package. Start with registering in-memory dispatcher on your `IMSHelperBuilder` by calling a `AddInMemoryCommandDispatcher()` method:

```
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = services.AddMSHelper()
        .AddCommandHandlers()
        .AddInMemoryCommandDispatcher();

    //other registrations    
    return builder.Build();
}
```

Then simply inject `ICommandDispatcher` into a class and call `DispatchAsync()` method:

```
public class AccountsService
{
    private readonly ICommandDispatcher  _dispatcher;

    public AccountsService(ICommandDispatcher  dispatcher)
    {
        _dispatcher = dispatcher;
    } 

    public Task<AccountDto> GetAccountAsync(Guid id)
        => _dispatcher.DispatchAsync(new GetAccount { Id = id });
}
```


## Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
