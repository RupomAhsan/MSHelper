using System;
using MSHelper.CQRS.Queries.Dispatchers;
using MSHelper.Types;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.CQRS.Queries;

public static class Extensions
{
    public static IMSHelperBuilder AddQueryHandlers(this IMSHelperBuilder builder)
    {
        builder.Services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                    .WithoutAttribute(typeof(DecoratorAttribute)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return builder;
    }

    public static IMSHelperBuilder AddInMemoryQueryDispatcher(this IMSHelperBuilder builder)
    {
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        return builder;
    }
}