using System;
using MSHelper.Persistence.MongoDB.Builders;
using MSHelper.Persistence.MongoDB.Factories;
using MSHelper.Persistence.MongoDB.Initializers;
using MSHelper.Persistence.MongoDB.Repositories;
using MSHelper.Persistence.MongoDB.Seeders;
using MSHelper.Types;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MSHelper.Persistence.MongoDB;

public static class Extensions
{
    // Helpful when dealing with integration testing
    private static bool _conventionsRegistered;
    private const string SectionName = "mongo";
    private const string RegistryName = "persistence.mongoDb";

    public static IMSHelperBuilder AddMongo(this IMSHelperBuilder builder, string sectionName = SectionName,
        Type seederType = null, bool registerConventions = true)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            sectionName = SectionName;
        }

        var mongoOptions = builder.GetOptions<MongoDbOptions>(sectionName);
        return builder.AddMongo(mongoOptions, seederType, registerConventions);
    }

    public static IMSHelperBuilder AddMongo(this IMSHelperBuilder builder, Func<IMongoDbOptionsBuilder,
        IMongoDbOptionsBuilder> buildOptions, Type seederType = null, bool registerConventions = true)
    {
        var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();
        return builder.AddMongo(mongoOptions, seederType, registerConventions);
    }

    public static IMSHelperBuilder AddMongo(this IMSHelperBuilder builder, MongoDbOptions mongoOptions,
        Type seederType = null, bool registerConventions = true)
    {
        if (!builder.TryRegister(RegistryName))
        {
            return builder;
        }

        if (mongoOptions.SetRandomDatabaseSuffix)
        {
            var suffix = $"{Guid.NewGuid():N}";
            Console.WriteLine($"Setting a random MongoDB database suffix: '{suffix}'.");
            mongoOptions.Database = $"{mongoOptions.Database}_{suffix}";
        }

        builder.Services.AddSingleton(mongoOptions);
        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<MongoDbOptions>();
            return new MongoClient(options.ConnectionString);
        });
        builder.Services.AddTransient(sp =>
        {
            var options = sp.GetRequiredService<MongoDbOptions>();
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.Database);
        });
        builder.Services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
        builder.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

        if (seederType is null)
        {
            builder.Services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
        }
        else
        {
            builder.Services.AddTransient(typeof(IMongoDbSeeder), seederType);
        }

        builder.AddInitializer<IMongoDbInitializer>();
        if (registerConventions && !_conventionsRegistered)
        {
            RegisterConventions();
        }

        return builder;
    }

    private static void RegisterConventions()
    {
        _conventionsRegistered = true;
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?),
            new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
        ConventionRegistry.Register("MSHelper", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
        }, _ => true);
    }

    public static IMSHelperBuilder AddMongoRepository<TEntity, TIdentifiable>(this IMSHelperBuilder builder,
        string collectionName)
        where TEntity : IIdentifiable<TIdentifiable>
    {
        builder.Services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
        });

        return builder;
    }
}