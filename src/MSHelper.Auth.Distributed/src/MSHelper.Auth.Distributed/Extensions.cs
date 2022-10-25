using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.Auth.Distributed;

public static class Extensions
{
    private const string RegistryName = "auth.distributed";

    public static IMSHelperBuilder AddDistributedAccessTokenValidator(this IMSHelperBuilder builder)
    {
        if (!builder.TryRegister(RegistryName))
        {
            return builder;
        }

        builder.Services.AddSingleton<IAccessTokenService, DistributedAccessTokenService>();

        return builder;
    }
}