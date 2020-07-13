using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public class Node : IComparable
    {
        public Vector2i Position { get; private set; }
        public Node Parent { get; private set; }

        public double FCost { get { return GCost + HCost; } }
        public double GCost { get; private set; }
        public double HCost { get; private set; }

        public Node(Vector2i tile, Node parent, double gCost, double hCost)
        {
            Position = tile;
            Parent = parent;
            GCost = gCost;
            HCost = HCost;
        }

        public int CompareTo(object obj)
        {
            var other = (Node)obj;
            if (other == null)
            {
                throw new ArgumentException("Object is not a Node.");
            }

            if (other.FCost < FCost) return 1;
            if (other.FCost > FCost) return -1;

            return 0;
        }
    }
}
