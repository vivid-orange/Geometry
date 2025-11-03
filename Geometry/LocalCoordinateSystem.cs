using System;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public class LocalCoordinateSystem : ILocalCoordinateSystem
    {
        public IVector3d XAxis { get; set; } = Vector3d.UnitX;
        public IVector3d YAxis { get; set; } = Vector3d.UnitY;
        public IVector3d ZAxis { get; set; } = Vector3d.UnitZ;
        public IPoint3d Origin { get; set; } = new Point3d();

        public LocalCoordinateSystem() { }

        public LocalCoordinateSystem(IVector3d xaxis, IVector3d yaxis, IVector3d zaxis, IPoint3d origin)
        {
            XAxis = xaxis;
            YAxis = yaxis;
            ZAxis = zaxis;
            Origin = origin;
        }

        public static ILocalCoordinateSystem LocalCoordSystemFromLinePoints(ILine3d ln)
        {
            return LocalCoordSystemFromLinePoints(ln.Start, ln.End);
        }

        public static ILocalCoordinateSystem LocalCoordSystemFromLinePoints(IPoint3d point1, IPoint3d point2)
        {
            LengthUnit unit = point1.X.Unit;
            Vector3d normal = ((Point3d)point2) - ((Point3d)point1);
            Vector3d newX;
            if (Math.Abs(normal.Z.As(unit)) < 0.99)
            {
                newX = Vector3d.CrossProduct(normal, Vector3d.UnitZ);
            }
            else
            {
                newX = Vector3d.CrossProduct(normal, Vector3d.UnitX);
            }

            Vector3d newY = Vector3d.CrossProduct(normal, newX);
            Vector3d newX2 = newX.Normalised();
            Vector3d newY2 = newY.Normalised();
            Vector3d newZ2 = normal.Normalised();

            return new LocalCoordinateSystem(newX2, newY2, newZ2, point1);
        }

        public override string ToString()
        {
            return $"LocalCoordinateSystem (O:{Origin};X:{XAxis};Y:{YAxis};Z:{ZAxis})";
        }
    }
}
