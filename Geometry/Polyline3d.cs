using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry.Extensions;

namespace VividOrange.Geometry
{
    public class Polyline3d : IPolyline3d
    {
        public IList<IPoint3d> Points { get; set; }
        public bool IsClosed => FirstPointEquivilantToLast();

        public Polyline3d(IList<IPoint3d> points)
        {
            if (points.IsNullOrEmpty() || points.Count < 2)
            {
                throw new ArgumentException("List must contain two or more points");
            }

            Points = points;
        }

        public Point3d GetBarycenter()
        {
            return Point3d.GetBarycenter(Points);
        }

        public bool IsInsidePlane<P>(P p) where P : IPoint3d
        {
            return Point3d.IsInsidePlane(p, Points);
        }

        public static Point3d PlaneLineIntersection<P>(P line, P plane, bool within = true)
            where P : Polyline3d
        {
            return Point3d.PlaneLineIntersection(line.Points, plane.Points, within);
        }

        public static explicit operator Line3d(Polyline3d Polyline)
        {
            if (Polyline.Points.Count != 2)
            {
                throw new InvalidCastException("Only a Polyline with two points can be cast to a Line");
            }

            return new Line3d(Polyline.Points[0], Polyline.Points[1]);
        }

        public IDomain Domain()
        {
            var max = new Point3d(
                Points.Select(pt => pt.X).Max(LengthUnit.Meter),
                Points.Select(pt => pt.Y).Max(LengthUnit.Meter),
                Points.Select(pt => pt.Z).Max(LengthUnit.Meter));
            var min = new Point3d(
                Points.Select(pt => pt.X).Min(LengthUnit.Meter),
                Points.Select(pt => pt.Y).Min(LengthUnit.Meter),
                Points.Select(pt => pt.Z).Min(LengthUnit.Meter));
            return new Domain(max, min);
        }

        private bool FirstPointEquivilantToLast()
        {
            return Points.First().X.Meters == Points.Last().X.Meters
                && Points.First().Y.Meters == Points.Last().Y.Meters
                && Points.First().Z.Meters == Points.Last().Z.Meters;
        }

        public override string ToString()
        {
            string closed = IsClosed ? "Closed" : "Open";
            return $"3D Polyline ({Points.Count} points;{closed})";
        }
    }
}
