using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using MSHelper.Persistence.OpenStack.OCS.Auth;
using MSHelper.Persistence.OpenStack.OCS.Client;
using MSHelper.Persistence.OpenStack.OCS.RequestHandler;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.Persistence.OpenStack.OCS;

public static class Extensions
{
    private const string SectionName = "OcsClient";

    public static IMSHelperBuilder AddOcsClient(this IMSHelperBuilder builder, string sectionName = SectionName)
    {
        if (string.IsNullOrEmpty(sectionName))
        {
            sectionName = SectionName;
        }

        var ocsOptions = builder.GetOptions<OcsOptions>(sectionName);

        if (string.IsNullOrEmpty(ocsOptions.InternalHttpClientName))
        {
            ocsOptions.InternalHttpClientName = "OcsClient";
        }

        builder.Services.AddSingleton(ocsOptions);
        builder.Services.AddHttpClient(ocsOptions.InternalHttpClientName, c =>
        {
            c.BaseAddress = new Uri(ocsOptions.StorageUrl);
            c.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("OcsClient"));
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        });
        builder.Services.AddTransient<IRequestHandler, RequestHandler.RequestHandler>();
        builder.Services.AddTransient<IOcsClient, OcsClient>();
        builder.Services.AddTransient<IAuthManager, AuthManager>();

        return builder;
    }
}