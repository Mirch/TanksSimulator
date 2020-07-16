using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Game;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.WebApi.Services
{
    public class GameSimulatorService
    {
        private readonly ResultsApiClient _resultsApiClient;
        private readonly DataApiClient _dataApiClient;


        public GameSimulatorService(
            ResultsApiClient resultsApiClient,
            DataApiClient dataApiClient)
        {
            _resultsApiClient = resultsApiClient;
            _dataApiClient = dataApiClient;
        }

        private async void OnGameFinished(object sender, GameFinishedEventArgs e)
        {
            var logger = (sender as GameSimulator).Logger;
            var logs = logger.GetLogs();

            var gameData = await _resultsApiClient.GetScoreAsync(e.GameId);

            gameData.Logs = logs;
            gameData.WinnerId = e.WinnerTankId;
            gameData.Status = GameStatus.Finished;

            await _resultsApiClient.UpdateAsync(gameData);
        }

        public async Task<GameDataModel> SimulateAsync(string gameId, TankModel tank1, TankModel tank2, GameMapModel map)
        {
            var gameData = await _resultsApiClient.GetScoreAsync(gameId);

            GameSimulator simulator = new GameSimulator(gameData.Id, Environment.GetEnvironmentVariable("WEATHER_API_URL"), map);
            simulator.GameFinished += OnGameFinished;

            gameData.Status = GameStatus.InProgress;
            await _resultsApiClient.UpdateAsync(gameData);

            simulator.Start(tank1, tank2);

            return gameData;
        }
    }
}
