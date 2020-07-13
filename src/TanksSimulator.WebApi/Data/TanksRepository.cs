using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Data
{
    public class TanksRepository
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
    }
}
