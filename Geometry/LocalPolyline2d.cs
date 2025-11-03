using System;
using System.Collections.Generic;
using System.Linq;
using VividOrange.Geometry.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class LocalPolyline2d : ILocalPolyline2d
    {
        public IList<ILocalPoint2d> Points { get; set; }
        public bool IsClosed => FirstPointEquivilantToLast();

        public LocalPolyline2d(IList<ILocalPoint2d> points)
        {
            if (points.IsNullOrEmpty() || points.Count < 2)
            {
                throw new ArgumentException("List must contain two or more points");
            }

            Points = points;
        }

        public Area GetArea() => LocalPoint2d.GetPolylineArea(Points);

        public LocalPoint2d GetBarycenter() =>
            new LocalPoint2d(Utility.GetCenterLocal(Points.Select(p => new Point2d(p)).ToList()));

        public LocalPolyline2d Rotate(Angle angle) =>
            new LocalPolyline2d(LocalPoint2d.RotatePoints(Points, angle).Select(x => (ILocalPoint2d)x).ToList());

        public ILocalDomain2d Domain()
        {
            var max = new LocalPoint2d(
                Points.Select(pt => pt.Y).Max(LengthUnit.Meter),
                Points.Select(pt => pt.Z).Max(LengthUnit.Meter));
            var min = new LocalPoint2d(
                Points.Select(pt => pt.Y).Min(LengthUnit.Meter),
                Points.Select(pt => pt.Z).Min(LengthUnit.Meter));
            return new LocalDomain2d(max, min);
        }

        public bool IsClockwise() => LocalPoint2d.IsClockwise(Points);
        public ILocalPolyline2d Offset(Length distance) =>
            new LocalPolyline2d(LocalPoint2d.Offset(Points, distance).Select(x => (ILocalPoint2d)x).ToList());

        private bool FirstPointEquivilantToLast()
        {
            return Points.First().Y.Meters == Points.Last().Y.Meters
                && Points.First().Z.Meters == Points.Last().Z.Meters;
        }

        public override string ToString()
        {
            string closed = IsClosed ? "Closed" : "Open";
            return $"Local 2D Polyline ({Points.Count} points;{closed})";
        }
    }
}
