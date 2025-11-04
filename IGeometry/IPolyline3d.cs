using System.Collections.Generic;

namespace VividOrange.Geometry
{
    public interface IPolyline3d : IGeometryBase, IPolylineBase<IDomain, IPoint3d>
    {
        IList<IPoint3d> Points { get; }
    }
}
