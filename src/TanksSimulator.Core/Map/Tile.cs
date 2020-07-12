using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public class Tile
    {
        public bool Solid { get; private set; }

        public Tile(
            bool solid)
        {
            Solid = solid;
        }
    }
}
