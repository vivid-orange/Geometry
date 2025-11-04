using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using VividOrange.Geometry.Extensions;

namespace VividOrange.Geometry
{
    public class Model3d : IModel3d
    {
        public IEnumerable<IMesh> Meshes { get; set; } = new List<IMesh>();
        public IEnumerable<IText3d> Texts { get; set; } = new List<IText3d>();
        public IEnumerable<IPolyline3d> Polylines { get; set; } = new List<IPolyline3d>();
        public IEnumerable<ILine3d> Lines { get; set; } = new List<ILine3d>();
        public IEnumerable<IPoint3d> Points { get; set; } = new List<IPoint3d>();

        public Model3d() { }

        public Model3d(IMesh mesh)
        {
            Meshes = new List<IMesh> { mesh };
        }

        public Point3d GetMaximumCorner()
        {
            var maxPt = new Point3d(double.MinValue, double.MinValue, double.MinValue, UnitsNet.Units.LengthUnit.Meter);
            if (!Meshes.IsNullOrEmpty())
            {
                maxPt = Max(maxPt, GetMax(Meshes, (p) => p.Verticies));
            }

            if (!Texts.IsNullOrEmpty())
            {
                maxPt = Max(maxPt, GetMax(Texts, (p) => p.Position));
            }

            if (!Polylines.IsNullOrEmpty())
            {
                maxPt = Max(maxPt, GetMax(Polylines, (p) => p.Points));
            }

            if (!Lines.IsNullOrEmpty())
            {
                maxPt = Max(maxPt, GetMax(Lines, (p) => p.Start));
                maxPt = Max(maxPt, GetMax(Lines, (p) => p.End));
            }

            if (!Points.IsNullOrEmpty())
            {
                maxPt = Max(maxPt, GetMax(Points, (p) => p));
            }

            return maxPt;
        }

        public Point3d GetMinimumCorner()
        {
            var minPt = new Point3d(double.MaxValue, double.MaxValue, double.MaxValue, UnitsNet.Units.LengthUnit.Meter);
            if (!Meshes.IsNullOrEmpty())
            {
                minPt = Min(minPt, GetMin(Meshes, (p) => p.Verticies));
            }

            if (!Texts.IsNullOrEmpty())
            {
                minPt = Min(minPt, GetMin(Texts, (p) => p.Position));
            }

            if (!Polylines.IsNullOrEmpty())
            {
                minPt = Min(minPt, GetMin(Polylines, (p) => p.Points));
            }

            if (!Lines.IsNullOrEmpty())
            {
                minPt = Min(minPt, GetMin(Lines, (p) => p.Start));
                minPt = Min(minPt, GetMin(Lines, (p) => p.End));
            }

            if (!Points.IsNullOrEmpty())
            {
                minPt = Min(minPt, GetMin(Points, (p) => p));
            }

            return minPt;
        }

        private Point3d GetMax<T, P>(IEnumerable<T> list, Func<T, P> selector) where P : ICartesian3d<Length, Length, Length>
        {
            Length x = list.Select(selector).Max(p => p.X);
            Length y = list.Select(selector).Max(p => p.Y);
            Length z = list.Select(selector).Max(p => p.Z);
            return new Point3d(x, y, z);
        }

        private Point3d GetMax<T, P>(IEnumerable<T> list, Func<T, IEnumerable<P>> selector) where P : ICartesian3d<Length, Length, Length>
        {
            Length x = list.SelectMany(selector).Max(p => p.X);
            Length y = list.SelectMany(selector).Max(p => p.Y);
            Length z = list.SelectMany(selector).Max(p => p.Z);
            return new Point3d(x, y, z);
        }

        private Point3d GetMin<T, P>(IEnumerable<T> list, Func<T, P> selector) where P : ICartesian3d<Length, Length, Length>
        {
            Length x = list.Select(selector).Min(p => p.X);
            Length y = list.Select(selector).Min(p => p.Y);
            Length z = list.Select(selector).Min(p => p.Z);
            return new Point3d(x, y, z);
        }

        private Point3d GetMin<T, P>(IEnumerable<T> list, Func<T, IEnumerable<P>> selector) where P : ICartesian3d<Length, Length, Length>
        {
            Length x = list.SelectMany(selector).Min(p => p.X);
            Length y = list.SelectMany(selector).Min(p => p.Y);
            Length z = list.SelectMany(selector).Min(p => p.Z);
            return new Point3d(x, y, z);
        }

        private Point3d Max(Point3d p1, Point3d p2)
        {
            Length x = p1.X > p2.X ? p1.X : p2.X;
            Length y = p1.Y > p2.Y ? p1.Y : p2.Y;
            Length z = p1.Z > p2.Z ? p1.Z : p2.Z;
            return new Point3d(x, y, z);
        }

        private Point3d Min(Point3d p1, Point3d p2)
        {
            Length x = p1.X < p2.X ? p1.X : p2.X;
            Length y = p1.Y < p2.Y ? p1.Y : p2.Y;
            Length z = p1.Z < p2.Z ? p1.Z : p2.Z;
            return new Point3d(x, y, z);
        }
    }
}
