using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public class GameMap
    {
        private List<Tile> _tiles;

        public int Size { get; private set; }

        public GameMap(int size, string map)
        {
            Size = size;
            _tiles = new List<Tile>();
            BuildMap(map);
        }

        private void BuildMap(string map)
        {
            var tiles = map.ToCharArray();
            for (int i = 0; i < tiles.Length; i++)
            {
                Tile tile = null;
                switch (tiles[i])
                {
                    case '0':
                        tile = new PlainTile();
                        break;
                    case '1':
                        tile = new RockTile();
                        break;
                }
                _tiles.Add(tile);
            }
        }

        public List<Node> FindPath(Vector2i start, Vector2i goal)
        {
            var openList = new List<Node>();
            var closedList = new List<Node>();
            var current = new Node(start, null, 0, start.DistanceTo(goal));

            openList.Add(current);
            while (openList.Count > 0)
            {
                openList.Sort();
                current = openList[0];
                if (current.Position.Equals(goal))
                {
                    var path = new List<Node>();
                    while (current.Parent != null)
                    {
                        path.Add(current);
                        current = current.Parent;
                    }
                    return path;
                }
                openList.Remove(current);
                closedList.Add(current);

                for (int i = 0; i < 9; i++)
                {
                    if (i == 4) // current tile
                    {
                        continue;
                    }
                    var deltapos = new Vector2i { X = (i % 3) - 1, Y = (i / 3) - 1 };
                    var newPos = current.Position + deltapos;
                    Tile at = GetTile(newPos);
                    if (at == null || at.Solid)
                    {
                        continue;
                    }
                    var gCost = current.GCost + current.Position.DistanceTo(newPos);
                    var hCost = newPos.DistanceTo(goal);

                    Node node = new Node(newPos, current, gCost, hCost);
                    if (closedList.Any(n => n.Position == newPos) && gCost >= current.GCost)
                    {
                        continue;
                    }
                    if (!openList.Any(n => n.Position == newPos) || gCost < current.GCost)
                    {
                        openList.Add(node);
                    }
                }
            }
            return null;
        }
        public Tile GetTile(Vector2i coordinates)
        {
            if (coordinates.X < 0 || coordinates.X >= Size || coordinates.Y < 0 || coordinates.Y >= Size)
            {
                return null;
            }
            return _tiles[coordinates.X + coordinates.Y * Size];
        }

    }
}
