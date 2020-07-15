using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.DataApi.Data
{
    public class MapsRepository : IRepository<GameMapModel>
    {
        private readonly IMongoCollection<GameMapModel> _maps;

        public MapsRepository(
            IMongoDatabase database,
            ITanksSimulatorDbSettings databaseSettings)
        {
            _maps = database.GetCollection<GameMapModel>(databaseSettings.MapsCollection);
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
        public async Task<GameMapModel> CreateAsync(GameMapModel model)
        {
            await _maps.InsertOneAsync(model);

            return model;
        }

        public async Task<GameMapModel> UpdateAsync(GameMapModel model)
        {
            await _maps.ReplaceOneAsync(map => map.Id == model.Id, model);

            return model;
        }
    }
}
