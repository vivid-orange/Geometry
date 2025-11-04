using VividOrange.Geometry;

namespace GeometryTests.UnitTests
{
    public class BrushTests
    {
        [Fact]
        public void CreateBrushTest1()
        {
            // Assemble
            byte alpha = 0;
            byte red = 1;
            byte green = 2;
            byte blue = 3;

            // Act
            IBrush brush = new Brush(alpha, red, green, blue);

            // Assert
            Assert.Equal(alpha, brush.Color.Alpha);
            Assert.Equal(red, brush.Color.Red);
            Assert.Equal(green, brush.Color.Green);
            Assert.Equal(blue, brush.Color.Blue);
        }

        [Fact]
        public void CreateBrushTest2()
        {
            // Assemble
            byte red = 1;
            byte green = 2;
            byte blue = 3;

            // Act
            IBrush brush = new Brush(red, green, blue);

            // Assert
            Assert.Equal(255, brush.Color.Alpha);
            Assert.Equal(red, brush.Color.Red);
            Assert.Equal(green, brush.Color.Green);
            Assert.Equal(blue, brush.Color.Blue);
        }
    }
}
