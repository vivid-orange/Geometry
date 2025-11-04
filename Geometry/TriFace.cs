using UnitsNet;

namespace VividOrange.Geometry
{
    public class TriFace : ITriFace, IFace
    {
        public IVertex A { get; }
        public IVertex B { get; }
        public IVertex C { get; }
        public IVertex Center
        {
            get
            {
                _center ??= Utility.GetCenter(A, B, C);
                return _center;
            }
        }
        public IQuantity Area
        {
            get
            {
                _area ??= Utility.GetArea(A, B, C);
                return _area;
            }
        }

        private IVertex _center = null;
        private Area? _area = null;

        public TriFace(IVertex a, IVertex b, IVertex c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
