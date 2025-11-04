using VividOrange.Geometry.Extensions;

namespace GeometryTests.UnitTests
{
    public class DoubleExtensionsTests
    {
        [Theory]
        [InlineData(-1.23456, 2, -1.2)]
        [InlineData(1.23456, 4, 1.235)]
        [InlineData(-123456, 3, -123000)]
        [InlineData(123456, 1, 100000)]
        public void RoundToSignificantFigureTest(double value, int digits, double expected)
        {
            // Act
            double d = value.RoundToSignificantFigure(digits);

            // Assert
            Assert.Equal(expected, d);
        }
    }
}
