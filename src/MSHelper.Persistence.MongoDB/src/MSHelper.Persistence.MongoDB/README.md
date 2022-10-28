# MSHelper.Persistence.MongoDB : Adds the set of conventions and ease of use for MongoDB integration with .NET Core.
:star: Star us on GitHub — it motivates us a lot!

## Overview
Adds the set of conventions and ease of use for [MongoDB](https://www.mongodb.com/) integration with .NET Core.

## Installation

This document is for the latest MSHelper.Persistence.MongoDB **1.0.0 release and later**.

`dotnet add package MSHelper.Persistence.MongoDB`

## Dependencies

-- [MSHelper](https://www.nuget.org/packages/MSHelper)


## Usage
Extend `IMSHelperBuilder` with `AddMongo()` that will register the required services.

```
public static IMSHelperBuilder RegisterMSHelper(this IMSHelperBuilder builder)
{
    builder.AddMongo();
    // Other services.
    
    return builder;
}

```

```
public class SomeService
{
    private readonly IMongoDatabase _database;

    public SomeService(IMongoDatabase database)
    {
        _database = database;
    }
}

```

In order to use `IMongoRepository` abstraction, invoke `AddMongoRepository<TDocument, TIdentifiable>("collectionName")` for each document that you would like to be able to access with this repository abstraction and ensure that document type implements `IIdentifiable` interface.

By using the provided `IMongoRepository` you can access helper methods such as `AddAsync()`, `BrowseAsync()` etc. instead of relying on `IMongoDatabase` abstraction available via [MongoDB.Driver](https://docs.mongodb.com/ecosystem/drivers/csharp/).


## Options

--- connectionString - connection string e.g. mongodb://localhost:27017.

--- database - database name.

--- seed - boolean value, if true then `IMongoDbSeeder.SeedAsync()` will be invoked (if implemented).

### appsettings.json

```
"mongo": {
  "connectionString": "mongodb://localhost:27017",
  "database": "some-service",
  "seed": false
}

```



##### Important Note:
All the MSHelper packages are for self learning purposes inspired by Devmentors.io
