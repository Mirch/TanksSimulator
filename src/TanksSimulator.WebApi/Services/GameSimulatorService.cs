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

        public GameSimulatorService(
            ResultsApiClient resultsApiClient)
        {
            _resultsApiClient = resultsApiClient;
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

        public async Task<GameDataModel> SimulateAsync(string gameId)
        {
            var gameData = await _resultsApiClient.GetScoreAsync(gameId);

            GameSimulator simulator = new GameSimulator(gameData.Id, gameData.GameMapModel);
            simulator.GameFinished += OnGameFinished;

            gameData.Status = GameStatus.InProgress;
            await _resultsApiClient.UpdateAsync(gameData);

            simulator.Start(gameData.TankModel1, gameData.TankModel2);

            return gameData;
        }
    }
}
