using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Game;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Services
{
    public class GameSimulatorService
    {
        private readonly GameDataRepository _gameDataRepository;

        public GameSimulatorService(
            GameDataRepository gameDataRepository)
        {
            _gameDataRepository = gameDataRepository;
        }

        private void SaveGameLogs(object sender, EventArgs e)
        {
            var logger = (sender as GameSimulator).Logger;
            var logs = logger.Flush();
            //TODO: save to database
        }

        public GameData Simulate()
        {
            TankModel tank1 = new TankModel(); // from request
            TankModel tank2 = new TankModel(); // from request
            GameMapModel map = new GameMapModel()
            {
                Id = 1,
            };

            GameData gameData = new GameData
            {
                MapId = map.Id,
                Tank1Id = tank1.Id,
                Tank2Id = tank2.Id
            };

            _gameDataRepository.Insert(gameData);

            GameSimulator simulator = new GameSimulator(map);
            simulator.GameFinished += SaveGameLogs;

            simulator.Start(tank1, tank2); // in a thread

            return gameData;
        }
    }
}
