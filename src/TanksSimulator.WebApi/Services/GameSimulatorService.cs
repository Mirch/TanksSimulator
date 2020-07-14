using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Game;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;
using TanksSimulator.Shared.Utils;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Services
{
    public class GameSimulatorService
    {
        private readonly GameDataRepository _gameDataRepository;
        private readonly MapsRepository _mapsRepository;
        private readonly TanksRepository _tanksRepository;

        public GameSimulatorService(
            GameDataRepository gameDataRepository,
            MapsRepository mapsRepository,
            TanksRepository tanksRepository)
        {
            _gameDataRepository = gameDataRepository;
            _mapsRepository = mapsRepository;
            _tanksRepository = tanksRepository;
        }

        private async void OnGameFinished(object sender, GameFinishedEventArgs e)
        {
            var gameData = await _gameDataRepository.GetByIdAsync(e.GameId);

            gameData.WinnerId = e.WinnerTankId;
            gameData.Status = GameStatus.Finished;

            await _gameDataRepository.UpdateAsync(gameData);
        }

        public async Task<GameDataModel> SimulateAsync(string gameId)
        {
            var gameData = await _gameDataRepository.GetByIdAsync(gameId);

            GameSimulator simulator = new GameSimulator(gameData.Id, gameData.GameMapModel);
            simulator.GameFinished += OnGameFinished;
            
            gameData.Status = GameStatus.InProgress;
            await _gameDataRepository.UpdateAsync(gameData);

            simulator.Start(gameData.TankModel1, gameData.TankModel2);

            return gameData;
        }
    }
}
