using System.Collections.Generic;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Line3d : ILine3d
    {
        public IPoint3d Start { get; set; }
        public IPoint3d End { get; set; }

        public Line3d(IPoint3d start, IPoint3d end)
        {
            Start = start;
            End = end;
        }

        public static implicit operator Polyline3d(Line3d ln)
        {
            return new Polyline3d(new List<IPoint3d> { ln.Start, ln.End });
        }

        public static implicit operator Vector3d(Line3d ln)
        {
            return (Point3d)ln.Start - (Point3d)ln.End;
        }

        public IDomain Domain()
        {
            List<Length> xs = new List<Length>()
            {
                Start.X,
                End.X,
            };

            List<Length> ys = new List<Length>()
            {
                Start.Y,
                End.Y,
            };

            List<Length> zs = new List<Length>()
            {
                Start.Z,
                End.Z,
            };

            var max = new Point3d(
                xs.Max(LengthUnit.Meter),
                ys.Max(LengthUnit.Meter),
                zs.Max(LengthUnit.Meter));
            var min = new Point3d(
                xs.Min(LengthUnit.Meter),
                ys.Min(LengthUnit.Meter),
                zs.Min(LengthUnit.Meter));
            return new Domain(max, min);
        }

        public override string ToString()
        {
            return $"3D Line (S:{((Point3d)Start).CoordinatesToString()} - E:{((Point3d)End).CoordinatesToString()}";
        }
    }
}
