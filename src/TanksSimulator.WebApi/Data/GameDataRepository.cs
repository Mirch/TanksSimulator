using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Data
{
    public class GameDataRepository
    {
        private readonly IMongoCollection<GameDataModel> _gameData;

        public GameDataRepository(
            IMongoDatabase database,
            ITanksSimulatorDbSettings databaseSettings)
        {
            _gameData = database.GetCollection<GameDataModel>(databaseSettings.GameDataCollection);
        }

        public async Task<List<GameDataModel>> GetAsync()
        {
            var result = await _gameData
                .FindAsync(data => true);

            return result.ToList();
        }

        public async Task<GameDataModel> GetByIdAsync(string id)
        {
            var result = await _gameData
                .Find(data => data.Id == id)
                .SingleAsync();

            return result;
        }

        public async Task<GameDataModel> CreateAsync(GameDataModel model)
        {
            await _gameData.InsertOneAsync(model);

            return model;
        }

        public async Task<GameDataModel> UpdateAsync(GameDataModel model)
        {
            try
            {
                await _gameData.ReplaceOneAsync(data => data.Id == model.Id, model);
            }
            catch
            {
                return model;
            }

            return model;
        }
    }
}
