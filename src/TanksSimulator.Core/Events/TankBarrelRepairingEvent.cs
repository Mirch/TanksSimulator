using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankBarrelRepairingEvent : Event
    {
        private Tank _tank;
        private uint _remainingTurns;

        public TankBarrelRepairingEvent(Tank tank, uint turns)
        {
            _tank = tank;
            _remainingTurns = turns;
        }

        public override EventResult Process(Logger logger)
        {
            if (_remainingTurns == 0)
            {
                _tank.Barrel.Repair();
                logger.Log($"{_tank.Name}'s barrel is repaired.");
                return EventResult.Succeeded;
            }

            logger.Log($"{_tank.Name}'s barrel is being repaired. {_remainingTurns} turn(s) remaining.");
            var resultEvent = new EventResult();
            resultEvent.ChainEvent = new TankBarrelRepairingEvent(_tank, _remainingTurns - 1);
            return resultEvent;
        }
    }
}
