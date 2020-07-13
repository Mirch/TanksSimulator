using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class GameMapModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        public int Size { get; set; }
        public string Tiles { get; set; }
    }
}
