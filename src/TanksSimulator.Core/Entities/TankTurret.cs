using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities
{
    public class TankTurret
    {
        private double Range;
        private double Damage;
        private double Armor;

        public TankTurret(TankTurretModel model)
        {
            Range = model.Range;
            Damage = model.Damage;
            Armor = model.Armor;
        }
    }
}
