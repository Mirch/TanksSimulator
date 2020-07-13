using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class GameDataModel
    {
        public string Id { get; set; }
        public GameStatus Status { get; set; }

        public string Tank1Id { get; set; }
        public string Tank2Id { get; set; }
        public string MapId { get; set; }
    }
}
