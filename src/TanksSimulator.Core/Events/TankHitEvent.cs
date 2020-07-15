using System;
using System.Collections.Generic;
using System.Linq;
using TanksSimulator.Game.Entities;
using TanksSimulator.Game.Entities.Tank;
using TanksSimulator.Shared.Utils;

namespace TanksSimulator.Game.Events
{
    public class TankHitEvent : Event
    {
        private Entity _attacker;
        private Tank _target;
        private Random _random;
        public TankHitEvent(Entity attacker, Tank target)
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
                var winner = _attacker is Landmine ? (_attacker as Landmine).Owner : _attacker as Tank;
                var tankDestroyedEvent = new TankDestroyedEvent(winner, _target);
                tankDestroyedEvent.Process(logger);
            }

            if (_target.RoadWheel.IsDestroyed)
            {
                logger.Log($"{_target.Name}'s road wheels are destroyed! {_target.Name} can't move.");
            }

            if (_target.Barrel.IsDestroyed)
            {
                var resultEvent = new EventResult();
                resultEvent.ChainEvent = new TankBarrelRepairingEvent(_target, 2);
                return resultEvent;
            }

            return EventResult.Succeeded;
        }

        private string ProcessHit(Entity attacker, Tank target)
        {
            double damage = 0.0;
            if (attacker is Tank)
            {
                damage = CalculateTankDamage(attacker as Tank);

                if (damage == 0)
                {
                    return $"{attacker.Name} tried hitting {target.Name}, but missed.";
                }
            }

            var possibleHits = new TankComponent[] { target.MainBody, target.RoadWheel, target.Barrel }
                .Where(t => !t.IsDestroyed)
                .ToList();

            var hit = possibleHits[_random.Next(possibleHits.Count())];

            var result = $"{attacker.Name} hits {target.Name}'s {hit.Name} for {damage} damage.";
            hit.GetHit(damage);

            return result;
        }

        private double CalculateTankDamage(Tank tank)
        {
            var missChance = _random.Next(100);
            if (missChance > tank.Barrel.Accuracy)
            {
                return 0;
            }

            var damage = _random.Next((int)Math.Floor(tank.Barrel.Damage * 0.8), (int)Math.Floor(tank.Barrel.Damage));

            return damage;
        }
    }
}
