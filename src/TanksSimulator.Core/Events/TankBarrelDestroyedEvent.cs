using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankBarrelDestroyedEvent : Event
    {
        private Tank _tank;

        public TankBarrelDestroyedEvent(Tank tank)
        {
            _tank = tank;
        }

        public override EventResult Process(Logger logger)
        {
            logger.Log($"{_tank.Name}'s barrel was destroyed.");
            return EventResult.Succeeded;
        }
    }
}
