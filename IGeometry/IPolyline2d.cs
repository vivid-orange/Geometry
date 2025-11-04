using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public interface IPolyline2d : IGeometryBase, IPolylineBase<IDomain2d, IPoint2d>
    {
        IList<IPoint2d> Points { get; }
        bool IsClockwise();

        /// <summary>
        /// A clockwise Polyline will offset outwards (increasing the area) for a positive distance
        /// </summary>
        /// <param name="distance">The perpendicular distance between new and original Polyline</param>
        /// <returns>Returns a new, offset Polyline</returns>
        IPolyline2d Offset(Length distance);
    }
}
