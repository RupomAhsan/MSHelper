using MSHelper.Security.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace MSHelper.Security;

public static class Extensions
{
    public static IMSHelperBuilder AddSecurity(this IMSHelperBuilder builder)
    {
        builder.Services
            .AddSingleton<IEncryptor, Encryptor>()
            .AddSingleton<IHasher, Hasher>()
            .AddSingleton<ISigner, Signer>();

        return builder;
    }
}