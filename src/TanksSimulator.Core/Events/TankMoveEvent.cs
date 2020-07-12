using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankMoveEvent : Event
    {
        private Tank _tank;
        private Vector2i _delta;

        public TankMoveEvent(Tank tank, Vector2i delta)
        {
            _tank = tank;
            _delta = delta;
        }

        public override void Process(Logger logger)
        {
            _tank.Position += _delta;
            logger.Log($"{_tank.Name} moved to ({_tank.Position.X}, {_tank.Position.Y}).");
        }
    }
}
