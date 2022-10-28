# MSHelper : Introduction to MSHelper
:star: Star us on GitHub — it motivates us a lot!

# Welcome to MSHelper
MSHelper is a set of helper libraries that can be most of the time (with some exceptions) used independently of each other to help you to build your web applications and microservices. MSHelper is neither a framework nor a silver bullet.

Quite opposite, it’s mostly the set of extensions methods along with additional abstractions that will help you to deal with common infrastructural concerns such as routing, service discovery, load balancing, tracing, asynchronous messaging and so on.

# Getting started

This document is for the latest MSHelper.CQRS.Commands **1.0.1 release and later**.
In order to get started with MSHelper, simply install the core package:

`dotnet add package MSHelper`


## Usage
Its sole responsibility is to expose `IMSHelperBuilder` being used by other packages, which provides fluent API experience, similar to built-in ASP.NET Core IServiceCollection and IApplicationBuilder abstractions.


```
public class Program
{
    public static async Task Main(string[] args)
        => await WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddMSHelper().Build())
            .Configure(app =>
            {
                //Configure the middleware
            })
            .Build()
            .RunAsync();
}
```
  
Whether you’re using just a `Program.cs` on its own (yes, you can build your web applications and microservices without a need of having Startup class and `AddMvc()` along with full `UseMvc()` middleware) or doing it with a `Startup.cs` included, just invoke AddMSHelper() on `IServiceCollection` instance within the `ConfigureServices()` method and start using MSHelper packages.

The core MSHelper package also registers `AppOptions` type which contains the application name (and it’s purely optional).


## Options

 -- name - an optional name of the application.
 -- `displayBanner` - display a banner (console output) with the application name during a startup, true by default.


### appsettings.json

```
"app": {
  "name": "some service",
  "displayBanner": true
}
```


##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
