namespace VividOrange.Geometry
{
    public class LocalDomain2d : ADomain<ILocalPoint2d>, ILocalDomain2d
    {
        public LocalDomain2d(ILocalPoint2d max, ILocalPoint2d min) : base(max, min) { }
    }
}
