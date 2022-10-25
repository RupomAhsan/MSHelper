using System;
using MSHelper.CQRS.Events.Dispatchers;
using MSHelper.Types;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.CQRS.Events;

public static class Extensions
{
    public static IMSHelperBuilder AddEventHandlers(this IMSHelperBuilder builder)
    {
        builder.Services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                    .WithoutAttribute(typeof(DecoratorAttribute)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return builder;
    }

    public static IMSHelperBuilder AddInMemoryEventDispatcher(this IMSHelperBuilder builder)
    {
        builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
        return builder;
    }
}