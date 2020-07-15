using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;
using TanksSimulator.Shared.Models;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game
{
    public class GameSimulator
    {
        private string _gameId;

        //Game loop
        public bool Running { get; private set; }
        public event EventHandler<GameFinishedEventArgs> GameFinished;
        private Thread _gameThread;

        //Game logic
        private List<Tank> _tanks;
        private GameMap _map;
        private int _turns;
        private Tank _winner;

        //Utils
        public Logger Logger { get; }
        private Random _random;
        private WeatherApiClient _weatherApiClient;

        public GameSimulator(
            string gameId,
            string weatherApiUri,
            GameMapModel map)
        {
            _gameId = gameId;

            Logger = new Logger(gameId);
            _random = new Random();

            _tanks = new List<Tank>();
            _map = new GameMap(map);

            _weatherApiClient = new WeatherApiClient(weatherApiUri);
        }

        private void OnGameFinished()
        {
            GameFinished?.Invoke(
                this,
                new GameFinishedEventArgs
                {
                    GameId = _gameId,
                    NumberOfTurns = _turns,
                    WinnerTankId = _winner.TankId
                });
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

            Logger.Log("Starting the simulation...");
            _gameThread = new Thread(() => Run());
            _gameThread.Start();
        }

        private async void Run()
        {
            Running = true;

            _turns = 1; // keeping track of the game's turns
            var actingTank = _random.Next(_tanks.Count); // the index of the tank that is acting this turn

            var chainedEvents = new List<Event>(); // events triggered from last turn

            while (Running)
            {
                Logger.Log($"--- Turn {_turns}: ---");
                _map.CurrentWeather = await _weatherApiClient.GetWeatherAsync();
                Logger.Log($"It's {_map.CurrentWeather} outsite.");

                // Processing chained events from last turn before acting this turn
                var eventsToAdd = new List<Event>();
                for (int i = chainedEvents.Count - 1; i >= 0; i--)
                {
                    var chainResult = chainedEvents[i].Process(Logger);
                    eventsToAdd.Add(chainResult.ChainEvent);
                    chainedEvents.RemoveAt(i);
                }
                chainedEvents.AddRange(eventsToAdd);
                eventsToAdd.Clear();

                // This turn's action, as well as its consequences (chained events)
                var resultEvent = _tanks[actingTank].Act();
                var result = resultEvent.Process(Logger);
                if (result != EventResult.Succeeded)
                {
                    chainedEvents.Add(result.ChainEvent);
                }

                // Win condition
                if (_tanks.Any(t => t.IsDestroyed))
                {
                    Running = false;
                    _winner = _tanks.SingleOrDefault(t => !t.IsDestroyed);
                    break;
                }

                // Changing the turn
                actingTank = (actingTank + 1) % _tanks.Count;
                _turns++;
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
            while (_map.GetTile(position).Solid) // finding an available tile
            {
                position = new Vector2i()
                {
                    X = _random.Next(_map.Size),
                    Y = _random.Next(_map.Size)
                };
            }
            Logger.Log($"Placing {tankModel.Name} at ({position.X}, {position.Y}).");
            return new Tank(tankModel.Id, tankModel, position, _map);
        }
    }
}