using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;

namespace GeometryTests.FunctionalTests
{
    public class MeshTests
    {
        [Fact]
        public void CreateMeshAddVertexTest()
        {
            // Assemble
            var x1 = new Length(0, LengthUnit.Centimeter);
            var y1 = new Length(0, LengthUnit.Centimeter);
            var z1 = new Length(1.2, LengthUnit.Centimeter);

            var x2 = new Length(0, LengthUnit.Centimeter);
            var y2 = new Length(5.4, LengthUnit.Centimeter);
            var z2 = new Length(0, LengthUnit.Centimeter);

            var x3 = new Length(2.3, LengthUnit.Centimeter);
            var y3 = new Length(5.4, LengthUnit.Centimeter);
            var z3 = new Length(1.2, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();

            var p1 = new Point3d(x1, y1, z1);
            var v1 = new Vertex(p1, new Coordinate());
            m.AddVertex(v1);

            var v2 = new Vertex(x2, y2, z2);
            m.AddVertex(v2);

            var v3 = new Vertex(x3.Value, y3.Value, z3.Value, LengthUnit.Centimeter);
            m.AddVertex(v3);

            m.SetIndices(new List<int[]>() { new int[] { 0, 1, 2 } });

            // Assert
            Assert.Equal(3, m.Verticies.Count());
            Assert.Single(m.Faces);
            TestUtility.TestLengthsAreEqual(x1, m.Verticies[0].X);
            TestUtility.TestLengthsAreEqual(y1, m.Verticies[0].Y);
            TestUtility.TestLengthsAreEqual(z1, m.Verticies[0].Z);
            TestUtility.TestLengthsAreEqual(x2, m.Verticies[1].X);
            TestUtility.TestLengthsAreEqual(y2, m.Verticies[1].Y);
            TestUtility.TestLengthsAreEqual(z2, m.Verticies[1].Z);
            TestUtility.TestLengthsAreEqual(x3, m.Verticies[2].X);
            TestUtility.TestLengthsAreEqual(y3, m.Verticies[2].Y);
            TestUtility.TestLengthsAreEqual(z3, m.Verticies[2].Z);
        }

        [Fact]
        public void CreateTriMeshTest()
        {
            // Assemble
            var x1 = new Length(0, LengthUnit.Centimeter);
            var y1 = new Length(0, LengthUnit.Centimeter);
            var z1 = new Length(0, LengthUnit.Centimeter);

            var x2 = new Length(0, LengthUnit.Centimeter);
            var y2 = new Length(3, LengthUnit.Centimeter);
            var z2 = new Length(3, LengthUnit.Centimeter);

            var x3 = new Length(4, LengthUnit.Centimeter);
            var y3 = new Length(0, LengthUnit.Centimeter);
            var z3 = new Length(4, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();

            var p1 = new Point3d(x1, y1, z1);
            var v1 = new Vertex(p1, new Coordinate());
            m.AddVertex(v1);

            var v2 = new Vertex(x2, y2, z2);
            m.AddVertex(v2);

            var v3 = new Vertex(x3.Value, y3.Value, z3.Value, LengthUnit.Centimeter);
            m.AddVertex(v3);

            m.SetIndices(new List<int[]>() { new int[] { 0, 1, 2 } });

            // Assert
            Assert.Equal(3, m.Verticies.Count());
            Assert.Single(m.Faces);
            Assert.Equal(typeof(TriFace), m.Faces[0].GetType());
            Assert.Equal(10.39, m.Faces[0].Area.As(AreaUnit.SquareCentimeter), 2);
        }

        [Fact]
        public void CreateQuadMeshTest()
        {
            // Assemble
            var x1 = new Length(0, LengthUnit.Centimeter);
            var y1 = new Length(0, LengthUnit.Centimeter);
            var z1 = new Length(0, LengthUnit.Centimeter);

            var x2 = new Length(0, LengthUnit.Centimeter);
            var y2 = new Length(3, LengthUnit.Centimeter);
            var z2 = new Length(3, LengthUnit.Centimeter);

            var x3 = new Length(4, LengthUnit.Centimeter);
            var y3 = new Length(0, LengthUnit.Centimeter);
            var z3 = new Length(4, LengthUnit.Centimeter);

            var x4 = new Length(3, LengthUnit.Centimeter);
            var y4 = new Length(-3, LengthUnit.Centimeter);
            var z4 = new Length(0, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();

            var p1 = new Point3d(x1, y1, z1);
            var v1 = new Vertex(p1, new Coordinate());
            m.AddVertex(v1);

            var v2 = new Vertex(x2, y2, z2);
            m.AddVertex(v2);

            var v3 = new Vertex(x3.Value, y3.Value, z3.Value, LengthUnit.Centimeter);
            m.AddVertex(v3);

            var v4 = new Vertex(x4, y4, z4);
            m.AddVertex(v4);

            m.SetIndices(new List<int[]>() { new int[] { 0, 1, 2, 3 } });

            // Assert
            Assert.Equal(4, m.Verticies.Count());
            Assert.Single(m.Faces);
            Assert.Equal(typeof(QuadFace), m.Faces[0].GetType());
            Assert.Equal(20.78, m.Faces[0].Area.As(AreaUnit.SquareCentimeter), 2);
        }

        [Fact]
        public void CreateNgonMeshTest()
        {
            // Assemble
            var x1 = new Length(0, LengthUnit.Centimeter);
            var y1 = new Length(0, LengthUnit.Centimeter);
            var z1 = new Length(0, LengthUnit.Centimeter);

            var x2 = new Length(0, LengthUnit.Centimeter);
            var y2 = new Length(3, LengthUnit.Centimeter);
            var z2 = new Length(3, LengthUnit.Centimeter);

            var x3 = new Length(4, LengthUnit.Centimeter);
            var y3 = new Length(0, LengthUnit.Centimeter);
            var z3 = new Length(4, LengthUnit.Centimeter);

            var x4 = new Length(4, LengthUnit.Centimeter);
            var y4 = new Length(-4, LengthUnit.Centimeter);
            var z4 = new Length(0, LengthUnit.Centimeter);

            var x5 = new Length(2, LengthUnit.Centimeter);
            var y5 = new Length(-2, LengthUnit.Centimeter);
            var z5 = new Length(0, LengthUnit.Centimeter);

            // Act
            var m = new Mesh();

            var p1 = new Point3d(x1, y1, z1);
            var v1 = new Vertex(p1, new Coordinate());
            m.AddVertex(v1);

            var v2 = new Vertex(x2, y2, z2);
            m.AddVertex(v2);

            var v3 = new Vertex(x3.Value, y3.Value, z3.Value, LengthUnit.Centimeter);
            m.AddVertex(v3);

            var v4 = new Vertex(x4, y4, z4);
            m.AddVertex(v4);
            var v5 = new Vertex(x5, y5, z5);
            m.AddVertex(v5);

            m.SetIndices(new List<int[]>() { new int[] { 0, 1, 2, 3, 4 } });

            // Assert
            Assert.Equal(5, m.Verticies.Count());
            Assert.Single(m.Faces);
            Assert.Equal(typeof(NgonFace), m.Faces[0].GetType());
            Assert.Equal(24.2487, m.Faces[0].Area.As(AreaUnit.SquareCentimeter), 4);
        }
    }
}
