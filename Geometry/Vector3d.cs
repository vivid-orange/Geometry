using System;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Vector3d : IVector3d
    {
        public Length X { get; set; }
        public Length Y { get; set; }
        public Length Z { get; set; }
        public Length Length
        {
            get
            {
                LengthUnit unit = X.Unit;
                double area = Math.Pow(X.As(unit), 2) + Math.Pow(Y.As(unit), 2) + Math.Pow(Z.As(unit), 2);
                return new Length(Math.Sqrt(area), unit);
            }
        }

        private Vector3d() { }

        public Vector3d(Length x, Length y, Length z)
        {
            X = x;
            Y = y.ToUnit(x.Unit);
            Z = z.ToUnit(x.Unit);
        }

        public Vector3d(double x, double y, double z, LengthUnit unit)
        {
            X = new Length(x, unit);
            Y = new Length(y, unit);
            Z = new Length(z, unit);
        }

        public Vector3d(IVector3d other)
        {
            X = other.X;
            Y = other.Y.ToUnit(other.X.Unit);
            Z = other.Z.ToUnit(other.X.Unit);
        }

        public static implicit operator Point3d(Vector3d vect)
        {
            return new Point3d(vect.X, vect.Y, vect.Z);
        }

        public Vector3d Normalised()
        {
            LengthUnit unit = X.Unit;
            double magnitude = Math.Pow(Math.Pow(X.As(unit), 2) + Math.Pow(Y.As(unit), 2) + Math.Pow(Z.As(unit), 2), 1d / 2d);
            return new Vector3d(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public Vector3d CrossProduct(IVector3d other)
        {
            return CrossProduct(this, other);
        }

        public static Vector3d CrossProduct<V>(V v1, V v2) where V : ICartesian3d<Length, Length, Length>
        {
            LengthUnit unit = v1.X.Unit;
            double x = v1.Y.As(unit) * v2.Z.As(unit) - v2.Y.As(unit) * v1.Z.As(unit);
            double y = (v1.X.As(unit) * v2.Z.As(unit) - v2.X.As(unit) * v1.Z.As(unit)) * -1;
            double z = v1.X.As(unit) * v2.Y.As(unit) - v2.X.As(unit) * v1.Y.As(unit);
            return new Vector3d(new Length(x, unit), new Length(y, unit), new Length(z, unit));
        }

        public static double ScalarProduct<V>(V u, V v) where V : IVector3d
        {
            LengthUnit unit = u.X.Unit;
            return u.X.As(unit) * v.X.As(unit) + u.Y.As(unit) * v.Y.As(unit) + u.Z.As(unit) * v.Z.As(unit);
        }

        public static Angle VectorAngle<V>(V v1, V v2) where V : IVector3d
        {
            LengthUnit unit = v1.X.Unit;
            double angle = Math.Acos(ScalarProduct(v1, v2)) / (v1.Length.As(unit) * v2.Length.As(unit));
            return new Angle(angle, AngleUnit.Radian);
        }

        public static Area TriangleArea<V>(V v1, V v2) where V : IVector3d
        {
            return 0.5 * v1.Length * v2.Length * Math.Abs(Math.Sin(VectorAngle(v1, v2).Radians));
        }

        public static Vector3d VectorialProduct<V>(V v1, V v2) where V : IVector3d
        {
            LengthUnit unit = v1.X.Unit;
            return new Vector3d
            (
                new Length(v1.Y.As(unit) * v2.Z.As(unit) - v1.Z.As(unit) * v2.Y.As(unit), unit),
                new Length(v1.Z.As(unit) * v2.X.As(unit) - v1.X.As(unit) * v2.Z.As(unit), unit),
                new Length(v1.X.As(unit) * v2.Y.As(unit) - v1.Y.As(unit) * v2.X.As(unit), unit)
            );
        }

        public static Vector3d UnitX => new Vector3d(new Length(1, LengthUnit.Meter), Length.Zero, Length.Zero);
        public static Vector3d UnitY => new Vector3d(Length.Zero, new Length(1, LengthUnit.Meter), Length.Zero);
        public static Vector3d UnitZ => new Vector3d(Length.Zero, Length.Zero, new Length(1, LengthUnit.Meter));

        public override string ToString()
        {
            return "3D Vector " + CoordinatesToString();
        }

        public string CoordinatesToString()
        {
            return $"(X:{X.ToString().Replace(" ", "\u2009")}, " +
                    $"Y:{Y.ToString().Replace(" ", "\u2009")}, " +
                    $"Z:{Z.ToString().Replace(" ", "\u2009")})";
        }
    }
}
