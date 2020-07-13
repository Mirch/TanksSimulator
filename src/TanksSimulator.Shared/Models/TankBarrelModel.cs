using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class TankBarrelModel
    {
        public double Range { get; set; }
        public double Damage { get; set; }
        public double Accuracy { get; set; }
    }
}
