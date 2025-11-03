using System;
using System.Collections.Generic;
using System.Linq;
using VividOrange.Geometry.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Point2d : IPoint2d, IEquatable<IPoint2d>
    {
        public Length U { get; set; }
        public Length V { get; set; }

        public Point2d()
        {
            U = Length.Zero;
            V = Length.Zero;
        }

        public Point2d(Length u, Length v)
        {
            U = u;
            V = v.ToUnit(u.Unit);
        }

        public Point2d(double u, double v, LengthUnit unit)
        {
            U = new Length(u, unit);
            V = new Length(v, unit);
        }

        public Point2d(IPoint2d other)
        {
            U = other.U;
            V = other.V.ToUnit(other.U.Unit);
        }

        public Point2d(ILocalPoint2d other)
        {
            U = other.Y;
            V = other.Z.ToUnit(other.Y.Unit);
        }

        public bool Equals(IPoint2d other)
        {
            return U.IsEqual(other.U) && V.IsEqual(other.V);
        }

        public static Vector2d operator -(Point2d point2, Point2d point1)
        {
            return new Vector2d(point2.U - point1.U, point2.V - point1.V);
        }

        public static Point2d operator *(double number, Point2d point)
        {
            return new Point2d(point.U * number, point.V * number);
        }

        public static implicit operator Vector2d(Point2d pt)
        {
            return new Vector2d(pt.U, pt.V);
        }

        /// <summary>
        /// Returns the distance between the 2D points p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Length Distance<P>(P p1, P p2) where P : IPoint2d
        {
            LengthUnit unit = p1.U.Unit;
            double area = Math.Pow((p1.U.As(unit) - p2.U.As(unit)), 2) + Math.Pow((p1.V.As(unit) - p2.V.As(unit)), 2);
            return new Length(Math.Sqrt(area), unit);
        }

        public static Point2d GetClosest(IPoint2d pt, IList<IPoint2d> pts)
        {
            List<Length> dists = pts.Select(p => Distance(pt, p)).ToList();
            return (Point2d)pts[dists.IndexOf(dists.Min())];
        }

        /// <summary>
        /// Returns the distance between p3 and the infinite line formed by p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns> Returns the distance between p3 and the infinite line formed by p1 and p2 </returns>
        public static Length DistancePointToLine(ILine2d ln, IPoint2d p, bool infinite = true)
        {
            return DistancePointToLine(ln.Start, ln.End, p, infinite);
        }

        /// <summary>
        /// Returns the distance between p3 and the infinite line formed by p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns> Returns the distance between p3 and the infinite line formed by p1 and p2 </returns>
        public static Length DistancePointToLine(IPoint2d l1, IPoint2d l2, IPoint2d p0, bool infinite = true)
        {
            LengthUnit unit = l1.U.Unit;
            double d = 0;
            if (((Point2d)l1).Equals(l2) == false)
            {
                d = Math.Abs((l2.V.As(unit) - l1.V.As(unit)) * p0.U.As(unit)
                           - (l2.U.As(unit) - l1.U.As(unit)) * p0.V.As(unit)
                           + l2.U.As(unit) * l1.V.As(unit) - l2.V.As(unit) * l1.U.As(unit))
                    / Math.Sqrt(Math.Pow(l2.V.As(unit) - l1.V.As(unit), 2) + Math.Pow(l2.U.As(unit) - l1.U.As(unit), 2));
            }

            if (!infinite)
            {
                var v1 = new Vector2d(p0.U - l1.U, p0.V - l1.V);
                var v2 = new Vector2d(p0.U - l2.U, p0.V - l2.V);
                var v12 = new Vector2d(l2.U - l1.U, l2.V - l1.V);
                var v21 = new Vector2d(l1.U - l2.U, l1.V - l2.V);
                Angle a1 = Vector2d.VectorAngle(v1, v12);
                Angle a2 = Vector2d.VectorAngle(v2, v21);
                if (a1.Radians > Math.PI / 2)
                {
                    return Distance(p0, l1);
                }
                else if (a2.Radians > Math.PI / 2)
                {
                    return Distance(p0, l2);
                }
            }

            return new Length(d, unit);
        }

        public static Point2d PointProjOnLine<L, P>(L ln, P p) where L : ILine2d where P : IPoint2d
        {
            return PointProjOnLine(ln.Start, ln.End, p);
        }

        public static Point2d PointProjOnLine<P>(P l1, P l2, P p0) where P : IPoint2d
        {
            LengthUnit unit = l1.U.Unit;
            double d = Math.Pow(l2.U.As(unit) - l1.U.As(unit), 2) + Math.Pow(l2.V.As(unit) - l1.V.As(unit), 2);
            double uv = (p0.U.As(unit) - l1.U.As(unit)) * (l2.U.As(unit) - l1.U.As(unit))
                        + (p0.V.As(unit) - l1.V.As(unit)) * (l2.V.As(unit) - l1.V.As(unit));
            return new Point2d(
                l1.U + uv / d * (l2.U - l1.U),
                l1.V + uv / d * (l2.V - l1.V));
        }

        public static (bool, Point2d) IsCloseToPolyline<Pt, Pl>(Pt p, Pl Polyline, Length d)
            where Pt : IPoint2d where Pl : IPolyline2d
        {
            return IsCloseToPolyline(p, Polyline.Points, d);
        }

        public static (bool, Point2d) IsCloseToPolyline(IPoint2d p0, IList<IPoint2d> points, Length d)
        {
            Point2d pt = null;
            bool isClose = false;
            for (int i = 0; i < points.Count - 1; i++)
            {
                Length distance = DistancePointToLine(points[i], points[i + 1], p0);
                if (distance <= d)
                {
                    d = distance;
                    pt = PointProjOnLine(points[i], points[i + 1], p0);
                    isClose = true;
                }
            }

            return (isClose, pt);
        }

        public static List<Point2d> RotatePoints(IList<IPoint2d> pts, Angle angle)
        {
            double cosa = Math.Cos(angle.Radians);
            double sina = Math.Sin(angle.Radians);
            return pts.Select(p => new Point2d(cosa * p.U - sina * p.V, sina * p.U + cosa * p.V)).ToList();
        }

        public static Point2d RotatePoint<P>(P pt, Angle angle) where P : IPoint2d
        {
            if (angle.Value != 0)
            {
                double cosa = Math.Cos(angle.Radians);
                double sina = Math.Sin(angle.Radians);
                return new Point2d(cosa * pt.U - sina * pt.V, sina * pt.U + cosa * pt.V);
            }
            else return new Point2d(pt);
        }

        public static Point2d RotatePoint<P>(P pt, Angle angle, P p0) where P : IPoint2d
        {
            if (angle.Value != 0)
            {
                double cosa = Math.Cos(angle.Radians);
                double sina = Math.Sin(angle.Radians);
                return new Point2d(cosa * (pt.U - p0.U) - sina * (pt.V - p0.V) + p0.U, sina * (pt.U - p0.U) + cosa * (pt.V - p0.V) + p0.V);
            }
            else return new Point2d(pt);
        }

        public static List<Point2d> GetBoundingBox(IList<IPoint2d> pts)
        {
            var vecs = new List<Vector2d>();
            for (int i = 0; i < pts.Count; i++)
            {
                for (int j = 0; j < pts.Count; j++)
                {
                    if (i != j)
                    {
                        if (!(pts[i].U.IsEqual(pts[j].U) && pts[i].V.IsEqual(pts[j].V)))
                        {
                            var v = new Vector2d(pts[i].U - pts[j].U, pts[i].V - pts[j].V);
                            vecs.Add(v);
                        }
                    }
                }
            }

            Vector2d vy = Vector2d.UnitV;
            Area area = new Area(100000, AreaUnit.SquareMillimeter);
            var boundingPts = new List<Point2d>();
            for (int i = 0; i < vecs.Count; i++)
            {
                Angle alpha = Vector2d.VectorAngle(vy, vecs[i]);
                double cosa = Math.Cos(alpha.Radians);
                double sina = Math.Sin(alpha.Radians);
                List<Point2d> pts2 = pts.Select(p => new Point2d(cosa * p.U - sina * p.V, sina * p.U + cosa * p.V)).ToList();

                Length Xmin = pts2.Select(q => q.U).Min();
                Length Xmax = pts2.Select(q => q.U).Max();
                Length Ymin = pts2.Select(q => q.V).Min();
                Length Ymax = pts2.Select(q => q.V).Max();

                if ((Xmax - Xmin) * (Ymax - Ymin) < area)
                {
                    area = (Xmax - Xmin) * (Ymax - Ymin);
                    boundingPts = new List<Point2d>
                    {
                        new Point2d(Xmin * cosa + Ymin * sina, -Xmin * sina + Ymin * cosa),
                        new Point2d(Xmax * cosa + Ymin * sina, -Xmax * sina + Ymin * cosa),
                        new Point2d(Xmax * cosa + Ymax * sina, -Xmax * sina + Ymax * cosa),
                        new Point2d(Xmin * cosa + Ymax * sina, -Xmin * sina + Ymax * cosa)
                    };
                }
            }

            return boundingPts;
        }

        /// <summary>
        /// Returns true if point is inside the Polyline defined by the vertices.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsInside(IList<IPoint2d> vertices, IPoint2d point, double tol = 0, bool border = true)
        {
            if (border)
            {
                if (vertices.Contains(point))
                    return true;
            }
            if (tol > 0)
            {

                vertices = Expand(vertices, tol);
            }

            bool inside = false;
            int nvert = vertices.Count;
            for (int i = 0; i < nvert; i++)
            {
                int j = (i == 0) ? nvert - 1 : i - 1;
                if (((vertices[i].V >= point.V) != (vertices[j].V >= point.V))
                    && (point.U <= (vertices[j].U - vertices[i].U) * (point.V - vertices[i].V) / (vertices[j].V - vertices[i].V) + vertices[i].U))
                    inside = !inside;
            }

            return inside;
        }

        public static Area GetPolylineArea(IList<IPoint2d> vertices, bool closed = false)
        {
            return CalculateArea(vertices, closed).Abs();
        }

        public static bool IsClockwise(IList<IPoint2d> vertices)
        {
            bool isClosed = ((Point2d)vertices.First()).Equals(vertices.Last());
            return CalculateArea(vertices, isClosed).Value < 0;
        }

        public static IList<IPoint2d> Contract(IList<IPoint2d> lpts, double factor)
        {
            Point2d cog = Utility.GetCenterLocal(lpts);
            var points = new List<IPoint2d>();
            for (int i = 0; i < lpts.Count; i++)
            {
                Length x = (1 - factor) * cog.U + factor * lpts[i].U;
                Length y = (1 - factor) * cog.V + factor * lpts[i].V;
                points.Add(new Point2d(x, y));
            }

            return points;
        }

        public static IList<IPoint2d> Expand(IList<IPoint2d> lpts, double factor)
        {
            Point2d cog = Utility.GetCenterLocal(lpts);
            var points = new List<IPoint2d>();
            foreach (IPoint2d pt in lpts)
            {
                var v = new Vector2d(pt.U - cog.U, pt.V - cog.V);
                v = v.Normalize();
                points.Add(new Point2d(pt.U + v.U * factor, pt.V + v.V * factor));
            }

            return points;
        }

        public static IList<IPoint2d> Offset(IList<IPoint2d> pts, Length distance)
        {
            LengthUnit u = distance.Unit;
            bool isClosed = pts.First().U.Meters == pts.Last().U.Meters
                         && pts.First().V.Meters == pts.Last().V.Meters;
            var offsetPts = new List<IPoint2d>();
            for (int i = 0; i < pts.Count; i++)
            {
                var tangentBefore = new Vector2d(Length.Zero, Length.Zero);
                var tangentAfter = new Vector2d(Length.Zero, Length.Zero);
                if (i == 0)
                {
                    if (isClosed)
                    {
                        tangentBefore = (Point2d)pts[0] - (Point2d)pts[pts.Count - 2];
                    }

                    tangentAfter = (Point2d)pts[1] - (Point2d)pts[0];

                }
                else if (i + 1 == pts.Count)
                {
                    if (isClosed)
                    {
                        tangentAfter = (Point2d)pts[1] - (Point2d)pts[0];
                    }

                    tangentBefore = (Point2d)pts[i] - (Point2d)pts[i - 1];
                }
                else
                {
                    tangentBefore = (Point2d)pts[i] - (Point2d)pts[i - 1];
                    tangentAfter = (Point2d)pts[i + 1] - (Point2d)pts[i];
                }

                int nonZeroVectors = 0;
                if (!(tangentBefore.U.Value == 0 && tangentBefore.V.Value == 0))
                {
                    tangentBefore = tangentBefore.Normalize();
                    tangentBefore = new Vector2d(new Length(tangentBefore.U.Value, u),
                                                 new Length(tangentBefore.V.Value, u));
                    nonZeroVectors++;
                }

                if (!(tangentAfter.U.Value == 0 && tangentAfter.V.Value == 0))
                {
                    tangentAfter = tangentAfter.Normalize();
                    tangentAfter = new Vector2d(new Length(tangentAfter.U.Value, u),
                                                 new Length(tangentAfter.V.Value, u));
                    nonZeroVectors++;
                }

                Vector2d tangent = tangentBefore + tangentAfter;
                Length tangentLength = tangent.Length;
                double factor = 1.0;
                if (nonZeroVectors > 1)
                {
                    factor = 2.0 / tangentLength.As(u);
                }

                var normal = new Vector2d(-tangent.V, tangent.U);
                normal = normal.Normalize();
                normal = new Vector2d(new Length(normal.U.Value, u),
                                      new Length(normal.V.Value, u));
                offsetPts.Add((Point2d)pts[i] + normal.Amplitude(distance * factor));
            }

            return offsetPts;
        }

        private static Area CalculateArea(IList<IPoint2d> vertices, bool closed = false)
        {
            Area res = Area.Zero;
            Area.TryParse($"0 {Length.GetAbbreviation(vertices[0].U.Unit)}²", out res);

            int n = vertices.Count;
            if (closed)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    res += (vertices[i].U * vertices[i + 1].V - vertices[i].V * vertices[i + 1].U) / 2;
                }
            }
            else
            {
                for (int i = 0; i < n - 1; i++)
                {
                    res += (vertices[i].U * vertices[i + 1].V - vertices[i].V * vertices[i + 1].U) / 2;
                }
                res += (vertices[n - 1].U * vertices[0].V - vertices[n - 1].V * vertices[0].U) / 2;
            }

            return res;
        }

        public override string ToString()
        {
            return "2D Point " + CoordinatesToString();
        }

        public string CoordinatesToString()
        {
            return $"(U:{U.ToString().Replace(" ", "\u2009")}, V:{V.ToString().Replace(" ", "\u2009")})";
        }
    }
}
