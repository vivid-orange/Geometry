using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry.Extensions
{
    internal static class LengthEquatable
    {
        internal static bool IsEqual(this Length length, Length other, double tolerance = 1.0e-12)
        {
            return length.Equals(other, new Length(tolerance, length.Unit));
        }

        internal static bool IsEqual(this List<Length> lengths, List<Length> others, double tolerance = 1.0e-12)
        {
            if (lengths.Count != others.Count)
            {
                return false;
            }

            for (int i = 0; i < lengths.Count; i++)
            {
                if (lengths[i].IsEqual(others[i], tolerance) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
