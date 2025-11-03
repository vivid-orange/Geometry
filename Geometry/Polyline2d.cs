using System;
using System.Collections.Generic;
using System.Linq;
using VividOrange.Geometry.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Polyline2d : IPolyline2d
    {
        public IList<IPoint2d> Points { get; set; }
        public bool IsClosed => FirstPointEquivilantToLast();

        public Polyline2d(IList<IPoint2d> points)
        {
            if (points.IsNullOrEmpty() || points.Count < 2)
            {
                throw new ArgumentException("List must contain two or more points");
            }

            Points = points;
        }

        public IDomain2d Domain()
        {
            var max = new Point2d(
                Points.Select(pt => pt.U).Max(LengthUnit.Meter),
                Points.Select(pt => pt.V).Max(LengthUnit.Meter));
            var min = new Point2d(
                Points.Select(pt => pt.U).Min(LengthUnit.Meter),
                Points.Select(pt => pt.V).Min(LengthUnit.Meter));
            return new Domain2d(max, min);
        }
        public Area GetArea() => Point2d.GetPolylineArea(Points);
        public Point2d GetBarycenter() => Utility.GetCenterLocal(Points);
        public Point2d GetClosest<P>(P pt) where P : IPoint2d => Point2d.GetClosest(pt, Points);
        public bool IsClockwise() => Point2d.IsClockwise(Points);
        public (bool, Point2d) IsCloseToPolyline<P>(P p0, Length d) where P : IPoint2d
            => Point2d.IsCloseToPolyline(p0, Points, d);
        public bool IsInside<P>(P point, double tol = 0, bool border = true) where P : IPoint2d
            => Point2d.IsInside(Points, point, tol, border);
        public IPolyline2d Offset(Length distance) => new Polyline2d(Point2d.Offset(Points, distance));
        public Polyline2d Rotate(Angle angle) =>
            new Polyline2d(Point2d.RotatePoints(Points, angle).Select(x => (IPoint2d)x).ToList());

        public static explicit operator Line2d(Polyline2d Polyline)
        {
            if (Polyline.Points.Count != 2)
            {
                throw new InvalidCastException("Only a Polyline with two points can be cast to a Line");
            }

            return new Line2d(Polyline.Points[0], Polyline.Points[1]);
        }

        private bool FirstPointEquivilantToLast()
        {
            return Points.First().U.Meters == Points.Last().U.Meters
                && Points.First().V.Meters == Points.Last().V.Meters;
        }

        public override string ToString()
        {
            string closed = IsClosed ? "Closed" : "Open";
            return $"2D Polyline ({Points.Count} points;{closed})";
        }
    }
}
