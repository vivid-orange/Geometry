using UnitsNet;

namespace VividOrange.Geometry
{
    public interface ICartesianText3d<TPoint, TVector, Txyz, Tx, Ty, Tz> : IGeometryBase
        where TPoint : ICartesian3d<Tx, Ty, Tz>
        where TVector : ICartesianVector3d<Txyz, Tx, Ty, Tz>
        where Txyz : IQuantity where Tx : IQuantity where Ty : IQuantity where Tz : IQuantity
    {
        TPoint Position { get; }
        TVector Direction { get; }
        TVector Up { get; }
        double Height { get; }
        bool IsDoubleSided { get; }
        string Text { get; }
        VerticalAlignment VerticalAlignment { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        IColor Color { get; }
    }
}
