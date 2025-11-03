namespace VividOrange.Geometry
{
    public class IntersectionResult : IIntersectionResult
    {
        public IntersectionType IntersectionType { get; set; }
        public IPoint2d Point { get; set; }

        public IntersectionResult(IntersectionType intersectionType, IPoint2d point)
        {
            Point = point;
            IntersectionType = intersectionType;
        }
    }
}
