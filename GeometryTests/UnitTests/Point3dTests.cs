using GeometryTests.Utility;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.UnitTests
{
    public class Point3dTests
    {
        [Fact]
        public void CreatePointTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt = new Point3d(x, y, z);

            // Assert
            TestUtility.TestLengthsAreEqual(x, pt.X);
            TestUtility.TestLengthsAreEqual(y, pt.Y);
            TestUtility.TestLengthsAreEqual(z, pt.Z);
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

            // Assert
            Assert.Equal("3D Point (X:2.3 cm, Y:5.4 cm, Z:6.8 cm)", pt.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void PointsAreEqualTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(2.3, LengthUnit.Centimeter);
            var y2 = new Length(5.4, LengthUnit.Centimeter);
            var z2 = new Length(6.8, LengthUnit.Centimeter);
            var x3 = new Length(666, LengthUnit.Centimeter);
            var y3 = new Length(777, LengthUnit.Centimeter);
            var z3 = new Length(888, LengthUnit.Centimeter);

            // Act
            var pt1 = new Point3d(x1, y1, z1);
            var pt2 = new Point3d(x2, y2, z2);
            var pt3 = new Point3d(x3, y3, z3);

            // Assert
            Assert.True(pt1.Equals(pt2));
            Assert.False(pt1.Equals(pt3));
        }

        [Fact]
        public void PointSurvivesJsonRoundtripTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            IPoint3d pt = new Point3d(x, y, z);
            string json = pt.ToJson();
            IPoint3d ptDeserialized = json.FromJson<Point3d>();

            // Assert
            TestUtility.TestLengthsAreEqual(pt.X, ptDeserialized.X);
            TestUtility.TestLengthsAreEqual(pt.Y, ptDeserialized.Y);
            TestUtility.TestLengthsAreEqual(pt.Z, ptDeserialized.Z);
        }

        [Fact]
        public void PointCastToVectorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var vector = (Vector3d)pt;

            // Assert
            TestUtility.TestLengthsAreEqual(x, vector.X);
            TestUtility.TestLengthsAreEqual(y, vector.Y);
            TestUtility.TestLengthsAreEqual(z, vector.Z);
        }

        [Fact]
        public void PointCastToVertexTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var vertex = (Vertex)pt;

            // Assert
            TestUtility.TestLengthsAreEqual(x, vertex.X);
            TestUtility.TestLengthsAreEqual(y, vertex.Y);
            TestUtility.TestLengthsAreEqual(z, vertex.Z);
        }

        [Fact]
        public void PointMinusOperatorTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var z1 = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(2.3, LengthUnit.Centimeter);
            var y2 = new Length(5.4, LengthUnit.Centimeter);
            var z2 = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var pt1 = new Point3d(x1, y1, z1);
            var pt2 = new Point3d(x2, y2, z2);
            Vector3d vector = pt1 - pt2;

            // Assert
            Assert.Equal(0, vector.X.Value);
            Assert.Equal(0, vector.Y.Value);
            Assert.Equal(0, vector.Z.Value);
        }

        [Fact]
        public void PointAdditionVectorOperatorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(10, LengthUnit.Centimeter);
            var y2 = new Length(10, LengthUnit.Centimeter);
            var z2 = new Length(10, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var vect = new Vector3d(x2, y2, z2);
            Point3d ptMoved = pt + vect;

            // Assert
            Assert.Equal(2.3 + 10, ptMoved.X.Value);
            Assert.Equal(5.4 + 10, ptMoved.Y.Value);
            Assert.Equal(6.8 + 10, ptMoved.Z.Value);
        }

        [Fact]
        public void PointAdditionPointOperatorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);
            var x2 = new Length(10, LengthUnit.Centimeter);
            var y2 = new Length(10, LengthUnit.Centimeter);
            var z2 = new Length(10, LengthUnit.Centimeter);

            // Act
            var pt = new Point3d(x, y, z);
            var pt2 = new Point3d(x2, y2, z2);
            Point3d ptMoved = pt + pt2;

            // Assert
            Assert.Equal(2.3 + 10, ptMoved.X.Value);
            Assert.Equal(5.4 + 10, ptMoved.Y.Value);
            Assert.Equal(6.8 + 10, ptMoved.Z.Value);
        }
    }
}
