using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class TankBarrelModel
    {
        public string Id { get; set; }
        public double Range { get; set; }
        public double Damage { get; set; }
        public double Accuracy { get; set; }
    }
}
