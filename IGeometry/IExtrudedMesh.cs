using System.Collections.Generic;

namespace VividOrange.Geometry
{
    public interface IExtrudedMesh : IGeometryBase
    {
        IList<IPoint3d> Points { get; }
        IVector3d Direction { get; }
    }
}
