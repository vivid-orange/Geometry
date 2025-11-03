using System.Collections.Generic;
using UnitsNet;

namespace VividOrange.Geometry
{
    public static partial class Utility
    {
        public static Vertex GetCenter<T>(T a, T b) where T : ICartesian3d<Length, Length, Length>
        {
            return new Vertex()
            {
                X = (a.X + b.X) / 2,
                Y = (a.Y + b.Y) / 2,
                Z = (a.Z + b.Z) / 2,
            };
        }

        public static Vertex GetCenter<T>(T a, T b, T c) where T : ICartesian3d<Length, Length, Length>
        {
            return new Vertex()
            {
                X = (a.X + b.X + c.X) / 3,
                Y = (a.Y + b.Y + c.Y) / 3,
                Z = (a.Z + b.Z + c.Z) / 3,
            };
        }

        public static Vertex GetCenter<T>(T a, T b, T c, T d) where T : ICartesian3d<Length, Length, Length>
        {
            return new Vertex()
            {
                X = (a.X + b.X + c.X + d.X) / 4,
                Y = (a.Y + b.Y + c.Y + d.Y) / 4,
                Z = (a.Z + b.Z + c.Z + d.Z) / 4,
            };
        }

        public static Vertex GetCenter<T>(IList<T> vertices) where T : ICartesian3d<Length, Length, Length>
        {
            Length x = Length.Zero;
            Length y = Length.Zero;
            Length z = Length.Zero;
            foreach (T v in vertices)
            {
                x += v.X;
                y += v.Y;
                z += v.Z;
            }

            return new Vertex()
            {
                X = x / vertices.Count,
                Y = y / vertices.Count,
                Z = z / vertices.Count,
            };
        }

        public static Point2d GetCenterLocal<T>(T a, T b) where T : ICartesian2d<Length, Length>
        {
            return new Point2d()
            {
                U = (a.U + b.U) / 2,
                V = (a.V + b.V) / 2,
            };
        }

        public static Point2d GetCenterLocal<T>(T a, T b, T c) where T : ICartesian2d<Length, Length>
        {
            return new Point2d()
            {
                U = (a.U + b.U + c.U) / 3,
                V = (a.V + b.V + c.V) / 3,
            };
        }

        public static Point2d GetCenterLocal<T>(T a, T b, T c, T d) where T : ICartesian2d<Length, Length>
        {
            return new Point2d()
            {
                U = (a.U + b.U + c.U + d.U) / 4,
                V = (a.V + b.V + c.V + d.V) / 4,
            };
        }

        public static Point2d GetCenterLocal<T>(IList<T> points) where T : ICartesian2d<Length, Length>
        {
            Length u = Length.Zero;
            Length v = Length.Zero;
            foreach (T p in points)
            {
                u += p.U;
                v += p.V;
            }

            return new Point2d()
            {
                U = u / points.Count,
                V = v / points.Count,
            };
        }
    }
}
