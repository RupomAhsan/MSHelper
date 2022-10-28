using System.Threading.Tasks;
using MongoDB.Driver;

namespace MSHelper.Persistence.MongoDB;

public interface IMongoDbSeeder
{
    Task SeedAsync(IMongoDatabase database);
}