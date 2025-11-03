namespace VividOrange.Geometry
{
    public interface IPolylineBase<D, T> where D : IExtrema<T>
    {
        bool IsClosed { get; }
        D Domain();
    }
}
