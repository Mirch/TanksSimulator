using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TanksSimulator.Game.Utils
{
    public class Vector2i
    {
        public int X { get; set; }
        public int Y { get; set; }

        public double DistanceTo(Vector2i other)
        {
            return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
        }

        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            var result = new Vector2i();
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;

            return result;
        }

        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            var result = new Vector2i();
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector2i))
            {
                return false;
            }

            var other = (Vector2i)obj;
            return X == other.X && Y == other.Y;
        }

    }
}
