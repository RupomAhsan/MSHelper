using System.Threading.Tasks;

namespace MSHelper.Persistence.OpenStack.OCS.Auth;

internal interface IAuthManager
{
    Task<AuthData> Authenticate();
}