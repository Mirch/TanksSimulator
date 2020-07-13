using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game
{
    public class GameSimulator
    {
        //Game loop
        public bool Running { get; private set; }
        public event EventHandler GameFinished;
        private Thread _gameThread;

        //Game logic
        private List<Tank> _tanks;
        private GameMap _map;

        //Utils
        public Logger Logger { get; }
        private Random _random;

        public GameSimulator(
            GameMapModel map)
        {
            _random = new Random();
            Logger = new Logger();
            //TODO: build map
        }

        private void OnGameFinished()
        {
            GameFinished?.Invoke(this, null);
        }

        public void Start(
            TankModel tank1,
            TankModel tank2)
        {
            var tankEntity1 = BuildTank(tank1);
            var tankEntity2 = BuildTank(tank2);

            tankEntity1.Enemy = tankEntity2;
            tankEntity2.Enemy = tankEntity1;

            _tanks.Add(tankEntity1);
            _tanks.Add(tankEntity2);

            _gameThread = new Thread(() => Run());
            Logger.Log("Starting the simulation...");
            _gameThread.Start();
        }

        private void Run()
        {
            Running = true;

            uint turn = 1; // keeping track of the game's turns
            var actingTank = _random.Next(_tanks.Count); // the index of the tank that is acting this turn

            var chainedEvents = new List<Event>(); // events triggered from last turn

            while (Running)
            {
                chainedEvents.ForEach(c => c.Process(Logger)); // processing events from last turn before acting this turn

                var resultEvent = _tanks[actingTank].Act();
                var result = resultEvent.Process(Logger);
                if (result != EventResult.Succeeded)
                {
                    chainedEvents.Add(result.ChainEvent);
                }

                if (_tanks.Any(t => t.IsDestroyed))
                {
                    Running = false;
                    break;
                }

                actingTank = (actingTank + 1) % _tanks.Count; // changing the turn
                turn++;
            }
            OnGameFinished();
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
