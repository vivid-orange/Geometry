using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Coordinate : ICoordinate
    {
        public Ratio U { get; set; } = Ratio.Zero;
        public Ratio V { get; set; } = Ratio.Zero;

        public Coordinate() { }

        public Coordinate(double u, double v)
        {
            U = new Ratio(u, RatioUnit.DecimalFraction);
            V = new Ratio(v, RatioUnit.DecimalFraction);
        }

        public Coordinate(Ratio u, Ratio v)
        {
            U = u;
            V = v.ToUnit(u.Unit);
        }

        public Coordinate(double u, double v, RatioUnit unit)
        {
            U = new Ratio(u, unit);
            V = new Ratio(v, unit);
        }

        public override string ToString()
        {
            return $"Coordinate (U:{U};V:{V}";
        }
    }
}
