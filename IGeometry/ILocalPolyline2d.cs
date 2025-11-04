using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ILocalPolyline2d : IGeometryBase, IPolylineBase<ILocalDomain2d, ILocalPoint2d>
    {
        IList<ILocalPoint2d> Points { get; }
        bool IsClockwise();

        /// <summary>
        /// A clockwise Polyline will offset outwards (increasing the area) for a positive distance
        /// </summary>
        /// <param name="distance">The perpendicular distance between new and original Polyline</param>
        /// <returns>Returns a new, offset Polyline</returns>
        ILocalPolyline2d Offset(Length distance);
    }
}
