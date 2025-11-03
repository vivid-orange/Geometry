using System.Collections.Generic;
using System.Linq;

namespace VividOrange.Geometry.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            if (value == null || value.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}
