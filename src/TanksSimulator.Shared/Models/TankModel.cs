using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class TankModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Armor { get; set; }
        public TankTurretModel Turret { get; set; }
    }
}
