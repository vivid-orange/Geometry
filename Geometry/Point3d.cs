using System;
using System.Collections.Generic;
using System.Numerics;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry.Extensions;

namespace VividOrange.Geometry
{
    public class Point3d : IPoint3d, IEquatable<IPoint3d>
    {
        public Length X { get; set; } = Length.Zero;
        public Length Y { get; set; } = Length.Zero;
        public Length Z { get; set; } = Length.Zero;

        public Point3d() { }
        public Point3d(Length x, Length y, Length z)
        {
            X = x;
            Y = y.ToUnit(x.Unit);
            Z = z.ToUnit(x.Unit);
        }

        public Point3d(double x, double y, double z, LengthUnit unit)
        {
            X = new Length(x, unit);
            Y = new Length(y, unit);
            Z = new Length(z, unit);
        }

        public Point3d(IPoint3d other)
        {
            X = other.X;
            Y = other.Y.ToUnit(other.X.Unit);
            Z = other.Z.ToUnit(other.X.Unit);
        }

        public bool Equals(IPoint3d other)
        {
            return X.IsEqual(other.X) && Y.IsEqual(other.Y) && Z.IsEqual(other.Z);
        }

        public static Vector3d operator -(Point3d point2, Point3d point1)
        {
            return new Vector3d(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z);
        }

        public static Point3d operator +(Point3d point1, Vector3d vector1)
        {
            return new Point3d(point1.X + vector1.X, point1.Y + vector1.Y, point1.Z + vector1.Z);
        }

        public static Vector3d operator +(Point3d point1, Point3d point2)
        {
            return new Vector3d(point2.X + point1.X, point2.Y + point1.Y, point2.Z + point1.Z);
        }

        public static implicit operator Vector3d(Point3d pt)
        {
            return new Vector3d(pt.X, pt.Y, pt.Z);
        }

        public static implicit operator Vertex(Point3d pt)
        {
            return new Vertex(pt, new Coordinate());
        }

        public static Point3d TransformedPoint(IPoint3d pt, Matrix4x4 matrix)
        {
            LengthUnit unit = pt.X.Unit;
            double newX = matrix.M11 * pt.X.As(unit) + matrix.M21 * pt.Y.As(unit) + matrix.M31 * pt.Z.As(unit) + matrix.M41;
            double newY = matrix.M12 * pt.X.As(unit) + matrix.M22 * pt.Y.As(unit) + matrix.M32 * pt.Z.As(unit) + matrix.M42;
            double newZ = matrix.M13 * pt.X.As(unit) + matrix.M23 * pt.Y.As(unit) + matrix.M33 * pt.Z.As(unit) + matrix.M43;
            return new Point3d(new Length(newX, unit), new Length(newY, unit), new Length(newZ, unit));
        }

        /// <summary>
        /// Returns the barycenter of the 3D points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point3d GetBarycenter(IList<IPoint3d> points)
        {
            return Utility.GetCenter(points);
        }

        /// <summary>
        /// Returns the distance between the 3D points p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Length Distance<P>(P p1, P p2) where P : ICartesian3d<Length, Length, Length>
        {
            return Utility.GetDistance(p1, p2);
        }

