using System;
using System.Diagnostics.Contracts;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankShootEvent : Event
    {
        private Random _random;
        private Tank _attacker;
        private Tank _target;

        public TankShootEvent(Tank attacker, Tank target)
        {
            _attacker = attacker;
            _target = target;
            _random = new Random();
        }

        public override EventResult Process(Logger logger)
        {
            var result = ProcessHit(_attacker, _target);
            logger.Log(result);

            if (_target.IsDestroyed)
            {
                var tankDestroyedEvent = new TankDestroyedEvent(_attacker, _target);
                tankDestroyedEvent.Process(logger);
            }

            if (_target.Turret.IsDestroyed)
            {
                var resultEvent = new EventResult();
                resultEvent.ChainEvent = new TankTurretRepairingEvent(_target, 2);
                return resultEvent;
            }

            return EventResult.Succeeded;
        }

        private string ProcessHit(Tank attacker, Tank target)
        {
            var missChance = _random.Next(100);
            if (missChance > attacker.Turret.Accuracy)
            {
                return $"{attacker.Name} tried shooting {target.Name}, but missed.";
            }

            var hittingPlace = _random.Next(2);
            string result = "";
            switch (hittingPlace)
            {
                case 0:
                    var damage = _random.Next(10, 15);
                    result = $"{attacker.Name} hits {target.Name} in the main body for {damage} damage.";
                    target.MainBody.GetHit(damage);
                    break;
                case 1:
                    damage = _random.Next(20, 25);
                    result = $"{attacker.Name} hits {target.Name}'s road wheels for {damage} damage.";
                    target.RoadWheel.GetHit(damage);
                    break;
                case 2:
                    damage = _random.Next(50, 70);
                    result = $"{attacker.Name} hits {target.Name}'s turret for {damage} damage.";
                    target.Turret.GetHit(damage);
                    break;
            }

            return result;
        }
    }
}
