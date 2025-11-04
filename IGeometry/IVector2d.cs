using UnitsNet;

namespace VividOrange.Geometry
{
    public interface IVector2d : IPoint2d
    {
        Length Length { get; }
    }
}
