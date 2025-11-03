using System;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public static partial class Utility
    {
        public static Length GetDistance<P>(P p1, P p2) where P : ICartesian3d<Length, Length, Length>
        {
            LengthUnit unit = p1.X.Unit;
            double area = Math.Pow(p1.X.As(unit) - p2.X.As(unit), 2)
                + Math.Pow(p1.Y.As(unit) - p2.Y.As(unit), 2)
                + Math.Pow(p1.Z.As(unit) - p2.Z.As(unit), 2);
            return new Length(Math.Sqrt(area), unit);
        }
    }
}
