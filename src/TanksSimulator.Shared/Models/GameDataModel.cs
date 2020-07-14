using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class GameDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
        public GameStatus Status { get; set; }

        public string Tank1Id { get; set; }
        public TankModel TankModel1 { get; set; }
        public string Tank2Id { get; set; }
        public TankModel TankModel2 { get; set; }
        public string MapId { get; set; }
        public GameMapModel GameMapModel { get; set; }

        public string WinnerId { get; set; }
        public ICollection<string> Logs { get; set; }
    }
}
