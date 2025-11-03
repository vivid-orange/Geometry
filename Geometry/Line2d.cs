using System.Collections.Generic;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Line2d : ILine2d
    {
        public IPoint2d Start { get; set; }
        public IPoint2d End { get; set; }

        public Line2d(IPoint2d start, IPoint2d end)
        {
            Start = start;
            End = end;
        }

        public static implicit operator Polyline2d(Line2d ln)
        {
            return new Polyline2d(new List<IPoint2d> { ln.Start, ln.End });
        }

        public static implicit operator Vector2d(Line2d ln)
        {
            return (Point2d)ln.Start - (Point2d)ln.End;
        }

        public IDomain2d Domain()
        {
            List<Length> us = new List<Length>()
            {
                Start.U,
                End.U,
            };

            List<Length> vs = new List<Length>()
            {
                Start.V,
                End.V,
            };

            var max = new Point2d(us.Max(LengthUnit.Meter), vs.Max(LengthUnit.Meter));
            var min = new Point2d(us.Min(LengthUnit.Meter), vs.Min(LengthUnit.Meter));
            return new Domain2d(max, min);
        }

        public override string ToString()
        {
            return $"2D Line (S:{((Point2d)Start).CoordinatesToString()} - E:{((Point2d)End).CoordinatesToString()}";
        }
    }
}
