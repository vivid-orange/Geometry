using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesian3d<Tx, Ty, Tz> : ILocalCartesian2d<Ty, Tz>
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        Tx X { get; }
    }
}
