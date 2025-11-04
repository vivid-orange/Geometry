using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;

namespace GeometryTests.UnitTests
{
    public class Point2dTests
    {
        [Fact]
        public void CreatePointTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            IPoint2d pt = new Point2d(u, v);

            // Assert
            TestUtility.TestLengthsAreEqual(u, pt.U);
            TestUtility.TestLengthsAreEqual(v, pt.V);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var pt = new Point2d(u, v);

            // Assert
            Assert.Equal("2D Point (U:2.3 cm, V:5.4 cm)", pt.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void PointsAreEqualTest()
        {
            // Assemble
            var u1 = new Length(2.3, LengthUnit.Centimeter);
            var v1 = new Length(5.4, LengthUnit.Centimeter);
            var u2 = new Length(2.3, LengthUnit.Centimeter);
            var v2 = new Length(5.4, LengthUnit.Centimeter);
            var u3 = new Length(666, LengthUnit.Centimeter);
            var v3 = new Length(777, LengthUnit.Centimeter);

            // Act
            var pt1 = new Point2d(u1, v1);
            var pt2 = new Point2d(u2, v2);
            var pt3 = new Point2d(u3, v3);

            // Assert
            Assert.True(pt1.Equals(pt2));
            Assert.False(pt1.Equals(pt3));
        }

        [Fact]
        public void PointSurvivesJsonRoundtripTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            IPoint2d pt = new Point2d(u, v);
            string json = pt.ToJson();
            IPoint2d ptDeserialized = json.FromJson<Point2d>();

            // Assert
            TestUtility.TestLengthsAreEqual(pt.U, ptDeserialized.U);
            TestUtility.TestLengthsAreEqual(pt.V, ptDeserialized.V);
        }

        [Fact]
        public void PointCastToVectorTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var pt = new Point2d(u, v);
            var vector = (Vector2d)pt;

            // Assert
            TestUtility.TestLengthsAreEqual(u, vector.U);
            TestUtility.TestLengthsAreEqual(v, vector.V);
        }

        [Fact]
        public void PointMinusOperatorTest()
        {
            // Assemble
            var x1 = new Length(2.3, LengthUnit.Centimeter);
            var y1 = new Length(5.4, LengthUnit.Centimeter);
            var x2 = new Length(2.3, LengthUnit.Centimeter);
            var y2 = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var pt1 = new Point2d(x1, y1);
            var pt2 = new Point2d(x2, y2);
            Vector2d vector = pt1 - pt2;

            // Assert
            Assert.Equal(0, vector.U.Value);
            Assert.Equal(0, vector.V.Value);
        }

        [Fact]
        public void PointMultiplyOperatorTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var pt = new Point2d(u, v);
            double scalar = 5.6;
            Point2d ptScaled = scalar * pt;

            // Assert
            Assert.Equal(scalar * 2.3, ptScaled.U.Value);
            Assert.Equal(scalar * 5.4, ptScaled.V.Value);
        }
    }
}
