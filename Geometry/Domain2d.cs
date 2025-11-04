namespace VividOrange.Geometry
{
    public class Domain2d : ADomain<IPoint2d>, IDomain2d
    {
        public Domain2d(IPoint2d max, IPoint2d min) : base(max, min) { }
    }
}
