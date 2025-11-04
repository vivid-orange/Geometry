using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;

namespace GeometryTests.UnitTests
{
    public class VertexTests
    {
        [Fact]
        public void CreateVertexTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var txtCoord = new Coordinate();
            var vertex = new Vertex(pt, txtCoord);

            // Assert
            TestUtility.TestLengthsAreEqual(x, vertex.X);
            TestUtility.TestLengthsAreEqual(y, vertex.Y);
            TestUtility.TestLengthsAreEqual(z, vertex.Z);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var txtCoord = new Coordinate();
            var vertex = new Vertex(pt, txtCoord);

            // Assert
            Assert.Equal("Vertex (X:2.3 cm, Y:5.4 cm, Z:6.8 cm)", vertex.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void VertexSurvivesJsonRoundtripTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var txtCoord = new Coordinate();
            var vertex = new Vertex(pt, txtCoord);
            string json = vertex.ToJson();
            IVertex vectDeserialized = json.FromJson<Vertex>();

            // Assert
            TestUtility.TestLengthsAreEqual(x, vectDeserialized.X);
            TestUtility.TestLengthsAreEqual(y, vectDeserialized.Y);
            TestUtility.TestLengthsAreEqual(z, vectDeserialized.Z);
        }

        [Fact]
        public void VertexCastToVectorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var p = new Point3d(x, y, z);
            var txtCoord = new Coordinate();
            var vertex = new Vertex(p, txtCoord);
            var pt = (Point3d)vertex;

            // Assert
            TestUtility.TestLengthsAreEqual(x, pt.X);
            TestUtility.TestLengthsAreEqual(y, pt.Y);
            TestUtility.TestLengthsAreEqual(z, pt.Z);
        }
    }
}
