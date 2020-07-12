using System;
using System.Collections.Generic;
using System.Text;

namespace TanksSimulator.Game.Utils
{
    public class Line
    {
        private Vector2i _startingPoint;
        private Vector2i _endPoint;

        public Line(
            Vector2i startingPoint,
            Vector2i endPoint)
        {
            _startingPoint = startingPoint;
            _endPoint = endPoint;
        }

        public IEnumerable<Vector2i> GetIntersectingPoints()
        {
            var result = new List<Vector2i>();

            int x0 = _startingPoint.X;
            int y0 = _startingPoint.Y;

            int x1 = _endPoint.X;
            int y1 = _endPoint.Y;

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }

                result.Add(new Vector2i { X = x0, Y = y0 });
            }

            return result;
        }
    }
}
