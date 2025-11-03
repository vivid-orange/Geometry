using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianNgonFace<TVertex, TCoordinate, Tx, Ty, Tz> : ICartesianFace<TVertex, TCoordinate, Tx, Ty, Tz>
        where TVertex : ICartesianVertex<TCoordinate, Tx, Ty, Tz>
        where TCoordinate : ICoordinate
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        IList<TVertex> Verticies { get; }
    }
}
