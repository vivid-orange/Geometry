namespace VividOrange.Geometry
{
    public interface ILine3d : IGeometryBase
    {
        IPoint3d Start { get; }
        IPoint3d End { get; }
        IDomain Domain();
    }
}
