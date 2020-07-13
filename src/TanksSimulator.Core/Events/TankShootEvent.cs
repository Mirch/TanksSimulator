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

            if (_target.Barrel.IsDestroyed)
            {
                var resultEvent = new EventResult();
                resultEvent.ChainEvent = new TankBarrelRepairingEvent(_target, 2);
                return resultEvent;
            }

            return EventResult.Succeeded;
        }

        private string ProcessHit(Tank attacker, Tank target)
        {
            var missChance = _random.Next(100);
            if (missChance > attacker.Barrel.Accuracy)
            {
                return $"{attacker.Name} tried shooting {target.Name}, but missed.";
            }

            var hittingPlace = _random.Next(2);
            var damage = _random.Next((int)Math.Floor(attacker.Barrel.Damage * 0.8), (int)Math.Floor(attacker.Barrel.Damage));
            string result = "";
            switch (hittingPlace)
            {
                case 0:
                    result = $"{attacker.Name} hits {target.Name} in the main body for {damage} damage.";
                    target.MainBody.GetHit(damage);
                    break;
                case 1:
                    result = $"{attacker.Name} hits {target.Name}'s road wheels for {damage} damage.";
                    target.RoadWheel.GetHit(damage);
                    break;
                case 2:
                    result = $"{attacker.Name} hits {target.Name}'s barrel for {damage} damage.";
                    target.Barrel.GetHit(damage);
                    break;
            }

            return result;
        }
    }
}
