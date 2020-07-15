using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game.Events
{
    public class PlantLandmineEvent : Event
    {
        private Tank _owner;

        public PlantLandmineEvent(Tank owner)
        {
            _owner = owner;
        }

        public override EventResult Process(Logger logger)
        {
            logger.Log($"{_owner.Name} plants a landmine.");

            var landmine = new Landmine(_owner, _owner.Position, _owner.GameMap);

            var result = new EventResult();
            result.ChainEvent = new LandmineWaitingEvent(landmine);

            return result;
        }
    }
}
