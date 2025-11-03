using System;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Vector2d : IVector2d
    {
        public Length U { get; set; }
        public Length V { get; set; }
        public Length Length
        {
            get
            {
                LengthUnit unit = U.Unit;
                double area = Math.Pow(U.As(unit), 2) + Math.Pow(V.As(unit), 2);
                return new Length(Math.Sqrt(area), unit);
            }
        }

        private Vector2d() { }

        public Vector2d(Length x, Length y)
        {
            U = x;
            V = y.ToUnit(x.Unit);
        }

        public Vector2d(double x, double y, LengthUnit unit)
        {
            U = new Length(x, unit);
            V = new Length(y, unit);
        }

        public Vector2d(IVector2d other)
        {
            U = other.U;
            V = other.V.ToUnit(other.U.Unit);
        }

        public Vector2d Normalize()
        {
            LengthUnit unit = U.Unit;
            return new Vector2d(new Length(U / Length, unit), new Length(V / Length, unit));
        }

        public Vector2d Amplitude(Length amplitude)
        {
            LengthUnit unit = amplitude.Unit;
            return new Vector2d(new Length(U.As(unit) * amplitude.As(unit), unit),
                                new Length(V.As(unit) * amplitude.As(unit), unit));
        }

        public static Vector2d operator *(double number, Vector2d point)
        {
            return new Vector2d(point.U * number, point.V * number);
        }

        public static Vector2d operator *(Vector2d point, double number)
        {
            return number * point;
        }

        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.U + v2.U, v1.V + v2.V);
        }

        public static implicit operator Point2d(Vector2d vect)
        {
            return new Point2d(vect.U, vect.V);
        }

        public static double ScalarProduct<V>(V v1, V v2) where V : IVector2d
        {
            LengthUnit unit = v1.U.Unit;
            return v1.U.As(unit) * v2.U.As(unit) + v1.V.As(unit) * v2.V.As(unit);
        }

        public static double CrossProduct<V>(V v1, V v2) where V : IVector2d
        {
            LengthUnit unit = v1.U.Unit;
            return v1.U.As(unit) * v2.V.As(unit) - v1.V.As(unit) * v2.U.As(unit);
        }

        public static Angle VectorAngle<V>(V v1, V v2) where V : IVector2d
        {
            LengthUnit unit = v1.U.Unit;
            double angle = Math.Acos(ScalarProduct(v1, v2) / (v1.Length.As(unit) * v2.Length.As(unit)));
            return new Angle(angle, AngleUnit.Radian);
        }

        public static Vector2d UnitU => new Vector2d(new Length(1, LengthUnit.Meter), Length.Zero);
        public static Vector2d UnitV => new Vector2d(Length.Zero, new Length(1, LengthUnit.Meter));

        public override string ToString()
        {
            return "2D Vector " + CoordinatesToString();
        }

        public string CoordinatesToString()
        {
            return $"(U:{U.ToString().Replace(" ", "\u2009")}, V:{V.ToString().Replace(" ", "\u2009")})";
        }
    }
}
