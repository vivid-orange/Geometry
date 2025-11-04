using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class Vertex : IVertex
    {
        public Length X { get; set; } = Length.Zero;
        public Length Y { get; set; } = Length.Zero;
        public Length Z { get; set; } = Length.Zero;
        public ICoordinate TextureCoordinate { get; set; } = new Coordinate();

        public Vertex() { }

        public Vertex(Length x, Length y, Length z)
        {
            X = x;
            Y = y.ToUnit(x.Unit);
            Z = z.ToUnit(x.Unit);
        }

        public Vertex(double x, double y, double z, LengthUnit unit)
        {
            X = new Length(x, unit);
            Y = new Length(y, unit);
            Z = new Length(z, unit);
        }

        public Vertex(IPoint3d point, ICoordinate textureCoords)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
            TextureCoordinate = textureCoords;
        }

        public static implicit operator Point3d(Vertex v)
        {
            return new Point3d(v.X, v.Y, v.Z);
        }

        public override string ToString()
        {
            return "Vertex " + CoordinatesToString();
        }

        public string CoordinatesToString()
        {
            return $"(X:{X.ToString().Replace(" ", "\u2009")}, " +
                    $"Y:{Y.ToString().Replace(" ", "\u2009")}, " +
                    $"Z:{Z.ToString().Replace(" ", "\u2009")})";
        }
    }
}
