using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianMesh<TVertex, TFace, TCoordinate, Tx, Ty, Tz> : IGeometryBase
        where TVertex : ICartesianVertex<TCoordinate, Tx, Ty, Tz>
        where TFace : ICartesianFace<TVertex, TCoordinate, Tx, Ty, Tz>
        where TCoordinate : ICoordinate
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        IList<int[]> MeshIndices { get; }
        IList<TVertex> Verticies { get; }
        IList<TFace> Faces { get; }
        double Opacity { get; set; }
        IBrush Brush { get; set; }
    }
}
