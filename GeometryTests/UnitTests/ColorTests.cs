using VividOrange.Geometry;

namespace GeometryTests.UnitTests
{
    public class ColorTests
    {
        [Fact]
        public void CreateColorTest()
        {
            // Assemble
            byte alpha = 0;
            byte red = 1;
            byte green = 2;
            byte blue = 3;

            // Act
            IColor col = new Color(alpha, red, green, blue);

            // Assert
            Assert.Equal(50462976, col.ColorInt);
            Assert.Equal(alpha, col.Alpha);
            Assert.Equal(red, col.Red);
            Assert.Equal(green, col.Green);
            Assert.Equal(blue, col.Blue);
        }
    }
}
