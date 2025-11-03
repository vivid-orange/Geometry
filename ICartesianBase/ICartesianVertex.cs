using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianVertex<TCoordinate, Tx, Ty, Tz> : ICartesian3d<Tx, Ty, Tz>
        where TCoordinate : ICoordinate
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        TCoordinate TextureCoordinate { get; }
    }
}
