using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities.Tank
{
    public abstract class TankComponent
    {
        public virtual string Name { get; }
        public double Armor { get; protected set; }
        public bool IsDestroyed {
            get {
                return Armor <= 0;
            }
        }

        public TankComponent()
        {
            Armor = 100;
        }

        public virtual void GetHit(double damage)
        {
            Armor -= damage;
        }
    }

    public class TankRoadWheels : TankComponent
    {
        public override string Name { get; } = "road wheels";
        public TankRoadWheels()
            : base()
        {
        }
    }

    public class TankBarrel : TankComponent
    {
        public override string Name { get; } = "barrel";
        public double Range { get; private set; }
        public double Damage { get; private set; }
        public double Accuracy { get; private set; }

        public TankBarrel(TankBarrelModel model)
            : base()
        {
            Range = model.Range;
            Damage = model.Damage;
            Accuracy = model.Accuracy;
        }

        public void Repair()
        {
            Armor = 100;
        }
    }

    public class TankMainBody : TankComponent
    {
        public override string Name { get; } = "main body";
        public TankMainBody()
            : base()
        {
        }
    }
}
