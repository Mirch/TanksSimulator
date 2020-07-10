using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Shared.Models
{
    public class GameData
    {
        public int Id { get; set; }
        public GameStatus Status { get; set; }

        public int Tank1Id { get; set; }
        public int Tank2Id { get; set; }
        public int MapId { get; set; }
    }
}
