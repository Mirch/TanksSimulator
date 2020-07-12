using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game
{
    public class GameSimulator
    {
        //Game loop
        private bool _running = false;
        private Thread _gameThread;

        //Game logic
        private List<Tank> _tanks;
        private GameMap _map;

        //Utils
        private Random _random;
        private Logger _logger;

        public GameSimulator(
            GameMapModel map)
        {
            _random = new Random();
            _logger = new Logger();
            //TODO: build map
        }

        public void Start(
            TankModel tank1,
            TankModel tank2)
        {
            var tankEntity1 = BuildTank(tank1);
            var tankEntity2 = BuildTank(tank2);

            tankEntity1.SetEnemy(tankEntity2);
            tankEntity2.SetEnemy(tankEntity1);

            _tanks.Add(tankEntity1);
            _tanks.Add(tankEntity2);

            _gameThread = new Thread(() => Run());
            _logger.Log("Starting the simulation...");
            _gameThread.Start();
        }

        private void Run()
        {
            uint turn = 1; // keeping track of the game's turns
            var actingTank = _random.Next(_tanks.Count); // the index of the tank that is acting this turn

            while (_running)
            {
                var resultEvent = _tanks[actingTank].DecideAction();
                resultEvent.Process(_logger);

                if (_tanks.Any(t => t.IsDestroyed))
                {
                    _running = false;
                    break;
                }

                actingTank = (actingTank + 1) % _tanks.Count; // changing the turn
                turn++;
            }
        }

        private Tank BuildTank(TankModel tankModel)
        {
            var position = new Vector2i()
            {
                X = _random.Next(_map.Size),
                Y = _random.Next(_map.Size)
            };
            return new Tank(tankModel, position, _map);
        }
    }
}
