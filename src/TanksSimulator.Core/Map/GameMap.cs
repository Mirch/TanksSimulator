using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public class GameMap
    {
        private Tile[] _tiles;

        public int Size { get; private set; }

        public GameMap(int size)
        {
            Size = size;
            _tiles = new Tile[size * size];
        }

        public Tile GetTile(Vector2i coordinates)
        {
            return _tiles[coordinates.X + coordinates.Y * Size];
        }

    }
}
