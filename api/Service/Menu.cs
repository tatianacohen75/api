using api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Service
{
    public class Menu
    {
        private readonly IMongoCollection<sandwich> _sandwichName;

        public Menu(
       IOptions<apiDatabaseSettings> apiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                apiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                apiDatabaseSettings.Value.DatabaseName);

            _sandwichName = mongoDatabase.GetCollection<sandwich>(
                apiDatabaseSettings.Value.SandwichName);
        }

        public async Task<List<sandwich>> GetAsync() =>
       await _sandwichName.Find(_ => true).ToListAsync();

        public async Task<sandwich?> GetAsync(string id) =>
            await _sandwichName.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(sandwich newSandwich) =>
            await _sandwichName.InsertOneAsync(newSandwich);

        public async Task UpdateAsync(string id, sandwich updatedSandwich) =>
            await _sandwichName.ReplaceOneAsync(x => x.Id == id, updatedSandwich);

        public async Task RemoveAsync(string id) =>
            await _sandwichName.DeleteOneAsync(x => x.Id == id);
    }
}

