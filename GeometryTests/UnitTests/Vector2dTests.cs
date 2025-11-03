using GeometryTests.Utility;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.UnitTests
{
    public class Vector2dTests
    {
        [Fact]
        public void CreateVectorTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            IVector2d vect = new Vector2d(u, v);

            // Assert
            TestUtility.TestLengthsAreEqual(u, vect.U);
            TestUtility.TestLengthsAreEqual(v, vect.V);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var vect = new Vector2d(u, v);

            // Assert
            Assert.Equal("2D Vector (U:2.3 cm, V:5.4 cm)", vect.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void VectorLengthTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            IVector2d vect = new Vector2d(u, v);
            double expectedLength = Math.Sqrt(2.3 * 2.3 + 5.4 * 5.4);

            // Assert
            Assert.Equal(expectedLength, vect.Length.Value);
        }

        [Fact]
        public void VectorSurvivesJsonRoundtripTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            IVector2d vect = new Vector2d(u, v);
            string json = vect.ToJson();
            IVector2d vectDeserialized = json.FromJson<Vector2d>();

            // Assert
            TestUtility.TestLengthsAreEqual(vect.U, vectDeserialized.U);
            TestUtility.TestLengthsAreEqual(vect.V, vectDeserialized.V);
        }

        [Fact]
        public void VectorMultiplyOperatorTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var vect = new Vector2d(u, v);
            Vector2d scaled = 1.5 * vect;

            // Assert
            Assert.Equal(1.5 * 2.3, scaled.U.Value);
            Assert.Equal(1.5 * 5.4, scaled.V.Value);
        }

        [Fact]
        public void VectorCastToPointTest()
        {
            // Assemble
            var u = new Length(2.3, LengthUnit.Centimeter);
            var v = new Length(5.4, LengthUnit.Centimeter);

            // Act
            var vect = new Vector2d(u, v);
            var pt = (Point2d)vect;

            // Assert
            TestUtility.TestLengthsAreEqual(u, pt.U);
            TestUtility.TestLengthsAreEqual(v, pt.V);
        }
    }
}
