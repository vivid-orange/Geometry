using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public class NgonFace : INgonFace, IFace
    {
        public IVertex Center
        {
            get
            {
                _center ??= Utility.GetCenter(Verticies);
                return _center;
            }
        }
        public IQuantity Area
        {
            get
            {
                _area ??= CalculateArea();
                return _area;
            }
        }

        public IList<IVertex> Verticies { get; }

        private IVertex _center = null;
        private Area? _area = null;

        public NgonFace(IList<IVertex> vertices)
        {
            Verticies = vertices;
        }

        private Area CalculateArea()
        {
            Area a = UnitsNet.Area.Zero;
            for (int i = 0; i < Verticies.Count - 1; i++)
            {
                a += Utility.GetArea(Verticies[i], Center, Verticies[i + 1]);
            }

            return a + Utility.GetArea(Verticies[Verticies.Count - 1], Center, Verticies[0]);
        }
    }
}
