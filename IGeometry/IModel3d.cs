using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public interface IModel3d : ICartesianModel3d<IMesh, IVertex, IFace, ICoordinate,
        IText3d, IPoint3d, IVector3d, Length, Length, Length, Length>
    {
        IEnumerable<IPolyline3d> Polylines { get; }
        IEnumerable<ILine3d> Lines { get; }
        IEnumerable<IPoint3d> Points { get; }
    }
}