        public static Point3d PlaneLineIntersection(IList<IPoint3d> linePoints, IList<IPoint3d> planePoints, bool within = true)
        {
            IPoint3d p1 = planePoints[0];
            IPoint3d p2 = planePoints[1];
            IPoint3d p3 = planePoints[2];
            LengthUnit unit = p1.X.Unit;
            var n = new Vector3d
            (
                (p2.Y.As(unit) - p1.Y.As(unit)) * (p3.Z.As(unit) - p1.Z.As(unit)) - (p2.Z.As(unit) - p1.Z.As(unit)) * (p3.Y.As(unit) - p1.Y.As(unit)),
                (p2.Z.As(unit) - p1.Z.As(unit)) * (p3.X.As(unit) - p1.X.As(unit)) - (p2.X.As(unit) - p1.X.As(unit)) * (p3.Z.As(unit) - p1.Z.As(unit)),
                (p2.X.As(unit) - p1.X.As(unit)) * (p3.Y.As(unit) - p1.Y.As(unit)) - (p2.Y.As(unit) - p1.Y.As(unit)) * (p3.X.As(unit) - p1.X.As(unit)),
                unit
            );
            IPoint3d l1 = linePoints[0];
            IPoint3d l2 = linePoints[1];
            var l = new Vector3d
            (
                l2.X - l1.X,
                l2.Y - l1.Y,
                l2.Z - l1.Z
            );

            var v = new Vector3d
            (
                p1.X - l1.X,
                p1.Y - l1.Y,
                p1.Z - l1.Z
            );

            if (Vector3d.ScalarProduct(n, l) == 0)
            {
                if (Vector3d.ScalarProduct(n, v) == 0)
                {
                    return new Point3d(l1);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                double d = Vector3d.ScalarProduct(v, n) / Vector3d.ScalarProduct(l, n);
                var p = new Point3d
                (
                    d * l.X + l1.X,
                    d * l.Y + l1.Y,
                    d * l.Z + l1.Z
                );

                if (within)
                {
                    if (Math.Abs((Distance(p, l1) + Distance(p, l2) - Distance(l1, l2)).As(unit)) < 1E-5)
                        return p;
                    else
                        return null;
                }
                else
                    return p;
            }
        }

        public static bool IsInsidePlane(IPoint3d p, IList<IPoint3d> vertices)
        {
            IPoint3d p1 = vertices[0];
            IPoint3d p2 = vertices[1];
            IPoint3d p3 = vertices[2];

            // we check the point p is contained in the plane
            LengthUnit unit = p.X.Unit;
            var n = new Vector3d
            (
                (p2.Y.As(unit) - p1.Y.As(unit)) * (p3.Z.As(unit) - p1.Z.As(unit)) - (p2.Z.As(unit) - p1.Z.As(unit)) * (p3.Y.As(unit) - p1.Y.As(unit)),
                (p2.Z.As(unit) - p1.Z.As(unit)) * (p3.X.As(unit) - p1.X.As(unit)) - (p2.X.As(unit) - p1.X.As(unit)) * (p3.Z.As(unit) - p1.Z.As(unit)),
                (p2.X.As(unit) - p1.X.As(unit)) * (p3.Y.As(unit) - p1.Y.As(unit)) - (p2.Y.As(unit) - p1.Y.As(unit)) * (p3.X.As(unit) - p1.X.As(unit)),
                unit
            );
            var v = new Vector3d
            (
                p.X - p1.X,
                p.Y - p1.Y,
                p.Z - p1.Z
            );
            double sp = Vector3d.ScalarProduct(n, v);
            if (Math.Abs(sp) <= 1E-3)
            {
                //Computation of the inverse matrix;
                double[][] A = new double[3][];
                A[0] = new double[3] { p1.X.As(unit), p1.Y.As(unit), p1.Z.As(unit) };
                A[1] = new double[3] { p2.X.As(unit), p2.Y.As(unit), p2.Z.As(unit) };
                A[2] = new double[3] { p3.X.As(unit), p3.Y.As(unit), p3.Z.As(unit) };

                double det = A[0][0] * A[1][1] * A[2][2] + A[1][0] * A[2][1] * A[0][2] + A[2][0] * A[0][1] * A[1][2]
                    - A[2][0] * A[1][1] * A[0][2] - A[2][1] * A[1][2] * A[0][0] - A[2][2] * A[1][0] * A[0][1];

                double[][] Ainv = new double[3][];
                Ainv[0] = new double[3]
                {
                    (A[1][1] * A[2][2] - A[2][1] * A[1][2]) / det, (A[0][2] * A[2][1] - A[2][2] * A[0][1]) / det, (A[0][1] * A[1][2] - A[1][1] * A[0][2]) / det
                };
                Ainv[1] = new double[3]
                {
                    (A[1][2] * A[2][0] - A[2][2] * A[1][0]) / det, (A[0][0] * A[2][2] - A[2][0] * A[0][2]) / det, (A[0][2] * A[1][0] - A[1][2] * A[0][0]) / det
                };
                Ainv[2] = new double[3]
                {
                    (A[1][0] * A[2][1] - A[2][0] * A[1][1]) / det, (A[0][1] * A[2][0] - A[2][1] * A[0][0]) / det, (A[0][0] * A[1][1] - A[1][0] * A[0][1]) / det
                };

                double a = Ainv[0][1];
                double b = Ainv[1][1];
                double c = Ainv[2][1];
                double d = Ainv[0][2];
                double e = Ainv[1][2];
                double f = Ainv[2][2];

                var p2d = new Point2d
                (
                    a * p.X + b * p.Y + c * p.Z,
                    d * p.X + e * p.Y + f * p.Z
                );

                return Point2d.IsInside(new List<IPoint2d>
                {
                    new Point2d(Length.Zero, Length.Zero),
                    new Point2d(1, 0, unit),
                    new Point2d(0, 1, unit)
                }, p2d);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "3D Point " + CoordinatesToString();
        }

        public string CoordinatesToString()
        {
            return $"(X:{X.ToString().Replace(" ", "\u2009")}, " +
                    $"Y:{Y.ToString().Replace(" ", "\u2009")}, " +
                    $"Z:{Z.ToString().Replace(" ", "\u2009")})";
        }
    }
}
