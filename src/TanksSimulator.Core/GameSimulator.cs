using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities;
using TanksSimulator.Game.Map;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game
{
    public class GameSimulator
    {
        private List<Tank> _tanks;
        private GameMap _map;

        public GameSimulator(
            GameMapModel map,
            TankModel tank1,
            TankModel tank2)
        {
            //TODO: build map
            _tanks.Add(new Tank(tank1));
            _tanks.Add(new Tank(tank2));
        }

        public void Start()
        {

        }
    }
}
