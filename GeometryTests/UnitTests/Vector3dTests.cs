using GeometryTests.Utility;
using UnitsNet;
using UnitsNet.Units;
using VividOrange.Geometry;
using VividOrange.Geometry.Serialization.Extensions;

namespace GeometryTests.UnitTests
{
    public class Vector3dTests
    {
        [Fact]
        public void CreateVectorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            IVector3d vector = new Vector3d(x, y, z);

            // Assert
            TestUtility.TestLengthsAreEqual(x, vector.X);
            TestUtility.TestLengthsAreEqual(y, vector.Y);
            TestUtility.TestLengthsAreEqual(z, vector.Z);
        }

        [Fact]
        public void ToStringTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var vector = new Vector3d(x, y, z);

            // Assert
            Assert.Equal("3D Vector (X:2.3 cm, Y:5.4 cm, Z:6.8 cm)", vector.ToString()); // note: using Thin Space \u2009
        }

        [Fact]
        public void VectorLengthTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            IVector3d vect = new Vector3d(x, y, z);
            double expectedLength = Math.Sqrt(2.3 * 2.3 + 5.4 * 5.4 + 6.8 * 6.8);

            // Assert
            Assert.Equal(expectedLength, vect.Length.Value);
        }

        [Fact]
        public void VectorSurvivesJsonRoundtrivectorest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            IVector3d vect = new Vector3d(x, y, z);
            string json = vect.ToJson();
            IVector3d vectDeserialized = json.FromJson<Vector3d>();

            // Assert
            TestUtility.TestLengthsAreEqual(vect.X, vectDeserialized.X);
            TestUtility.TestLengthsAreEqual(vect.Y, vectDeserialized.Y);
            TestUtility.TestLengthsAreEqual(vect.Z, vectDeserialized.Z);
        }

        [Fact]
        public void VectorCastToVectorTest()
        {
            // Assemble
            var x = new Length(2.3, LengthUnit.Centimeter);
            var y = new Length(5.4, LengthUnit.Centimeter);
            var z = new Length(6.8, LengthUnit.Centimeter);

            // Act
            var vect = new Vector3d(x, y, z);
            var vector = (Point3d)vect;

            // Assert
            TestUtility.TestLengthsAreEqual(x, vector.X);
            TestUtility.TestLengthsAreEqual(y, vector.Y);
            TestUtility.TestLengthsAreEqual(z, vector.Z);
        }
    }
}
