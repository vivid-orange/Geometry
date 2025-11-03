using VividOrange.Geometry.Extensions;

namespace GeometryTests.UnitTests
{
    public class ListExtensionTests
    {
        [Fact]
        public void NullListTest()
        {
            // Assemble
            List<int> list = null;

            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();

            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void EmptyListTest()
        {
            // Assemble
            var list = new List<int>();

            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();

            // Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void NotNullOrEmptyListTest()
        {
            // Assemble
            var list = new List<int>()
            {
                1,
            };

            // Act
            bool isNullOrEmpty = list.IsNullOrEmpty();

            // Assert
            Assert.False(isNullOrEmpty);
        }
    }
}
