using UnitsNet;

namespace VividOrange.Geometry
{
    public class QuadFace : IQuadFace, IFace
    {
        public IVertex A { get; }
        public IVertex B { get; }
        public IVertex C { get; }
        public IVertex D { get; }
        public IVertex Center
        {
            get
            {
                _center ??= Utility.GetCenter(A, B, C, D);
                return _center;
            }
        }
        public IQuantity Area
        {
            get
            {
                _area ??= Utility.GetArea(A, B, C) + Utility.GetArea(C, D, A);
                return _area;
            }
        }

        private IVertex _center = null;
        private Area? _area = null;

        public QuadFace(IVertex a, IVertex b, IVertex c, IVertex d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }
}
