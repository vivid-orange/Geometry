namespace VividOrange.Geometry
{
    public interface ILocalCoordinateSystem : IGeometryBase
    {
        IVector3d XAxis { get; }
        IVector3d YAxis { get; }
        IVector3d ZAxis { get; }
        IPoint3d Origin { get; }
    }
}
