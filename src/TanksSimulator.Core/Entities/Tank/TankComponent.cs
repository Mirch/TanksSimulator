using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities.Tank
{
    public abstract class TankComponent
    {
        public double Armor { get; protected set; }
        public bool IsDestroyed {
            get {
                return Armor <= 0;
            }
        }

        public virtual void GetHit(double damage)
        {
            Armor -= damage;
        }
    }

    public class TankRoadWheels : TankComponent
    {
    }

    public class TankTurret : TankComponent
    {
        public double Range { get; private set; }
        public double Damage { get; private set; }
        public double Accuracy { get; private set; }

        public TankTurret(TankTurretModel model)
        {
            Range = model.Range;
            Damage = model.Damage;
            Accuracy = model.Accuracy;

            Armor = 100;
        }

        public void Repair()
        {
            Armor = 100;
        }
    }

    public class TankMainBody : TankComponent
    {
    }
}
