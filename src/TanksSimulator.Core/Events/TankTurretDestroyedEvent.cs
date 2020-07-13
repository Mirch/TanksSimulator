using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankTurretDestroyedEvent : Event
    {
        private Tank _tank;

        public TankTurretDestroyedEvent(Tank tank)
        {
            _tank = tank;
        }

        public override EventResult Process(Logger logger)
        {
            logger.Log($"{_tank.Name}'s turret was destroyed.");
            return EventResult.Succeeded;
        }
    }
}
