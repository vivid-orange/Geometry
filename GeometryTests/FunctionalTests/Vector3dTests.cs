using VividOrange.Geometry;
using UnitsNet;
using UnitsNet.Units;

namespace GeometryTests.FunctionalTests
{
    public class Vector3dTests
    {
        [Theory]
        [InlineData(69, 0, 0, 1, 0, 0)]
        [InlineData(0, 35, 0, 0, 1, 0)]
        [InlineData(0, 0, 14, 0, 0, 1)]
        public void NormalisedTest(double x, double y, double z, double expX, double expY, double expZ)
        {
            // Assemble
            var v = new Vector3d(x, y, z, LengthUnit.Meter);

            // Act
            Vector3d normalised = v.Normalised();

            // Assert
            Assert.Equal(expX, normalised.X.Value);
            Assert.Equal(expY, normalised.Y.Value);
            Assert.Equal(expZ, normalised.Z.Value);
        }

        [Fact]
        public void ScalarProductTest()
        {
            // Assemble
            Vector3d v1 = Vector3d.UnitX;
            Vector3d v2 = Vector3d.UnitY;

            // Act
            double scalarProd = Vector3d.ScalarProduct(v1, v2);

            // Assert
            Assert.Equal(0, scalarProd);
        }

        [Fact]
        public void VectorAngleTest()
        {
            // Assemble
            Vector3d v1 = Vector3d.UnitX;
            Vector3d v2 = Vector3d.UnitY;

            // Act
            Angle angle = Vector3d.VectorAngle(v1, v2);

            // Assert
            Assert.Equal(90, angle.Degrees);
        }

        [Fact]
        public void CrossProductTest()
        {
            // Assemble
            Vector3d v1 = Vector3d.UnitX;
            Vector3d v2 = Vector3d.UnitY;

            // Act
            Vector3d cross = Vector3d.CrossProduct(v1, v2);

            // Assert
            Assert.Equal(0, cross.X.Meters);
            Assert.Equal(0, cross.Y.Meters);
            Assert.Equal(1, cross.Z.Meters);
        }

        [Fact]
        public void TriangleAreaTest()
        {
            // Assemble
            Vector3d v1 = Vector3d.UnitX;
            Vector3d v2 = Vector3d.UnitY;

            // Act
            Area area = Vector3d.TriangleArea(v1, v2);

            // Assert
            Assert.Equal(0.5, area.Value);
        }

        [Fact]
        public void VectorialProductTest()
        {
            // Assemble
            Vector3d v1 = Vector3d.UnitX;
            Vector3d v2 = Vector3d.UnitY;

            // Act
            Vector3d cross = Vector3d.VectorialProduct(v1, v2);

            // Assert
            Assert.Equal(0, cross.X.Meters);
            Assert.Equal(0, cross.Y.Meters);
            Assert.Equal(1, cross.Z.Meters);
        }
    }
}
