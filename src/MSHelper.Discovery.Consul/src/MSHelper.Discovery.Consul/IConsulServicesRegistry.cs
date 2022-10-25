using System.Threading.Tasks;
using MSHelper.Discovery.Consul.Models;

namespace MSHelper.Discovery.Consul;

public interface IConsulServicesRegistry
{
    Task<ServiceAgent> GetAsync(string name);
}