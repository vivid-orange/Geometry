using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ILocalCartesian2d<Ty, Tz> : IGeometryBase
        where Ty : IQuantity where Tz : IQuantity
    {
        Ty Y { get; }
        Tz Z { get; }
    }
}
