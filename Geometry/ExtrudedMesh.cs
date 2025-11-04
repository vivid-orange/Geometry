using System.Collections.Generic;

namespace VividOrange.Geometry
{
    public class ExtrudedMesh : IExtrudedMesh
    {
        public IList<IPoint3d> Points { get; set; }
        public IVector3d Direction { get; set; }

        public ExtrudedMesh(IList<IPoint3d> points, IVector3d direction)
        {
            Points = points;
            Direction = direction;
        }
    }
}
