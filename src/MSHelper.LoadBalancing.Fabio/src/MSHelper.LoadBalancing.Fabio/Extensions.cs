using System;
using System.Collections.Generic;
using System.Linq;
using MSHelper.Discovery.Consul;
using MSHelper.Discovery.Consul.Models;
using MSHelper.HTTP;
using MSHelper.LoadBalancing.Fabio.Builders;
using MSHelper.LoadBalancing.Fabio.Http;
using MSHelper.LoadBalancing.Fabio.MessageHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.LoadBalancing.Fabio;

public static class Extensions
{
    private const string SectionName = "fabio";
    private const string RegistryName = "loadBalancing.fabio";

    public static IMSHelperBuilder AddFabio(this IMSHelperBuilder builder, string sectionName = SectionName,
        string consulSectionName = "consul", string httpClientSectionName = "httpClient")
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            sectionName = SectionName;
        }
            
        var fabioOptions = builder.GetOptions<FabioOptions>(sectionName);
        var consulOptions = builder.GetOptions<ConsulOptions>(consulSectionName);
        var httpClientOptions = builder.GetOptions<HttpClientOptions>(httpClientSectionName);
        return builder.AddFabio(fabioOptions, httpClientOptions,
            b => b.AddConsul(consulOptions, httpClientOptions));
    }

    public static IMSHelperBuilder AddFabio(this IMSHelperBuilder builder,
        Func<IFabioOptionsBuilder, IFabioOptionsBuilder> buildOptions,
        Func<IConsulOptionsBuilder, IConsulOptionsBuilder> buildConsulOptions,
        HttpClientOptions httpClientOptions)
    {
        var fabioOptions = buildOptions(new FabioOptionsBuilder()).Build();
        return builder.AddFabio(fabioOptions, httpClientOptions,
            b => b.AddConsul(buildConsulOptions, httpClientOptions));
    }

    public static IMSHelperBuilder AddFabio(this IMSHelperBuilder builder, FabioOptions fabioOptions,
        ConsulOptions consulOptions, HttpClientOptions httpClientOptions)
        => builder.AddFabio(fabioOptions, httpClientOptions, b => b.AddConsul(consulOptions, httpClientOptions));

    private static IMSHelperBuilder AddFabio(this IMSHelperBuilder builder, FabioOptions fabioOptions,
        HttpClientOptions httpClientOptions, Action<IMSHelperBuilder> registerConsul)
    {
        registerConsul(builder);
        builder.Services.AddSingleton(fabioOptions);

        if (!fabioOptions.Enabled || !builder.TryRegister(RegistryName))
        {
            return builder;
        }

        if (httpClientOptions.Type?.ToLowerInvariant() == "fabio")
        {
            builder.Services.AddTransient<FabioMessageHandler>();
            builder.Services.AddHttpClient<IFabioHttpClient, FabioHttpClient>("fabio-http")
                .AddHttpMessageHandler<FabioMessageHandler>();


            builder.RemoveHttpClient();
            builder.Services.AddHttpClient<IHttpClient, FabioHttpClient>("fabio")
                .AddHttpMessageHandler<FabioMessageHandler>();
        }

        using var serviceProvider = builder.Services.BuildServiceProvider();
        var registration = serviceProvider.GetRequiredService<ServiceRegistration>();
        var tags = GetFabioTags(registration.Name, fabioOptions.Service);
        if (registration.Tags is null)
        {
            registration.Tags = tags;
        }
        else
        {
            registration.Tags.AddRange(tags);
        }

        builder.Services.UpdateConsulRegistration(registration);

        return builder;
    }

    public static void AddFabioHttpClient(this IMSHelperBuilder builder, string clientName, string serviceName)
        => builder.Services.AddHttpClient<IHttpClient, FabioHttpClient>(clientName)
            .AddHttpMessageHandler(c => new FabioMessageHandler(c.GetRequiredService<FabioOptions>(), serviceName));

    private static void UpdateConsulRegistration(this IServiceCollection services,
        ServiceRegistration registration)
    {
        var serviceDescriptor = services.FirstOrDefault(sd => sd.ServiceType == typeof(ServiceRegistration));
        services.Remove(serviceDescriptor);
        services.AddSingleton(registration);
    }

    private static List<string> GetFabioTags(string consulService, string fabioService)
    {
        var service = (string.IsNullOrWhiteSpace(fabioService) ? consulService : fabioService)
            .ToLowerInvariant();

        return new List<string> {$"urlprefix-/{service} strip=/{service}"};
    }
}