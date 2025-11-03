namespace VividOrange.Geometry
{
    public class Domain : ADomain<IPoint3d>, IDomain
    {
        public Domain(IPoint3d max, IPoint3d min) : base(max, min) { }
    }
}
