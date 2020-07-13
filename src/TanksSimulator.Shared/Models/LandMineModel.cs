using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class LandMineModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public int Id { get; set; }
        public double Damage { get; set; }
    }
}
