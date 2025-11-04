using System;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public static partial class Utility
    {
        public static Area GetArea<T>(T p1, T p2, T p3) where T : ICartesian3d<Length, Length, Length>
        {
            LengthUnit unit = p1.X.Unit;
            double a = GetLength(p1, p2, unit);
            double b = GetLength(p2, p3, unit);
            double c = GetLength(p3, p1, unit);

            double s = (a + b + c) / 2;
            AreaUnit area = Area.ParseUnit(Length.GetAbbreviation(unit) + "²");
            return new Area(Math.Sqrt(s * (s - a) * (s - b) * (s - c)), area);
        }

        private static double GetLength<T>(T p1, T p2, LengthUnit unit) where T : ICartesian3d<Length, Length, Length>
        {
            return Math.Sqrt(Math.Pow(p1.X.As(unit) - p2.X.As(unit), 2)
                + Math.Pow(p1.Y.As(unit) - p2.Y.As(unit), 2)
                + Math.Pow(p1.Z.As(unit) - p2.Z.As(unit), 2));
        }
    }
}
