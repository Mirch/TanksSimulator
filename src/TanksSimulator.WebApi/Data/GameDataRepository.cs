using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Data
{
    public class GameDataRepository
    {
        private List<GameData> _gameData;

        public GameData Get(int id)
        {
            return _gameData[id];
        }

        public GameData Insert(GameData gameData)
        {
            gameData.Id = _gameData.Count;
            _gameData.Add(gameData);

            return gameData;
        }
    }
}
