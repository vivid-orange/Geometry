namespace VividOrange.Geometry
{
    public interface IExtrema<T> : IGeometryBase
    {
        T Max { get; }
        T Min { get; }
    }
}
