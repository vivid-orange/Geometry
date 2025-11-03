using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianVector3d<Txyz, Tx, Ty, Tz> : ICartesian3d<Tx, Ty, Tz>
        where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
        where Txyz : IQuantity
    {
        Txyz Length { get; }
    }
}
