using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class TankTurretModel
    {
        public int Id { get; set; }
        public double Range { get; set; }
        public double Damage { get; set; }
        public double Accuracy { get; set; }
    }
}
