using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianFace<TVertex, TCoordinate, Tx, Ty, Tz> : IGeometryBase
        where TVertex : ICartesianVertex<TCoordinate, Tx, Ty, Tz>
        where TCoordinate : ICoordinate
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        TVertex Center { get; }
        IQuantity Area { get; }
    }
}
