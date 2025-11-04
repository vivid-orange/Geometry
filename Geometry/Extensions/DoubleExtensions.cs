using System;

namespace VividOrange.Geometry.Extensions
{
    public static class DoubleExtensions
    {
        public static double RoundToSignificantFigure(this double d, int digits)
        {
            if (d == 0)
                return 0;

            decimal scale = (decimal)Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);

            return (double)(scale * Math.Round((decimal)d / scale, digits));
        }
    }
}
