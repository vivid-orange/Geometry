namespace VividOrange.Geometry
{
    public interface IColor : IGeometryBase
    {
        int ColorInt { get; }
        byte Alpha { get; }
        byte Red { get; }
        byte Green { get; }
        byte Blue { get; }
    }
}
