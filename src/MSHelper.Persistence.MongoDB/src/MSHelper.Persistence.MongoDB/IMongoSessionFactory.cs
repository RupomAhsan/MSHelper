using System.Threading.Tasks;
using MongoDB.Driver;

namespace MSHelper.Persistence.MongoDB;

public interface IMongoSessionFactory
{
    Task<IClientSessionHandle> CreateAsync();
}