using System;
using MSHelper.Swagger;
using MSHelper.WebApi.Swagger.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.WebApi.Swagger;

public static class Extensions
{
    private const string SectionName = "swagger";

    public static IMSHelperBuilder AddWebApiSwaggerDocs(this IMSHelperBuilder builder, string sectionName = SectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            sectionName = SectionName;
        }

        return builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(sectionName));
    }
        
    public static IMSHelperBuilder AddWebApiSwaggerDocs(this IMSHelperBuilder builder, 
        Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
        => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(buildOptions));
        
    public static IMSHelperBuilder AddWebApiSwaggerDocs(this IMSHelperBuilder builder, SwaggerOptions options)
        => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(options));
        
    private static IMSHelperBuilder AddWebApiSwaggerDocs(this IMSHelperBuilder builder, Action<IMSHelperBuilder> registerSwagger)
    {
        registerSwagger(builder);
        builder.Services.AddSwaggerGen(c => c.DocumentFilter<WebApiDocumentFilter>());
        return builder;
    }
}