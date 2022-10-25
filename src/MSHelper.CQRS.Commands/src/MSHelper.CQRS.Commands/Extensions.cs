using Microsoft.Extensions.DependencyInjection;
using MSHelper.CQRS.Commands.Dispatchers;
using MSHelper.Types;

namespace MSHelper.CQRS.Commands;

public static class Extensions
{
    public static IMSHelperBuilder AddCommandHandlers(this IMSHelperBuilder builder)
    {
        builder.Services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                    .WithoutAttribute(typeof(DecoratorAttribute)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return builder;
    }

    public static IMSHelperBuilder AddInMemoryCommandDispatcher(this IMSHelperBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        return builder;
    }
}