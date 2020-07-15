using System;
using System.Linq;
using TanksSimulator.Game.Events;
using TanksSimulator.Game.Map;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Entities.Tank
{
    public class Landmine : Entity
    {
        public Tank Owner { get; private set; }

        public Landmine(Tank owner, Vector2i position, GameMap map)
            : base(map)
        {
            Name = $"{owner.Name}'s Landmine";
            Owner = owner;
            Position = position;
        }

        public override Event Act()
        {
            var collidingTank = GameMap.Entities
                .SingleOrDefault(e => e is Tank && e.Position == Position && e != Owner) as Tank;

            if (collidingTank != null)
            {
                return new TankHitEvent(this, collidingTank);
            }

            return new LandmineWaitingEvent(this);
        }
    }
}
