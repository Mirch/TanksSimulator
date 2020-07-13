using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Data
{
    public class MapsRepository
    {
        private readonly IMongoCollection<GameMapModel> _maps;

        public MapsRepository(
            IMongoDatabase database,
            ITanksSimulatorDbSettings databaseSettings)
        {
            _maps = database.GetCollection<GameMapModel>(databaseSettings.TanksCollection);
        }

        public async Task<List<GameMapModel>> GetAsync()
        {
            var result = await _maps
                .FindAsync(map => true);

            return result.ToList();
        }

        public async Task<GameMapModel> GetByIdAsync(string id)
        {
            var result = await _maps
                .Find(map => map.Id == id)
                .SingleAsync();

            return result;
        }
    }
}
