using System;
using System.Collections.Generic;
using System.Text;
using TanksSimulator.Game.Utils;

namespace TanksSimulator.Game.Map
{
    public class Node : IComparable
    {
        public Vector2i Position { get; private set; }
        public Node Parent { get; set; }

        public double FCost { get { return GCost + HCost; } }
        public double GCost { get; set; }
        public double HCost { get; set; }

        public Node(Vector2i position, Node parent, double gCost, double hCost)
        {
            Position = position;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
        }

        public int CompareTo(object obj)
        {
            var other = (Node)obj;
            if (other == null)
            {
                throw new ArgumentException("Object is not a Node.");
            }

            return FCost.CompareTo(other.FCost);
        }

        public override string ToString()
        {
            return $"({Position.X},{Position.Y})";
        }
    }
}
