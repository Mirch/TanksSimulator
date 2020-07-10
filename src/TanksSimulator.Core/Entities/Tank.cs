using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.Game.Entities
{
    public class Tank
    {
        private double Armor;
        private TankTurret Turret;

        public Tank(TankModel model)
        {
            Armor = model.Armor;
            Turret = new TankTurret(model.Turret);
        }
    }
}
