using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Data
{
    public class TanksRepository : IRepository<TankModel>
    {
        private readonly IMongoCollection<TankModel> _tanks;

        public TanksRepository(
            IMongoDatabase database,
            ITanksSimulatorDbSettings databaseSettings)
        {
            _tanks = database.GetCollection<TankModel>(databaseSettings.TanksCollection);
        }

        public async Task<List<TankModel>> GetAsync()
        {
            var result = await _tanks
                .FindAsync(tank => true);

            return result.ToList();
        }

        public async Task<TankModel> GetByIdAsync(string id)
        {
            var result = await _tanks
                .Find(tank => tank.Id == id)
                .SingleAsync();

            return result;
        }
        
        public async Task<TankModel> CreateAsync(TankModel model)
        {
            await _tanks.InsertOneAsync(model);

            return model;
        }

        public async Task<TankModel> UpdateAsync(TankModel model)
        {
            await _tanks.ReplaceOneAsync(tank => tank.Id == model.Id, model);

            return model;
        }
    }
}
