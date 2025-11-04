namespace VividOrange.Geometry
{
    public interface ILine2d : IGeometryBase
    {
        IPoint2d Start { get; }
        IPoint2d End { get; }
        IDomain2d Domain();
    }
}
