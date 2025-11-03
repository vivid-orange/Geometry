using UnitsNet;
using VividOrange.Geometry;

namespace GeometryTests.FunctionalTests
{
    public class Vector2dTests
    {
        [Fact]
        public void ScalarProductTest()
        {
            // Assemble
            Vector2d v1 = Vector2d.UnitU;
            Vector2d v2 = Vector2d.UnitV;

            // Act
            double scalarProd = Vector2d.ScalarProduct(v1, v2);

            // Assert
            Assert.Equal(0, scalarProd);
        }

        [Fact]
        public void VectorAngleTest()
        {
            // Assemble
            Vector2d v1 = Vector2d.UnitU;
            Vector2d v2 = Vector2d.UnitV;

            // Act
            Angle angle = Vector2d.VectorAngle(v1, v2);

            // Assert
            Assert.Equal(90, angle.Degrees);
        }
    }
}
