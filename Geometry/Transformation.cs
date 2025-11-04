using System.Numerics;
using UnitsNet;
using UnitsNet.Units;

namespace VividOrange.Geometry
{
    public static class Transformation
    {
        /// <summary>
        /// Returns transformation matrix setting 2D plane in 3d space to XY plane
        /// </summary>
        /// <param name="pt1 at origin"></param>
        /// <param name="pt2 on x axis"></param>
        /// <param name="pt3 on xy plane"></param>
        /// <returns></returns>
        public static Matrix4x4 GetTransformMatrix2dTo3d<T>(T pt1, T pt2, T pt3) where T : ICartesian3d<Length, Length, Length>
        {
            var xAxis = new Vector3d(pt2.X - pt1.X, pt2.Y - pt1.Y, pt2.Z - pt1.Z);
            var hAxis = new Vector3d(pt3.X - pt1.X, pt3.Y - pt1.Y, pt3.Z - pt1.Z);
            Vector3d zAxis = Vector3d.CrossProduct(xAxis, hAxis);
            Vector3d yAxis = Vector3d.CrossProduct(zAxis, xAxis);

            xAxis = xAxis.Normalised();
            yAxis = yAxis.Normalised();
            zAxis = zAxis.Normalised();

            LengthUnit unit = pt1.X.Unit;
            Matrix4x4 trans = new Matrix4x4((float)xAxis.X.As(unit), (float)xAxis.Y.As(unit), (float)xAxis.Z.As(unit), 0,
                                            (float)yAxis.X.As(unit), (float)yAxis.Y.As(unit), (float)yAxis.Z.As(unit), 0,
                                            (float)zAxis.X.As(unit), (float)zAxis.Y.As(unit), (float)zAxis.Z.As(unit), 0,
                                            (float)pt1.X.As(unit), (float)pt1.Y.As(unit), (float)pt1.Z.As(unit), 1);

            return trans;
        }

        /// <summary>
        /// Returns transformation matrix setting 2D plane in 3d space to XY plane
        /// </summary>
        /// <param name="pt1 at origin"></param>
        /// <param name="pt2 on x axis"></param>
        /// <param name="pt3 on xy plane"></param>
        /// <returns></returns>
        public static Matrix4x4 GetTransformMatrixPlaneIn3dTo2d<T>(T pt1, T pt2, T pt3) where T : ICartesian3d<Length, Length, Length>
        {
            var xAxis = new Vector3d(pt2.X - pt1.X, pt2.Y - pt1.Y, pt2.Z - pt1.Z);
            var hAxis = new Vector3d(pt3.X - pt1.X, pt3.Y - pt1.Y, pt3.Z - pt1.Z);
            Vector3d zAxis = Vector3d.CrossProduct(xAxis, hAxis);
            Vector3d yAxis = Vector3d.CrossProduct(zAxis, xAxis);

            xAxis = xAxis.Normalised();
            yAxis = yAxis.Normalised();
            zAxis = zAxis.Normalised();

            LengthUnit unit = pt1.X.Unit;
            Matrix4x4 trans = new Matrix4x4((float)xAxis.X.As(unit), (float)xAxis.Y.As(unit), (float)xAxis.Z.As(unit), 0,
                                            (float)yAxis.X.As(unit), (float)yAxis.Y.As(unit), (float)yAxis.Z.As(unit), 0,
                                            (float)zAxis.X.As(unit), (float)zAxis.Y.As(unit), (float)zAxis.Z.As(unit), 0,
                                            (float)pt1.X.As(unit), (float)pt1.Y.As(unit), (float)pt1.Z.As(unit), 1);
            Matrix4x4 returnMatrix;
            Matrix4x4.Invert(trans, out returnMatrix);

            return returnMatrix;
        }
    }
}
