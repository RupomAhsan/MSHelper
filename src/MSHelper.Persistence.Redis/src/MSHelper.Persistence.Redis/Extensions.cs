using System;
using MSHelper.Persistence.Redis.Builders;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MSHelper.Persistence.Redis;

public static class Extensions
{
    private const string SectionName = "redis";
    private const string RegistryName = "persistence.redis";

    public static IMSHelperBuilder AddRedis(this IMSHelperBuilder builder, string sectionName = SectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            sectionName = SectionName;
        }

        var options = builder.GetOptions<RedisOptions>(sectionName);
        return builder.AddRedis(options);
    }

    public static IMSHelperBuilder AddRedis(this IMSHelperBuilder builder,
        Func<IRedisOptionsBuilder, IRedisOptionsBuilder> buildOptions)
    {
        var options = buildOptions(new RedisOptionsBuilder()).Build();
        return builder.AddRedis(options);
    }

    public static IMSHelperBuilder AddRedis(this IMSHelperBuilder builder, RedisOptions options)
    {
        if (!builder.TryRegister(RegistryName))
        {
            return builder;
        }

        builder.Services
            .AddSingleton(options)
            .AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(options.ConnectionString))
            .AddTransient(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase(options.Database))
            .AddStackExchangeRedisCache(o =>
            {
                o.Configuration = options.ConnectionString;
                o.InstanceName = options.Instance;
            });

        return builder;
    }
}