using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public abstract class Tile
    {
        public bool Solid { get; private set; }

        public Tile(
            bool solid)
        {
            Solid = solid;
        }
    }

    public class PlainTile : Tile
    {
        public PlainTile()
            : base(false)
        {
        }
    }

    public class RockTile : Tile
    {
        public RockTile()
            : base(true)
        {
        }
    }
}
