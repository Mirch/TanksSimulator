using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankTurretRepairingEvent : Event
    {
        private Tank _tank;
        private uint _remainingTurns;

        public TankTurretRepairingEvent(Tank tank, uint turns)
        {
            _tank = tank;
            _remainingTurns = turns;
        }

        public override EventResult Process(Logger logger)
        {

            if (_remainingTurns == 0)
            {
                _tank.Turret.Repair();
                logger.Log($"{_tank.Name}'s turret is repaired.");
                return EventResult.Succeeded;
            }

            logger.Log($"{_tank.Name}'s turret is being repaired. {_remainingTurns} turn(s) remaining.");
            var resultEvent = new EventResult();
            resultEvent.ChainEvent = new TankTurretRepairingEvent(_tank, _remainingTurns - 1);
            return resultEvent;
        }
    }
}
