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

        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            var result = new Vector2i();
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;

            return result;
        }
    }
}
